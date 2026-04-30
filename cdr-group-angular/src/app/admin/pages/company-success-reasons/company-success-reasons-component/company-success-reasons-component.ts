import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanySuccessReasonsService } from '../../../../services/company-success-reasons.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanySuccessReasonDto,
  CompanySuccessReasonPagedRequest
} from '../../../../models/company-success-reason.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanySuccessReasonDialogComponent,
  CompanySuccessReasonDialogData
} from '../company-success-reason-dialog/company-success-reason-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-success-reasons-component',
  standalone: false,
  templateUrl: './company-success-reasons-component.html',
  styleUrl: './company-success-reasons-component.scss',
})
export class CompanySuccessReasonsComponent implements OnInit {
  successReasons: CompanySuccessReasonDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['reasonEn', 'reasonAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanySuccessReasonDto>;

  constructor(
    private companySuccessReasonsService: CompanySuccessReasonsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.companyId = this.route.snapshot.params['companyId'];
    this.initGridConfig();
    this.loadSuccessReasons();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companySuccessReasons.title',
      subtitle: 'admin.companySuccessReasons.subtitle',
      addButton: {
        label: 'admin.companySuccessReasons.addReason',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_SUCCESS_REASONS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companySuccessReasons'),

      columns: [
        {
          key: 'reason',
          header: 'admin.companySuccessReasons.reason',
          cell: (row) => (this.isArabic ? row.reasonAr : row.reasonEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companySuccessReasons.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companySuccessReasons.edit',
          permission: Permissions.COMPANY_SUCCESS_REASONS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companySuccessReasons.delete',
          permission: Permissions.COMPANY_SUCCESS_REASONS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteSuccessReason(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanySuccessReason', row.id])
        }
      ],
      showExport: false,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadSuccessReasons(): void {
    this.loading = true;
    const request: CompanySuccessReasonPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companySuccessReasonsService.getCompanySuccessReasonsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.successReasons = response.data.data;
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
    this.loadSuccessReasons();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadSuccessReasons();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadSuccessReasons();
  }

  openCreateDialog(): void {
    const dialogData: CompanySuccessReasonDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanySuccessReasonDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadSuccessReasons();
      }
    });
  }

  openEditDialog(successReason: CompanySuccessReasonDto): void {
    const dialogData: CompanySuccessReasonDialogData = { mode: 'edit', companyId: this.companyId, successReason };
    const dialogRef = this.dialog.open(CompanySuccessReasonDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadSuccessReasons();
      }
    });
  }

  deleteSuccessReason(successReason: CompanySuccessReasonDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companySuccessReasons.deleteReason'),
      message: this.translate('admin.companySuccessReasons.confirmDelete'),
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
        this.companySuccessReasonsService.delete(successReason.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companySuccessReasons.reasonDeleted'));
            this.loadSuccessReasons();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companySuccessReasons.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
