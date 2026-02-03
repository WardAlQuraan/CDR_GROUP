import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { UsersService } from '../../../../services/users.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { UserDto } from '../../../../models/user.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { UserDialogComponent, UserDialogData } from '../user-dialog/user-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';

@Component({
  selector: 'app-users-component',
  standalone: false,
  templateUrl: './users-component.html',
  styleUrl: './users-component.scss',
})
export class UsersComponent implements OnInit {
  users: UserDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<UserDto>;

  constructor(
    private usersService: UsersService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadUsers();
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.users.title',
      subtitle: 'admin.users.subtitle',
      addButton: {
        label: 'admin.users.addUser',
        icon: 'add',
        color: 'primary'
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.users.searchPlaceholder'),
      filters: [
        {
          key: 'status',
          label: 'admin.users.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.users.all' },
            { value: 'active', label: 'admin.active' },
            { value: 'inactive', label: 'admin.inactive' }
          ]
        }
      ],
      columns: [
        { key: 'username', header: 'admin.users.username', sortable: true },
        { key: 'email', header: 'admin.users.email', sortable: true },
        {
          key: 'fullName',
          header: 'admin.users.fullName',
          cell: (row) => `${row.firstName || ''} ${row.lastName || ''}`.trim() || '-'
        },
        { key: 'phoneNumber', header: 'admin.users.phone', cell: (row) => row.phoneNumber || '-' },
        {
          key: 'isActive',
          header: 'admin.status',
          cell: (row) => row.isActive ? this.translate('admin.active') : this.translate('admin.inactive')
        },
        {
          key: 'createdAt',
          header: 'admin.users.created',
          sortable: true,
          cell: (row) => new Date(row.createdAt).toLocaleDateString()
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.users.edit',
          permission: Permissions.USERS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'toggle_on',
          tooltip: 'admin.users.deactivate',
          permission: Permissions.USERS_ACTIVATE,
          color: 'success',
          onClick: (row) => this.toggleUserStatus(row),
          visible: (row) => row.isActive
        },
        {
          icon: 'toggle_off',
          tooltip: 'admin.users.activate',
          permission: Permissions.USERS_ACTIVATE,
          color: 'warn',
          onClick: (row) => this.toggleUserStatus(row),
          visible: (row) => !row.isActive
        },
        {
          icon: 'delete',
          tooltip: 'admin.users.delete',
          permission: Permissions.USERS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteUser(row)
        }
      ],
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadUsers(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.filterValues['search'] || undefined,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.usersService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter (if API doesn't support it)
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(user =>
              statusFilter === 'active' ? user.isActive : !user.isActive
            );
          }

          this.users = items;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.users.errors.loadFailed'));
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadUsers();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadUsers();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadUsers();
  }

  openCreateDialog(): void {
    const dialogData: UserDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(UserDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUsers();
      }
    });
  }

  openEditDialog(user: UserDto): void {
    const dialogData: UserDialogData = { mode: 'edit', user };
    const dialogRef = this.dialog.open(UserDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUsers();
      }
    });
  }

  toggleUserStatus(user: UserDto): void {
    const action = user.isActive ? this.usersService.deactivate(user.id) : this.usersService.activate(user.id);
    const successMessage = user.isActive
      ? this.translate('admin.users.userDeactivated')
      : this.translate('admin.users.userActivated');

    this.loading = true;
    action.subscribe({
      next: () => {
        this.snackbar.success(successMessage);
        this.loadUsers();
      },
      error: (error) => {
        this.snackbar.error(error.message);
        this.loading = false;
      }
    });
  }

  deleteUser(user: UserDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.users.deleteUser'),
      message: this.translate('admin.users.confirmDelete'),
      confirmLabel: this.translate('common.delete'),
      cancelLabel: this.translate('common.cancel'),
      confirmColor: 'warn',
      icon: 'delete'
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.loading = true;
        this.usersService.delete(user.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.users.userDeleted'));
            this.loadUsers();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.users.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
