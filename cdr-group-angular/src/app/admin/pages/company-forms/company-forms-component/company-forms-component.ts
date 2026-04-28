import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyFormsService } from '../../../../services/company-forms.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyFormDto, CompanyFormPagedRequest } from '../../../../models/company-form.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyFormDialogComponent, CompanyFormDialogData } from '../company-form-dialog/company-form-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-company-forms-component',
  standalone: false,
  templateUrl: './company-forms-component.html',
  styleUrl: './company-forms-component.scss',
})
export class CompanyFormsComponent implements OnInit {
  forms: CompanyFormDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['formNameEn', 'formNameAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyFormDto>;

  constructor(
    private companyFormsService: CompanyFormsService,
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
    this.loadForms();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyForms.title',
      subtitle: 'admin.companyForms.subtitle',
      addButton: {
        label: 'admin.companyForms.addForm',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_FORMS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyForms'),

      columns: [
        {
          key: 'name',
          header: 'admin.companyForms.name',
          sortable: true,
          sortBy: this.isArabic ? 'formNameAr' : 'formNameEn',
          cell: (row) => this.isArabic ? row.formNameAr : row.formNameEn
        },
        {
          key: 'formUrl',
          header: 'admin.companyForms.formUrl',
          cell: (row) => row.formUrl || '-'
        },
        {
          key: 'company',
          header: 'admin.companyForms.company',
          cell: (row) => this.isArabic ? row.companyNameAr : row.companyNameEn
        },
        {
          key: 'createdAt',
          header: 'admin.companyForms.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyForms.edit',
          permission: Permissions.COMPANY_FORMS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyForms.delete',
          permission: Permissions.COMPANY_FORMS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteForm(row)
        },
        {
          icon: 'open_in_new',
          tooltip: 'admin.companyForms.open',
          color: 'primary',
          primary: false,
          onClick: (row) => {
            if (row.formUrl) {
              window.open(row.formUrl, '_blank');
            }
          }
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyForm', row.id])
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

  loadForms(): void {
    this.loading = true;
    const request: CompanyFormPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyFormsService.getCompanyFormsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.forms = response.data.data;
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
    this.loadForms();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadForms();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadForms();
  }

  openCreateDialog(): void {
    const dialogData: CompanyFormDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyFormDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadForms();
      }
    });
  }

  openEditDialog(form: CompanyFormDto): void {
    const dialogData: CompanyFormDialogData = { mode: 'edit', companyId: this.companyId, form };
    const dialogRef = this.dialog.open(CompanyFormDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadForms();
      }
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.companyFormsService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'CompanyForms');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteForm(form: CompanyFormDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyForms.deleteForm'),
      message: this.translate('admin.companyForms.confirmDelete'),
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
        this.companyFormsService.delete(form.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyForms.formDeleted'));
            this.loadForms();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyForms.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
