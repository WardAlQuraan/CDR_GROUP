import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyDto } from '../../../../models/company.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyDialogComponent, CompanyDialogData } from '../company-dialog/company-dialog.component';
import { CompanyLogoDialogComponent } from '../company-logo-dialog/company-logo-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-companies-component',
  standalone: false,
  templateUrl: './companies-component.html',
  styleUrl: './companies-component.scss',
})
export class CompaniesComponent implements OnInit {
  companies: CompanyDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['nameEn', 'nameAr'];

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyDto>;

  constructor(
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadCompanies();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.companies.title',
      subtitle: 'admin.companies.subtitle',
      addButton: {
        label: 'admin.companies.addCompany',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANIES_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companies'),
      filters: [
        {
          key: 'status',
          label: 'admin.companies.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.companies.all' },
            { value: 'active', label: 'admin.companies.active' },
            { value: 'inactive', label: 'admin.companies.inactive' }
          ]
        }
      ],
      columns: [
        {
          key: 'name',
          header: 'admin.companies.name',
          sortable: true,
          sortBy: this.isArabic ? 'nameAr' : 'nameEn',
          cell: (row) => this.isArabic ? row.nameAr : row.nameEn
        },
        {
          key: 'description',
          header: 'admin.companies.description',
          cell: (row) => {
            const desc = this.isArabic ? row.descriptionAr : row.descriptionEn;
            return desc || '-';
          }
        },
        {
          key: 'parent',
          header: 'admin.companies.parentCompany',
          cell: (row) => {
            if (!row.parentId) return '-';
            return (this.isArabic ? row.parentNameAr : row.parentNameEn) || '-';
          }
        },
        {
          key: 'isActive',
          header: 'admin.companies.status',
          type: 'badge',
          badge: (row) => ({
            text: row.isActive ? 'admin.companies.active' : 'admin.companies.inactive',
            color: row.isActive ? 'success' : 'warn'
          })
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companies.edit',
          permission: Permissions.COMPANIES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companies.delete',
          permission: Permissions.COMPANIES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteCompany(row)
        },
        {
          icon: 'account_tree',
          tooltip: 'admin.companies.organizationChart',
          color: 'success',
          primary: false,
          onClick: (row) => this.openOrgChart(row)
        },
        {
          icon: 'contacts',
          tooltip: 'admin.companyContacts.contacts',
          permission: Permissions.COMPANY_CONTACTS_READ,
          color: 'primary',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'contacts'])
        },
        {
          icon: 'store',
          tooltip: 'admin.companyBranches.branches',
          permission: Permissions.COMPANY_BRANCHES_READ,
          color: 'primary',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'branches'])
        },
        {
          icon: 'wallpaper',
          tooltip: 'admin.companies.backgrounds',
          permission: Permissions.COMPANY_BACKGROUNDS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'backgrounds'])
        },
        {
          icon: 'description',
          tooltip: 'admin.companies.forms',
          permission: Permissions.COMPANY_FORMS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'forms'])
        },
        {
          icon: 'tune',
          tooltip: 'admin.companies.preferences',
          permission: Permissions.COMPANY_PREFERENCES_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'preferences'])
        },
        {
          icon: 'emoji_events',
          tooltip: 'admin.companies.successReasons',
          permission: Permissions.COMPANY_SUCCESS_REASONS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'success-reasons'])
        },
        {
          icon: 'workspace_premium',
          tooltip: 'admin.companies.distinguishes',
          permission: Permissions.COMPANY_DISTINGUISHES_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'distinguishes'])
        },
        {
          icon: 'campaign',
          tooltip: 'admin.companies.distributionMarketings',
          permission: Permissions.COMPANY_DISTRIBUTION_MARKETINGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'distribution-marketings'])
        },
        {
          icon: 'attach_money',
          tooltip: 'admin.companies.financialClausesRights',
          permission: Permissions.COMPANY_FINANCIAL_CLAUSES_RIGHTS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'financial-clauses-rights'])
        },
        {
          icon: 'assessment',
          tooltip: 'admin.companies.preContractStudies',
          permission: Permissions.COMPANY_PRE_CONTRACT_STUDIES_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'pre-contract-studies'])
        },
        {
          icon: 'public',
          tooltip: 'admin.companies.geographicExpansions',
          permission: Permissions.COMPANY_GEOGRAPHIC_EXPANSIONS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'geographic-expansions'])
        },
        {
          icon: 'handshake',
          tooltip: 'admin.companies.partnershipFranchiseMechanisms',
          permission: Permissions.COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/companies', row.id, 'partnership-franchise-mechanisms'])
        },
        {
          icon: 'image',
          tooltip: 'admin.companies.uploadLogo',
          permission: Permissions.COMPANIES_UPDATE,
          color: 'primary',
          primary: false,
          onClick: (row) => this.openLogoDialog(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'Company', row.id])
        }
      ],
      showExport: true,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadCompanies(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.companiesService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data.data;

          // Client-side status filter
          const statusFilter = this.filterValues['status'];
          if (statusFilter && statusFilter !== 'all') {
            items = items.filter(company =>
              statusFilter === 'active' ? company.isActive : !company.isActive
            );
          }

          this.companies = items;
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
    this.loadCompanies();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadCompanies();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadCompanies();
  }

  openCreateDialog(): void {
    const dialogData: CompanyDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(CompanyDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCompanies();
      }
    });
  }

  openEditDialog(company: CompanyDto): void {
    const dialogData: CompanyDialogData = { mode: 'edit', company };
    const dialogRef = this.dialog.open(CompanyDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadCompanies();
      }
    });
  }

  openOrgChart(company: CompanyDto): void {
    const url = `/admin/companies/${company.id}/org-chart`;
    window.open(url, '_blank');
  }

  openLogoDialog(company: CompanyDto): void {
    const dialogRef = this.dialog.open(CompanyLogoDialogComponent, {
      width: '450px',
      data: company
    });

    dialogRef.afterClosed().subscribe(changed => {
      if (changed) {
        this.loadCompanies();
      }
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.companiesService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'Companies');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteCompany(company: CompanyDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companies.deleteCompany'),
      message: this.translate('admin.companies.confirmDelete'),
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
        this.companiesService.delete(company.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companies.companyDeleted'));
            this.loadCompanies();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companies.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
