import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyDistributionMarketingsService } from '../../../../services/company-distribution-marketings.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyDistributionMarketingDto,
  CompanyDistributionMarketingPagedRequest
} from '../../../../models/company-distribution-marketing.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyDistributionMarketingDialogComponent,
  CompanyDistributionMarketingDialogData
} from '../company-distribution-marketing-dialog/company-distribution-marketing-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-distribution-marketings-component',
  standalone: false,
  templateUrl: './company-distribution-marketings-component.html',
  styleUrl: './company-distribution-marketings-component.scss',
})
export class CompanyDistributionMarketingsComponent implements OnInit {
  marketings: CompanyDistributionMarketingDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['titleEn', 'titleAr', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyDistributionMarketingDto>;

  constructor(
    private companyDistributionMarketingsService: CompanyDistributionMarketingsService,
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
    this.loadMarketings();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyDistributionMarketings.title',
      subtitle: 'admin.companyDistributionMarketings.subtitle',
      addButton: {
        label: 'admin.companyDistributionMarketings.addMarketing',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_DISTRIBUTION_MARKETINGS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyDistributionMarketings'),

      columns: [
        {
          key: 'title',
          header: 'admin.companyDistributionMarketings.marketingTitle',
          cell: (row) => (this.isArabic ? row.titleAr : row.titleEn) || '-'
        },
        {
          key: 'description',
          header: 'admin.companyDistributionMarketings.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyDistributionMarketings.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyDistributionMarketings.edit',
          permission: Permissions.COMPANY_DISTRIBUTION_MARKETINGS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyDistributionMarketings.delete',
          permission: Permissions.COMPANY_DISTRIBUTION_MARKETINGS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteMarketing(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyDistributionMarketing', row.id])
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

  loadMarketings(): void {
    this.loading = true;
    const request: CompanyDistributionMarketingPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyDistributionMarketingsService.getCompanyDistributionMarketingsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.marketings = response.data.data;
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
    this.loadMarketings();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadMarketings();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadMarketings();
  }

  openCreateDialog(): void {
    const dialogData: CompanyDistributionMarketingDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyDistributionMarketingDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadMarketings();
      }
    });
  }

  openEditDialog(marketing: CompanyDistributionMarketingDto): void {
    const dialogData: CompanyDistributionMarketingDialogData = { mode: 'edit', companyId: this.companyId, marketing };
    const dialogRef = this.dialog.open(CompanyDistributionMarketingDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadMarketings();
      }
    });
  }

  deleteMarketing(marketing: CompanyDistributionMarketingDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyDistributionMarketings.deleteMarketing'),
      message: this.translate('admin.companyDistributionMarketings.confirmDelete'),
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
        this.companyDistributionMarketingsService.delete(marketing.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyDistributionMarketings.marketingDeleted'));
            this.loadMarketings();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyDistributionMarketings.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
