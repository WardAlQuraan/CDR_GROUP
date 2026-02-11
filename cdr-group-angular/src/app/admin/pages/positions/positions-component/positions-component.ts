import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { PositionsService } from '../../../../services/positions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { PositionDto } from '../../../../models/position.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { PositionDialogComponent, PositionDialogData } from '../position-dialog/position-dialog.component';
import { PositionViewDialogComponent } from '../position-view-dialog/position-view-dialog.component';
import { PositionAssignDepartmentDialogComponent } from '../position-assign-department-dialog/position-assign-department-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';

@Component({
  selector: 'app-positions-component',
  standalone: false,
  templateUrl: './positions-component.html',
  styleUrl: './positions-component.scss',
})
export class PositionsComponent implements OnInit {
  positions: PositionDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<PositionDto>;

  constructor(
    private positionsService: PositionsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadPositions();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.positions.title',
      subtitle: 'admin.positions.subtitle',
      addButton: {
        label: 'admin.positions.addPosition',
        icon: 'add',
        color: 'primary',
        permission: Permissions.POSITIONS_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.positions.searchPlaceholder'),
      filters: [
        {
          key: 'status',
          label: 'admin.positions.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.positions.all' },
            { value: 'active', label: 'admin.positions.active' },
            { value: 'inactive', label: 'admin.positions.inactive' }
          ]
        }
      ],
      columns: [
        { key: 'code', header: 'admin.positions.code', sortable: true },
        {
          key: 'name',
          header: 'admin.positions.name',
          sortable: true,
          cell: (row) => this.isArabic ? row.nameAr : row.nameEn
        },
        {
          key: 'department',
          header: 'admin.positions.department',
          cell: (row) => {
            if (!row.departmentId) return '-';
            return this.isArabic ? (row.departmentNameAr || '-') : (row.departmentNameEn || '-');
          }
        },
        {
          key: 'salaryRange',
          header: 'admin.positions.salaryRange',
          cell: (row) => {
            if (!row.minSalary && !row.maxSalary) return '-';
            const min = row.minSalary?.toLocaleString() || '0';
            const max = row.maxSalary?.toLocaleString() || '∞';
            return `${min} - ${max}`;
          }
        },
        {
          key: 'isActive',
          header: 'admin.positions.status',
          type: 'badge',
          badge: (row) => ({
            text: row.isActive ? 'admin.positions.active' : 'admin.positions.inactive',
            color: row.isActive ? 'success' : 'warn'
          })
        }
      ],
      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.positions.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'edit',
          tooltip: 'admin.positions.edit',
          permission: Permissions.POSITIONS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'business',
          tooltip: 'admin.positions.assignDepartment',
          permission: Permissions.POSITIONS_UPDATE,
          color: 'primary',
          onClick: (row) => this.openAssignDepartmentDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.positions.delete',
          permission: Permissions.POSITIONS_DELETE,
          color: 'warn',
          onClick: (row) => this.deletePosition(row)
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

  loadPositions(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: ['code', 'nameEn', 'nameAr'],
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.positionsService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(pos =>
              statusFilter === 'active' ? pos.isActive : !pos.isActive
            );
          }

          this.positions = items;
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
    this.loadPositions();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadPositions();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadPositions();
  }

  openCreateDialog(): void {
    const dialogData: PositionDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(PositionDialogComponent, {
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPositions();
      }
    });
  }

  openEditDialog(position: PositionDto): void {
    const dialogData: PositionDialogData = { mode: 'edit', position };
    const dialogRef = this.dialog.open(PositionDialogComponent, {
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPositions();
      }
    });
  }

  openViewDialog(position: PositionDto): void {
    this.dialog.open(PositionViewDialogComponent, {
      width: '700px',
      data: position
    });
  }

  openAssignDepartmentDialog(position: PositionDto): void {
    const dialogRef = this.dialog.open(PositionAssignDepartmentDialogComponent, {
      data: position,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPositions();
      }
    });
  }

  deletePosition(position: PositionDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.positions.deletePosition'),
      message: this.translate('admin.positions.confirmDelete'),
      confirmLabel: this.translate('common.delete'),
      cancelLabel: this.translate('common.cancel'),
      confirmColor: 'warn',
      icon: 'delete'
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.loading = true;
        this.positionsService.delete(position.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.positions.positionDeleted'));
            this.loadPositions();
          },
          error: (error) => {
            this.loading = false;
            this.cdr.markForCheck();
          }
        });
      }
    });
  }
}
