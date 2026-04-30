import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CompanyFinancialClausesRightsDto, CompanyFinancialClausesRightsPagedRequest } from '../../../../models/company-financial-clauses-rights.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyFinancialClausesRightsService } from '../../../../services/company-financial-clauses-rights.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { CompanyFinancialClausesRightsDialogComponent, CompanyFinancialClausesRightsDialogData } from '../company-financial-clauses-rights-dialog/company-financial-clauses-rights-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslationService } from '../../../../services/translation.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Permissions } from '../../../../models/auth.model';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'app-company-financial-clauses-rights-component',
  standalone: false,
  templateUrl: './company-financial-clauses-rights-component.html',
  styleUrl: './company-financial-clauses-rights-component.scss',
})
export class CompanyFinancialClausesRightsComponent implements OnInit {
  financialClauses: CompanyFinancialClausesRightsDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['titleEn', 'titleAr', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyFinancialClausesRightsDto>;

  constructor(
    private companyFinancialClausesRightsService: CompanyFinancialClausesRightsService,
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
    this.loadClauses();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyFinancialClausesRights.title',
      subtitle: 'admin.companyFinancialClausesRights.subtitle',
      addButton: {
        label: 'admin.companyFinancialClausesRights.addClause',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_FINANCIAL_CLAUSES_RIGHTS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyFinancialClausesRights'),

      columns: [
        {
          key: 'title',
          header: 'admin.companyFinancialClausesRights.clauseTitle',
          cell: (row) => (this.isArabic ? row.titleAr : row.titleEn) || '-'
        },
        {
          key: 'description',
          header: 'admin.companyFinancialClausesRights.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyFinancialClausesRights.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyFinancialClausesRights.edit',
          permission: Permissions.COMPANY_FINANCIAL_CLAUSES_RIGHTS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyFinancialClausesRights.delete',
          permission: Permissions.COMPANY_FINANCIAL_CLAUSES_RIGHTS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteFinancialClause(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyFinancialClausesRights', row.id])
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

  loadClauses(): void {
    this.loading = true;
    const request: CompanyFinancialClausesRightsPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyFinancialClausesRightsService.getCompanyFinancialClausesRights(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.financialClauses = response.data.data;
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
    this.loadClauses();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadClauses();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadClauses();
  }

  openCreateDialog(): void {
    const dialogData: CompanyFinancialClausesRightsDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyFinancialClausesRightsDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadClauses();
      }
    });
  }

  openEditDialog(financialClause: CompanyFinancialClausesRightsDto): void {
    const dialogData: CompanyFinancialClausesRightsDialogData = { mode: 'edit', companyId: this.companyId, financialClause };
    const dialogRef = this.dialog.open(CompanyFinancialClausesRightsDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadClauses();
      }
    });
  }

  deleteFinancialClause(financialClause: CompanyFinancialClausesRightsDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyFinancialClausesRights.deleteClause'),
      message: this.translate('admin.companyFinancialClausesRights.confirmDelete'),
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
        this.companyFinancialClausesRightsService.delete(financialClause.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyFinancialClausesRights.clauseDeleted'));
            this.loadClauses();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyFinancialClausesRights.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
