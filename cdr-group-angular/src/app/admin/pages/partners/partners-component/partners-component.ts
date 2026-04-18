import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { PartnersService } from '../../../../services/partners.service';
import { CompaniesService } from '../../../../services/companies.service';
import { CitiesService } from '../../../../services/cities.service';
import { CountriesService } from '../../../../services/countries.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { PartnerDto, PartnerPagedRequest } from '../../../../models/partner.model';
import { CityDto } from '../../../../models/city.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { DataGridComponent } from '../../../../shared/components/data-grid/data-grid.component';
import { PartnerDialogComponent, PartnerDialogData } from '../partner-dialog/partner-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-partners-component',
  standalone: false,
  templateUrl: './partners-component.html',
  styleUrl: './partners-component.scss',
})
export class PartnersComponent implements OnInit {
  @ViewChild(DataGridComponent) dataGrid!: DataGridComponent<PartnerDto>;

  partners: PartnerDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<PartnerDto>;

  constructor(
    private partnersService: PartnersService,
    private companiesService: CompaniesService,
    private countriesService: CountriesService,
    private citiesService: CitiesService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadFilterOptions();
    this.initGridConfig();
    this.loadPartners();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private companyOptions: { value: string; label: string }[] = [];
  private countryOptions: { value: string; label: string }[] = [];
  private allCities: CityDto[] = [];
  private cityOptions: { value: string; label: string }[] = [];

  private loadFilterOptions(): void {
    this.companiesService.getAll().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.companyOptions = response.data.map(c => ({
            value: c.id,
            label: this.isArabic ? c.nameAr : c.nameEn
          }));
          this.initGridConfig();
          this.cdr.markForCheck();
        }
      }
    });

    this.countriesService.getAllCached().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.countryOptions = response.data.map(c => ({
            value: c.id,
            label: this.isArabic ? c.nameAr : c.nameEn
          }));
          this.initGridConfig();
          this.cdr.markForCheck();
        }
      }
    });

    this.citiesService.getAllCached().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.allCities = response.data;
          this.cityOptions = this.buildCityOptions(this.allCities);
          this.initGridConfig();
          this.cdr.markForCheck();
        }
      }
    });
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.partners.title',
      subtitle: 'admin.partners.subtitle',
      addButton: {
        label: 'admin.partners.addPartner',
        icon: 'add',
        color: 'primary',
        permission: Permissions.PARTNERS_CREATE
      },

      showSearch: false,
      filters: [
        {
          key: 'companyId',
          label: 'admin.partners.companyFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.partners.allCompanies' },
            ...this.companyOptions
          ]
        },
        {
          key: 'countryId',
          label: 'admin.partners.countryFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.partners.allCountries' },
            ...this.countryOptions
          ],
          selectionChange: (value: string) => this.onCountryFilterChange(value)
        },
        {
          key: 'cityId',
          label: 'admin.partners.cityFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.partners.allCities' },
            ...this.cityOptions
          ]
        },
        {
          key: 'status',
          label: 'admin.partners.statusFilter',
          type: 'select',
          defaultValue: 'all',
          options: [
            { value: 'all', label: 'admin.partners.all' },
            { value: 'Present', label: 'admin.partners.present' },
            { value: 'Available', label: 'admin.partners.available' },
            // { value: 'NotAvailable', label: 'admin.partners.notAvailable' }
          ]
        }
      ],
      columns: [
        {
          key: 'company',
          header: 'admin.partners.company',
          sortable: true,
          sortBy: this.isArabic ? 'companyNameAr' : 'companyNameEn',
          cell: (row) => (this.isArabic ? row.companyNameAr : row.companyNameEn) || '-'
        },
        {
          key: 'city',
          header: 'admin.partners.city',
          sortable: true,
          sortBy: this.isArabic ? 'cityNameAr' : 'cityNameEn',
          cell: (row) => (this.isArabic ? row.cityNameAr : row.cityNameEn) || '-'
        },
        {
          key: 'status',
          header: 'admin.partners.status',
          type: 'badge',
          badge: (row) => {
            const statusMap: Record<string, { text: string; color: 'success' | 'info' | 'warn' }> = {
              'Present': { text: 'admin.partners.present', color: 'success' },
              'Available': { text: 'admin.partners.available', color: 'info' },
              'NotAvailable': { text: 'admin.partners.notAvailable', color: 'warn' }
            };
            return statusMap[row.status] || { text: row.status, color: 'info' };
          }
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.partners.edit',
          permission: Permissions.PARTNERS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'Partner', row.id])
        },
        {
          icon: 'delete',
          tooltip: 'admin.partners.delete',
          permission: Permissions.PARTNERS_DELETE,
          color: 'warn',
          onClick: (row) => this.deletePartner(row)
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

  loadPartners(): void {
    this.loading = true;
    const request: PartnerPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.filterValues['companyId'] !== 'all' ? this.filterValues['companyId'] : undefined,
      countryId: this.filterValues['countryId'] !== 'all' ? this.filterValues['countryId'] : undefined,
      cityId: this.filterValues['cityId'] !== 'all' ? this.filterValues['cityId'] : undefined,
      status: this.filterValues['status'] !== 'all' ? this.filterValues['status'] : undefined
    };

    this.partnersService.getPartnersPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.partners = response.data.data;
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
    this.loadPartners();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadPartners();
  }

  onCountryFilterChange(countryId: string): void {
    const selectedCountryId = countryId !== 'all' ? countryId : null;
    const filtered = selectedCountryId
      ? this.allCities.filter(c => c.countryId === selectedCountryId)
      : this.allCities;

    const cityFilter = this.gridConfig.filters?.find(f => f.key === 'cityId');
    if (cityFilter) {
      cityFilter.options = [
        { value: 'all', label: 'admin.partners.allCities' },
        ...this.buildCityOptions(filtered)
      ];
    }
    this.dataGrid?.setFilterValue('cityId', 'all');
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadPartners();
  }

  private buildCityOptions(cities: CityDto[]): { value: string; label: string }[] {
    return cities.map(c => ({
      value: c.id,
      label: this.isArabic ? c.nameAr : c.nameEn
    }));
  }

  openCreateDialog(): void {
    const dialogData: PartnerDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(PartnerDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPartners();
      }
    });
  }

  openEditDialog(partner: PartnerDto): void {
    const dialogData: PartnerDialogData = { mode: 'edit', partner };
    const dialogRef = this.dialog.open(PartnerDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPartners();
      }
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.partnersService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'Partners');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deletePartner(partner: PartnerDto): void {
    const companyName = this.isArabic ? partner.companyNameAr : partner.companyNameEn;
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.partners.deletePartner'),
      message: this.translate('admin.partners.confirmDelete'),
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
        this.partnersService.delete(partner.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.partners.partnerDeleted'));
            this.loadPartners();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.partners.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
