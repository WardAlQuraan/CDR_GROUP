import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyContactsService } from '../../../../services/company-contacts.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyContactDto, CompanyContactPagedRequest } from '../../../../models/company-contact.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyContactDialogComponent, CompanyContactDialogData } from '../company-contact-dialog/company-contact-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-company-contacts-component',
  standalone: false,
  templateUrl: './company-contacts-component.html',
  styleUrl: './company-contacts-component.scss',
})
export class CompanyContactsComponent implements OnInit {
  contacts: CompanyContactDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;
  companyId!: string;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['name', 'value'];

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyContactDto>;

  constructor(
    private companyContactsService: CompanyContactsService,
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
    this.loadContacts();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.companyContacts.title',
      subtitle: 'admin.companyContacts.subtitle',
      addButton: {
        label: 'admin.companyContacts.addContact',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_CONTACTS_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyContacts'),

      columns: [
        {
          key: 'icon',
          header: 'admin.companyContacts.icon',
          type: 'icon'
        },
        {
          key: 'name',
          header: 'admin.companyContacts.name',
          sortable: true
        },
        {
          key: 'value',
          header: 'admin.companyContacts.value',
          sortable: true
        },
        {
          key: 'company',
          header: 'admin.companyContacts.company',
          cell: (row) => this.isArabic ? row.companyNameAr : row.companyNameEn
        },
        {
          key: 'createdAt',
          header: 'admin.companyContacts.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyContacts.edit',
          permission: Permissions.COMPANY_CONTACTS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyContact', row.id])
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyContacts.delete',
          permission: Permissions.COMPANY_CONTACTS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteContact(row)
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

  loadContacts(): void {
    this.loading = true;
    const request: CompanyContactPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyContactsService.getPagedWithCompany(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.contacts = response.data.data;
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
    this.loadContacts();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadContacts();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadContacts();
  }

  openCreateDialog(): void {
    const dialogData: CompanyContactDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyContactDialogComponent, {
      width: '500px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadContacts();
      }
    });
  }

  openEditDialog(contact: CompanyContactDto): void {
    const dialogData: CompanyContactDialogData = { mode: 'edit', companyId: this.companyId, contact };
    const dialogRef = this.dialog.open(CompanyContactDialogComponent, {
      width: '500px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadContacts();
      }
    });
  }

  exportToExcel(): void {
    this.companyContactsService.export().subscribe(blob => downloadExcelBlob(blob, 'CompanyContacts'));
  }

  deleteContact(contact: CompanyContactDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyContacts.deleteContact'),
      message: this.translate('admin.companyContacts.confirmDelete'),
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
        this.companyContactsService.delete(contact.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyContacts.contactDeleted'));
            this.loadContacts();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyContacts.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
