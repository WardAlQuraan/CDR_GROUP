import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyDistinguishesService } from '../../../../services/company-distinguishes.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyDistinguishDto,
  CompanyDistinguishPagedRequest
} from '../../../../models/company-distinguish.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyDistinguishDialogComponent,
  CompanyDistinguishDialogData
} from '../company-distinguish-dialog/company-distinguish-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-distinguishes-component',
  standalone: false,
  templateUrl: './company-distinguishes-component.html',
  styleUrl: './company-distinguishes-component.scss',
})
export class CompanyDistinguishesComponent implements OnInit {
  distinguishes: CompanyDistinguishDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['titleEn', 'titleAr', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyDistinguishDto>;

  constructor(
    private companyDistinguishesService: CompanyDistinguishesService,
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
    this.loadDistinguishes();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyDistinguishes.title',
      subtitle: 'admin.companyDistinguishes.subtitle',
      addButton: {
        label: 'admin.companyDistinguishes.addDistinguish',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_DISTINGUISHES_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyDistinguishes'),

      columns: [
        {
          key: 'title',
          header: 'admin.companyDistinguishes.distinguishTitle',
          cell: (row) => (this.isArabic ? row.titleAr : row.titleEn) || '-'
        },
        {
          key: 'description',
          header: 'admin.companyDistinguishes.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyDistinguishes.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyDistinguishes.edit',
          permission: Permissions.COMPANY_DISTINGUISHES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyDistinguishes.delete',
          permission: Permissions.COMPANY_DISTINGUISHES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteDistinguish(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyDistinguish', row.id])
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

  loadDistinguishes(): void {
    this.loading = true;
    const request: CompanyDistinguishPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyDistinguishesService.getCompanyDistinguishesPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.distinguishes = response.data.data;
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
    this.loadDistinguishes();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadDistinguishes();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadDistinguishes();
  }

  openCreateDialog(): void {
    const dialogData: CompanyDistinguishDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyDistinguishDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDistinguishes();
      }
    });
  }

  openEditDialog(distinguish: CompanyDistinguishDto): void {
    const dialogData: CompanyDistinguishDialogData = { mode: 'edit', companyId: this.companyId, distinguish };
    const dialogRef = this.dialog.open(CompanyDistinguishDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadDistinguishes();
      }
    });
  }

  deleteDistinguish(distinguish: CompanyDistinguishDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyDistinguishes.deleteDistinguish'),
      message: this.translate('admin.companyDistinguishes.confirmDelete'),
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
        this.companyDistinguishesService.delete(distinguish.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyDistinguishes.distinguishDeleted'));
            this.loadDistinguishes();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyDistinguishes.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
