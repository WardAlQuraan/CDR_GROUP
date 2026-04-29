import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyPreferencesService } from '../../../../services/company-preferences.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyPreferenceDto, CompanyPreferencePagedRequest } from '../../../../models/company-preference.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyPreferenceDialogComponent, CompanyPreferenceDialogData } from '../company-preference-dialog/company-preference-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-preferences-component',
  standalone: false,
  templateUrl: './company-preferences-component.html',
  styleUrl: './company-preferences-component.scss',
})
export class CompanyPreferencesComponent implements OnInit {
  preferences: CompanyPreferenceDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['code', 'valueEn', 'valueAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyPreferenceDto>;

  constructor(
    private companyPreferencesService: CompanyPreferencesService,
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
    this.loadPreferences();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyPreferences.title',
      subtitle: 'admin.companyPreferences.subtitle',
      addButton: {
        label: 'admin.companyPreferences.addPreference',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_PREFERENCES_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyPreferences'),

      columns: [
        {
          key: 'code',
          header: 'admin.companyPreferences.code',
          sortable: true,
          cell: (row) => row.code
        },
        {
          key: 'value',
          header: 'admin.companyPreferences.value',
          cell: (row) => (this.isArabic ? row.valueAr : row.valueEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyPreferences.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyPreferences.edit',
          permission: Permissions.COMPANY_PREFERENCES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyPreferences.delete',
          permission: Permissions.COMPANY_PREFERENCES_DELETE,
          color: 'warn',
          onClick: (row) => this.deletePreference(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyPreference', row.id])
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

  loadPreferences(): void {
    this.loading = true;
    const request: CompanyPreferencePagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyPreferencesService.getCompanyPreferencesPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.preferences = response.data.data;
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
    this.loadPreferences();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadPreferences();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadPreferences();
  }

  openCreateDialog(): void {
    const dialogData: CompanyPreferenceDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyPreferenceDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPreferences();
      }
    });
  }

  openEditDialog(preference: CompanyPreferenceDto): void {
    const dialogData: CompanyPreferenceDialogData = { mode: 'edit', companyId: this.companyId, preference };
    const dialogRef = this.dialog.open(CompanyPreferenceDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadPreferences();
      }
    });
  }

  deletePreference(preference: CompanyPreferenceDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyPreferences.deletePreference'),
      message: this.translate('admin.companyPreferences.confirmDelete'),
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
        this.companyPreferencesService.delete(preference.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyPreferences.preferenceDeleted'));
            this.loadPreferences();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyPreferences.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
