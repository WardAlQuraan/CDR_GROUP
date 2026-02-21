import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { BranchesService } from '../../../../services/branches.service';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { BranchDto } from '../../../../models/branch.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { BranchDialogComponent, BranchDialogData } from '../branch-dialog/branch-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-branches-component',
  standalone: false,
  templateUrl: './branches-component.html',
  styleUrl: './branches-component.scss',
})
export class BranchesComponent implements OnInit {
  branches: BranchDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['code', 'address'];

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<BranchDto>;

  constructor(
    private branchesService: BranchesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadBranches();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.branches.title',
      subtitle: 'admin.branches.subtitle',
      addButton: {
        label: 'admin.branches.addBranch',
        icon: 'add',
        color: 'primary',
        permission: Permissions.BRANCHES_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.branches'),
      filters: [
        {
          key: 'status',
          label: 'admin.branches.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.branches.all' },
            { value: 'active', label: 'admin.branches.active' },
            { value: 'inactive', label: 'admin.branches.inactive' }
          ]
        }
      ],
      columns: [
        { key: 'code', header: 'admin.branches.code', sortable: true },
        {
          key: 'address',
          header: 'admin.branches.address',
          sortable: true,
          cell: (row) => row.address || '-'
        },
        {
          key: 'company',
          header: 'admin.branches.company',
          cell: (row) => this.isArabic ? row.companyNameAr : row.companyNameEn
        },
        {
          key: 'isPrimary',
          header: 'admin.branches.isPrimary',
          type: 'badge',
          badge: (row) => ({
            text: row.isPrimary ? 'common.yes' : 'common.no',
            color: row.isPrimary ? 'primary' : 'info'
          })
        },
        {
          key: 'isActive',
          header: 'admin.branches.status',
          type: 'badge',
          badge: (row) => ({
            text: row.isActive ? 'admin.branches.active' : 'admin.branches.inactive',
            color: row.isActive ? 'success' : 'warn'
          })
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.branches.edit',
          permission: Permissions.BRANCHES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.branches.delete',
          permission: Permissions.BRANCHES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteBranch(row)
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

  loadBranches(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.branchesService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(branch =>
              statusFilter === 'active' ? branch.isActive : !branch.isActive
            );
          }

          this.branches = items;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadBranches();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadBranches();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadBranches();
  }

  openCreateDialog(): void {
    const dialogData: BranchDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(BranchDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBranches();
      }
    });
  }

  openEditDialog(branch: BranchDto): void {
    const dialogData: BranchDialogData = { mode: 'edit', branch };
    const dialogRef = this.dialog.open(BranchDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBranches();
      }
    });
  }

  deleteBranch(branch: BranchDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.branches.deleteBranch'),
      message: this.translate('admin.branches.confirmDelete'),
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
        this.branchesService.delete(branch.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.branches.branchDeleted'));
            this.loadBranches();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.branches.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
