import { Component, OnInit, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { EmployeesService } from '../../../../services/employees.service';
import { FilesService } from '../../../../services/files.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { EmployeeDto } from '../../../../models/employee.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { EmployeeDialogComponent, EmployeeDialogData } from '../employee-dialog/employee-dialog.component';
import { EmployeeViewDialogComponent } from '../employee-view-dialog/employee-view-dialog.component';
import { EmployeeLinkUserDialogComponent } from '../employee-link-user-dialog/employee-link-user-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { EntityTypes } from '../../../../constants/entity-types.constant';

@Component({
  selector: 'app-employees-component',
  standalone: false,
  templateUrl: './employees-component.html',
  styleUrl: './employees-component.scss',
})
export class EmployeesComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  employees: EmployeeDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<EmployeeDto>;

  private selectedEmployeeForUpload: EmployeeDto | null = null;

  constructor(
    private employeesService: EmployeesService,
    private filesService: FilesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadEmployees();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.employees.title',
      subtitle: 'admin.employees.subtitle',
      addButton: {
        label: 'admin.employees.addEmployee',
        icon: 'add',
        color: 'primary',
        permission: Permissions.EMPLOYEES_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.employees.searchPlaceholder'),
      filters: [
        {
          key: 'status',
          label: 'admin.employees.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.employees.all' },
            { value: 'active', label: 'admin.employees.active' },
            { value: 'inactive', label: 'admin.employees.inactive' }
          ]
        }
      ],
      columns: [
        { key: 'employeeCode', header: 'admin.employees.code', sortable: true },
        {
          key: 'fullName',
          header: 'admin.employees.name',
          sortable: true,
          cell: (row) => this.isArabic ? row.fullNameAr : row.fullNameEn
        },
        { key: 'email', header: 'admin.employees.email', cell: (row) => row.email || '-' },
        { key: 'department', header: 'admin.employees.department', sortable: true, cell: (row) => row.departmentName || '-' },
        { key: 'position', header: 'admin.employees.position', cell: (row) => row.positionName || '-' },
        {
          key: 'manager',
          header: 'admin.employees.manager',
          cell: (row) => {
            if (!row.manager) return '-';
            return this.isArabic ? row.manager.fullNameAr : row.manager.fullNameEn;
          }
        },
        {
          key: 'isActive',
          header: 'admin.employees.status',
          type: 'badge',
          badge: (row) => ({
            text: row.isActive ? 'admin.employees.active' : 'admin.employees.inactive',
            color: row.isActive ? 'success' : 'warn'
          })
        }
      ],
      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.employees.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'edit',
          tooltip: 'admin.employees.edit',
          permission: Permissions.EMPLOYEES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'person_add',
          tooltip: 'admin.employees.linkToUser',
          permission: Permissions.EMPLOYEES_LINK_TO_USER,
          color: 'primary',
          onClick: (row) => this.openLinkToUserDialog(row),
          visible: (row) => !row.userId
        },
        {
          icon: 'photo_camera',
          tooltip: 'admin.employees.uploadPhoto',
          permission: Permissions.EMPLOYEES_UPDATE,
          color: 'primary',
          onClick: (row) => this.triggerFileUpload(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.employees.delete',
          permission: Permissions.EMPLOYEES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteEmployee(row)
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

  loadEmployees(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      search: this.filterValues['search'] || undefined,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.employeesService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(emp =>
              statusFilter === 'active' ? emp.isActive : !emp.isActive
            );
          }

          this.employees = items;
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
    this.loadEmployees();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadEmployees();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadEmployees();
  }

  openCreateDialog(): void {
    const dialogData: EmployeeDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(EmployeeDialogComponent, {
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEmployees();
      }
    });
  }

  openEditDialog(employee: EmployeeDto): void {
    const dialogData: EmployeeDialogData = { mode: 'edit', employee };
    const dialogRef = this.dialog.open(EmployeeDialogComponent, {
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEmployees();
      }
    });
  }

  openViewDialog(employee: EmployeeDto): void {
    this.dialog.open(EmployeeViewDialogComponent, {
      data: employee
    });
  }


  openLinkToUserDialog(employee: EmployeeDto): void {
    const dialogRef = this.dialog.open(EmployeeLinkUserDialogComponent, {
      data: employee,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEmployees();
      }
    });
  }

  triggerFileUpload(employee: EmployeeDto): void {
    this.selectedEmployeeForUpload = employee;
    this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length || !this.selectedEmployeeForUpload) return;

    const file = input.files[0];
    this.loading = true;

    this.filesService.upload({
      file,
      entityId: this.selectedEmployeeForUpload.id,
      entityType: EntityTypes.EMPLOYEE,
      removeOldFiles: true
    }).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.employees.photoUploaded'));
        this.loading = false;
        this.selectedEmployeeForUpload = null;
        this.cdr.markForCheck();
        input.value = '';
      },
      error: (error) => {
        this.cdr.markForCheck();
        this.loading = false;
        this.selectedEmployeeForUpload = null;
        input.value = '';
      }
    });
  }

  deleteEmployee(employee: EmployeeDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.employees.deleteEmployee'),
      message: this.translate('admin.employees.confirmDelete'),
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
        this.employeesService.delete(employee.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.employees.employeeDeleted'));
            this.loadEmployees();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.employees.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
