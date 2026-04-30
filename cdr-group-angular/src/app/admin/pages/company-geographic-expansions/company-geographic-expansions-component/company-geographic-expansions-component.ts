import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyGeographicExpansionsService } from '../../../../services/company-geographic-expansions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyGeographicExpansionDto,
  CompanyGeographicExpansionPagedRequest
} from '../../../../models/company-geographic-expansion.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyGeographicExpansionDialogComponent,
  CompanyGeographicExpansionDialogData
} from '../company-geographic-expansion-dialog/company-geographic-expansion-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-geographic-expansions-component',
  standalone: false,
  templateUrl: './company-geographic-expansions-component.html',
  styleUrl: './company-geographic-expansions-component.scss',
})
export class CompanyGeographicExpansionsComponent implements OnInit {
  expansions: CompanyGeographicExpansionDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['titleEn', 'titleAr', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyGeographicExpansionDto>;

  constructor(
    private companyGeographicExpansionsService: CompanyGeographicExpansionsService,
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
    this.loadExpansions();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyGeographicExpansions.title',
      subtitle: 'admin.companyGeographicExpansions.subtitle',
      addButton: {
        label: 'admin.companyGeographicExpansions.addExpansion',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_GEOGRAPHIC_EXPANSIONS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyGeographicExpansions'),

      columns: [
        {
          key: 'title',
          header: 'admin.companyGeographicExpansions.expansionTitle',
          cell: (row) => (this.isArabic ? row.titleAr : row.titleEn) || '-'
        },
        {
          key: 'description',
          header: 'admin.companyGeographicExpansions.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyGeographicExpansions.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyGeographicExpansions.edit',
          permission: Permissions.COMPANY_GEOGRAPHIC_EXPANSIONS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyGeographicExpansions.delete',
          permission: Permissions.COMPANY_GEOGRAPHIC_EXPANSIONS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteExpansion(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyGeographicExpansion', row.id])
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

  loadExpansions(): void {
    this.loading = true;
    const request: CompanyGeographicExpansionPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyGeographicExpansionsService.getCompanyGeographicExpansionsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.expansions = response.data.data;
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
    this.loadExpansions();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadExpansions();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadExpansions();
  }

  openCreateDialog(): void {
    const dialogData: CompanyGeographicExpansionDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyGeographicExpansionDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadExpansions();
      }
    });
  }

  openEditDialog(expansion: CompanyGeographicExpansionDto): void {
    const dialogData: CompanyGeographicExpansionDialogData = { mode: 'edit', companyId: this.companyId, expansion };
    const dialogRef = this.dialog.open(CompanyGeographicExpansionDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadExpansions();
      }
    });
  }

  deleteExpansion(expansion: CompanyGeographicExpansionDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyGeographicExpansions.deleteExpansion'),
      message: this.translate('admin.companyGeographicExpansions.confirmDelete'),
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
        this.companyGeographicExpansionsService.delete(expansion.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyGeographicExpansions.expansionDeleted'));
            this.loadExpansions();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyGeographicExpansions.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
