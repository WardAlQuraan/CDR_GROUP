import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyDto } from '../../../../models/company.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyDialogComponent, CompanyDialogData } from '../company-dialog/company-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';

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

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyDto>;

  constructor(
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
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
      searchPlaceholder: this.translate('admin.companies.searchPlaceholder'),
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
        { key: 'code', header: 'admin.companies.code', sortable: true },
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
          icon: 'account_tree',
          tooltip: 'admin.companies.organizationChart',
          color: 'success',
          onClick: (row) => this.openOrgChart(row)
        },
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

  loadCompanies(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: ['code', 'nameEn', 'nameAr'],
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
      width: '600px',
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
      width: '600px',
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
    const url = `/admin/companies/${company.code}/org-chart`;
    window.open(url, '_blank');
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
