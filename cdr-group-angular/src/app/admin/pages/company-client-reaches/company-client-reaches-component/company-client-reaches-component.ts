import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyClientReachesService } from '../../../../services/company-client-reaches.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyClientReachDto,
  CompanyClientReachPagedRequest
} from '../../../../models/company-client-reach.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyClientReachDialogComponent,
  CompanyClientReachDialogData
} from '../company-client-reach-dialog/company-client-reach-dialog.component';
import { CompanyClientReachLogoDialogComponent } from '../company-client-reach-logo-dialog/company-client-reach-logo-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-company-client-reaches-component',
  standalone: false,
  templateUrl: './company-client-reaches-component.html',
  styleUrl: './company-client-reaches-component.scss',
})
export class CompanyClientReachesComponent implements OnInit {
  reaches: CompanyClientReachDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['clientNameEn', 'clientNameAr', 'reach', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyClientReachDto>;

  constructor(
    private companyClientReachesService: CompanyClientReachesService,
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
    this.loadReaches();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyClientReaches.title',
      subtitle: 'admin.companyClientReaches.subtitle',
      addButton: {
        label: 'admin.companyClientReaches.addReach',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_CLIENT_REACHES_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyClientReaches'),

      columns: [
        {
          key: 'clientLogo',
          header: 'admin.companyClientReaches.clientLogo',
          type: 'image',
          imageUrl: (row) => this.resolveImageUrl(row.clientLogoUrl),
          sortable: false
        },
        {
          key: 'clientName',
          header: 'admin.companyClientReaches.clientName',
          sortable: true,
          sortBy: this.isArabic ? 'clientNameAr' : 'clientNameEn',
          cell: (row) => this.isArabic ? row.clientNameAr : row.clientNameEn
        },
        {
          key: 'reach',
          header: 'admin.companyClientReaches.reach',
          sortable: true
        },
        {
          key: 'description',
          header: 'admin.companyClientReaches.description',
          cell: (row) => {
            const desc = this.isArabic ? row.descriptionAr : row.descriptionEn;
            return desc || '-';
          }
        },
        {
          key: 'createdAt',
          header: 'admin.companyClientReaches.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyClientReaches.edit',
          permission: Permissions.COMPANY_CLIENT_REACHES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'image',
          tooltip: 'admin.companyClientReaches.uploadLogo',
          permission: Permissions.COMPANY_CLIENT_REACHES_UPDATE,
          color: 'primary',
          primary: false,
          onClick: (row) => this.openLogoDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyClientReaches.delete',
          permission: Permissions.COMPANY_CLIENT_REACHES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteReach(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyClientReach', row.id])
        }
      ],
      showExport: false,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  private resolveImageUrl(url?: string): string | undefined {
    if (!url) return undefined;
    if (/^https?:\/\//i.test(url)) return url;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${url.startsWith('/') ? '' : '/'}${url}`;
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadReaches(): void {
    this.loading = true;
    const request: CompanyClientReachPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyClientReachesService.getCompanyClientReachesPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.reaches = response.data.data;
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
    this.loadReaches();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadReaches();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadReaches();
  }

  openCreateDialog(): void {
    const dialogData: CompanyClientReachDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyClientReachDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadReaches();
      }
    });
  }

  openEditDialog(reach: CompanyClientReachDto): void {
    const dialogData: CompanyClientReachDialogData = { mode: 'edit', companyId: this.companyId, reach };
    const dialogRef = this.dialog.open(CompanyClientReachDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadReaches();
      }
    });
  }

  openLogoDialog(reach: CompanyClientReachDto): void {
    const dialogRef = this.dialog.open(CompanyClientReachLogoDialogComponent, {
      width: '450px',
      data: reach
    });

    dialogRef.afterClosed().subscribe(changed => {
      if (changed) {
        this.loadReaches();
      }
    });
  }

  deleteReach(reach: CompanyClientReachDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyClientReaches.deleteReach'),
      message: this.translate('admin.companyClientReaches.confirmDelete'),
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
        this.companyClientReachesService.delete(reach.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyClientReaches.reachDeleted'));
            this.loadReaches();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyClientReaches.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
