import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { DepartmentsService } from '../../../../services/departments.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { DepartmentDto } from '../../../../models/department.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { DepartmentDialogComponent, DepartmentDialogData } from '../department-dialog/department-dialog.component';
import { DepartmentViewDialogComponent } from '../department-view-dialog/department-view-dialog.component';
import { DepartmentAssignManagerDialogComponent } from '../department-assign-manager-dialog/department-assign-manager-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';

@Component({
  selector: 'app-departments-component',
  standalone: false,
  templateUrl: './departments-component.html',
  styleUrl: './departments-component.scss',
})
export class DepartmentsComponent implements OnInit {
  departments: DepartmentDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<DepartmentDto>;

  constructor(
    private departmentsService: DepartmentsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadDepartments();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.departments.title',
      subtitle: 'admin.departments.subtitle',
      addButton: {
        label: 'admin.departments.addDepartment',
        icon: 'add',
        color: 'primary',
        permission: Permissions.DEPARTMENTS_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.departments.searchPlaceholder'),
      filters: [
        {
          key: 'status',
          label: 'admin.departments.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.departments.all' },
            { value: 'active', label: 'admin.departments.active' },
            { value: 'inactive', label: 'admin.departments.inactive' }
          ]
        }
      ],
      columns: [
        { key: 'code', header: 'admin.departments.code', sortable: true },
        {
          key: 'name',
          header: 'admin.departments.name',
          sortable: true,
          cell: (row) => this.isArabic ? row.nameAr : row.nameEn
        },
        {
          key: 'parentDepartment',
          header: 'admin.departments.parentDepartment',
          cell: (row) => {
            if (!row.parentDepartment) return '-';
            return this.isArabic ? row.parentDepartment.nameAr : row.parentDepartment.nameEn;
          }
        },
        {
          key: 'manager',
          header: 'admin.departments.manager',
          cell: (row) => row.managerName || '-'
        },
        {
          key: 'isActive',
          header: 'admin.departments.status',
          type: 'badge',
          badge: (row) => ({
            text: row.isActive ? 'admin.departments.active' : 'admin.departments.inactive',
            color: row.isActive ? 'success' : 'warn'
          })
        }
      ],
      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.departments.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'account_tree',
          tooltip: 'admin.departments.organizationChart',
          color: 'success',
          onClick: (row) => this.openOrgChart(row)
        },
        {
          icon: 'edit',
          tooltip: 'admin.departments.edit',
          permission: Permissions.DEPARTMENTS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'supervisor_account',
          tooltip: 'admin.departments.assignManager',
          permission: Permissions.DEPARTMENTS_ASSIGN_MANAGER,
          color: 'primary',
          onClick: (row) => this.openAssignManagerDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.departments.delete',
          permission: Permissions.DEPARTMENTS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteDepartment(row)
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

  loadDepartments(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.filterValues['search'] || undefined,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.departmentsService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(dept =>
              statusFilter === 'active' ? dept.isActive : !dept.isActive
            );
          }

          this.departments = items;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadDepartments();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadDepartments();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadDepartments();
  }

  openCreateDialog(): void {
    const dialogData: DepartmentDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(DepartmentDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDepartments();
      }
    });
  }

  openEditDialog(department: DepartmentDto): void {
    const dialogData: DepartmentDialogData = { mode: 'edit', department };
    const dialogRef = this.dialog.open(DepartmentDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDepartments();
      }
    });
  }

  openViewDialog(department: DepartmentDto): void {
    this.dialog.open(DepartmentViewDialogComponent, {
      width: '700px',
      data: department
    });
  }

  openOrgChart(department: DepartmentDto): void {
    const url = `/admin/departments/${department.id}/org-chart`;
    window.open(url, '_blank');
  }

  openAssignManagerDialog(department: DepartmentDto): void {
    const dialogRef = this.dialog.open(DepartmentAssignManagerDialogComponent, {
      width: '500px',
      data: department,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDepartments();
      }
    });
  }

  deleteDepartment(department: DepartmentDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.departments.deleteDepartment'),
      message: this.translate('admin.departments.confirmDelete'),
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
        this.departmentsService.delete(department.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.departments.departmentDeleted'));
            this.loadDepartments();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.departments.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
