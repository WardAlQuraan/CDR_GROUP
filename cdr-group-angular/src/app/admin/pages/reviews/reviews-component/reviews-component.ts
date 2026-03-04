import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { ReviewsService } from '../../../../services/reviews.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { ReviewDto, ReviewPagedRequest } from '../../../../models/review.model';
import { CompanyDto } from '../../../../models/company.model';
import { CompaniesService } from '../../../../services/companies.service';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { ReviewDialogComponent, ReviewDialogData } from '../review-dialog/review-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-reviews-component',
  standalone: false,
  templateUrl: './reviews-component.html',
  styleUrl: './reviews-component.scss',
})
export class ReviewsComponent implements OnInit {
  reviews: ReviewDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['comment'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<ReviewDto>;
  companies: CompanyDto[] = [];

  constructor(
    private reviewsService: ReviewsService,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadCompanies();
    this.initGridConfig();
    this.loadReviews();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.reviews.title',
      subtitle: 'admin.reviews.subtitle',

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.reviews'),
      filters: [
        {
          key: 'isVisible',
          label: 'admin.reviews.visibilityFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.reviews.all' },
            { value: 'true', label: 'admin.reviews.visible' },
            { value: 'false', label: 'admin.reviews.hidden' }
          ]
        },
        {
          key: 'companyId',
          label: 'admin.reviews.companyFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.reviews.allCompanies' },
            ...this.companies.map(c => ({
              value: c.id,
              label: this.isArabic ? c.nameAr : c.nameEn
            }))
          ]
        }
      ],

      columns: [
        {
          key: 'numberOfStars',
          header: 'admin.reviews.numberOfStars',
          sortable: true,
          cell: (row) => '★'.repeat(row.numberOfStars) + '☆'.repeat(5 - row.numberOfStars)
        },
        {
          key: 'comment',
          header: 'admin.reviews.comment',
          cell: (row) => row.comment.length > 80 ? row.comment.substring(0, 80) + '...' : row.comment
        },
        {
          key: 'companyName',
          header: 'admin.reviews.company',
          cell: (row) => (this.isArabic ? row.companyNameAr : row.companyNameEn) || '-'
        },
        {
          key: 'isVisible',
          header: 'admin.reviews.isVisible',
          type: 'badge',
          badge: (row) => ({
            text: row.isVisible ? 'admin.reviews.visible' : 'admin.reviews.hidden',
            color: row.isVisible ? 'success' : 'warn'
          })
        },
        {
          key: 'createdAt',
          header: 'admin.reviews.createdAt',
          type: 'date',
          sortable: true,
          dateFormat: 'medium'
        }
      ],

      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.reviews.edit',
          permission: Permissions.REVIEWS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.reviews.delete',
          permission: Permissions.REVIEWS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteReview(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'Review', row.id])
        }
      ],

      showExport: true,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  loadCompanies(): void {
    this.companiesService.getActiveCompanies().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.companies = response.data;
          this.initGridConfig();
          this.cdr.markForCheck();
        }
      }
    });
  }

  loadReviews(): void {
    this.loading = true;
    const visibilityFilter = this.filterValues['isVisible'];
    const companyFilter = this.filterValues['companyId'];
    const request: ReviewPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      isVisible: visibilityFilter && visibilityFilter !== 'all' ? visibilityFilter === 'true' : undefined,
      companyId: companyFilter && companyFilter !== 'all' ? companyFilter : undefined
    };

    this.reviewsService.getReviewsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.reviews = response.data.data;
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
    this.loadReviews();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadReviews();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadReviews();
  }

  openEditDialog(review: ReviewDto): void {
    const dialogData: ReviewDialogData = { mode: 'edit', review };
    const dialogRef = this.dialog.open(ReviewDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadReviews();
      }
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.reviewsService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'Reviews');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteReview(review: ReviewDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.reviews.deleteReview'),
      message: this.translate('admin.reviews.confirmDelete'),
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
        this.reviewsService.delete(review.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.reviews.reviewDeleted'));
            this.loadReviews();
          },
          error: () => {
            this.loading = false;
            this.cdr.markForCheck();
          }
        });
      }
    });
  }
}
