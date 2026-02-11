import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { RolesService } from '../../../../services/roles.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { RoleDto } from '../../../../models/role.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { RoleDialogComponent, RoleDialogData } from '../role-dialog/role-dialog.component';
import { PermissionsDialogComponent, PermissionsDialogData } from '../permissions-dialog/permissions-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { formatDateTime } from '../../../../utils/date.utils';

@Component({
  selector: 'app-roles-component',
  standalone: false,
  templateUrl: './roles-component.html',
  styleUrl: './roles-component.scss',
})
export class RolesComponent implements OnInit {
  roles: RoleDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<RoleDto>;

  constructor(
    private rolesService: RolesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadRoles();
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.roles.title',
      subtitle: 'admin.roles.subtitle',
      addButton: {
        label: 'admin.roles.addRole',
        icon: 'add',
        color: 'primary',
        permission: Permissions.ROLES_MANAGE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.roles.searchPlaceholder'),
      filters: [
        {
          key: 'type',
          label: 'admin.roles.typeFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.roles.all' },
            { value: 'system', label: 'admin.roles.systemRoles' },
            { value: 'custom', label: 'admin.roles.customRoles' }
          ]
        }
      ],
      columns: [
        { key: 'name', header: 'admin.roles.name', sortable: true },
        {
          key: 'description',
          header: 'admin.roles.description',
          cell: (row) => row.description || '-'
        },
        {
          key: 'isSystemRole',
          header: 'admin.roles.type',
          cell: (row) => row.isSystemRole ? this.translate('admin.roles.system') : this.translate('admin.roles.custom')
        },
        {
          key: 'permissions',
          header: 'admin.roles.permissions',
          cell: (row) => row.permissions?.length?.toString() || '0'
        },
        {
          key: 'createdBy',
          header: 'admin.roles.createdBy',
          cell: (row) => row.createdBy || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.roles.created',
          sortable: true,
          cell: (row) => formatDateTime(row.createdAt)
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.roles.edit',
          permission: Permissions.ROLES_MANAGE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row),
          // visible: (row) => !row.isSystemRole
        },
        {
          icon: 'visibility',
          tooltip: 'admin.roles.view',
          permission: Permissions.ROLES_READ,
          color: 'info',
          onClick: (row) => this.openViewDialog(row),
          // visible: (row) => row.isSystemRole
        },
        {
          icon: 'security',
          tooltip: 'admin.roles.managePermissions',
          permission: Permissions.ROLES_MANAGE,
          color: 'primary',
          onClick: (row) => this.openPermissionsDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.roles.delete',
          permission: Permissions.ROLES_MANAGE,
          color: 'warn',
          onClick: (row) => this.deleteRole(row),
          visible: (row) => !row.isSystemRole
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

  loadRoles(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: ['name', 'description'],
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.rolesService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side type filter (if API doesn't support it)
          const typeFilter = this.filterValues['type'];
          if (typeFilter && typeFilter !== 'all') {
            items = items.filter(role =>
              typeFilter === 'system' ? role.isSystemRole : !role.isSystemRole
            );
          }

          this.roles = items;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadRoles();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadRoles();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadRoles();
  }

  openCreateDialog(): void {
    const dialogData: RoleDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(RoleDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadRoles();
      }
    });
  }

  openEditDialog(role: RoleDto): void {
    const dialogData: RoleDialogData = { mode: 'edit', role };
    const dialogRef = this.dialog.open(RoleDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadRoles();
      }
    });
  }

  openViewDialog(role: RoleDto): void {
    const dialogData: RoleDialogData = { mode: 'view', role };
    this.dialog.open(RoleDialogComponent, {
      width: '600px',
      data: dialogData
    });
  }

  openPermissionsDialog(role: RoleDto): void {
    const dialogData: PermissionsDialogData = { role };
    const dialogRef = this.dialog.open(PermissionsDialogComponent, {
      width: '800px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadRoles();
      }
    });
  }

  deleteRole(role: RoleDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.roles.deleteRole'),
      message: this.translate('admin.roles.confirmDelete'),
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
        this.rolesService.delete(role.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.roles.roleDeleted'));
            this.loadRoles();
          },
          error: (error) => {
            this.cdr.markForCheck();
            this.loading = false;
          }
        });
      }
    });
  }
}
