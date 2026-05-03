import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyTitleDescriptionsService } from '../../../../services/company-title-descriptions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyTitleDescriptionDto,
  CompanyTitleDescriptionPagedRequest
} from '../../../../models/company-title-description.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyTitleDescriptionDialogComponent,
  CompanyTitleDescriptionDialogData
} from '../company-title-description-dialog/company-title-description-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-title-descriptions-component',
  standalone: false,
  templateUrl: './company-title-descriptions-component.html',
  styleUrl: './company-title-descriptions-component.scss',
})
export class CompanyTitleDescriptionsComponent implements OnInit {
  items: CompanyTitleDescriptionDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['code', 'titleEn', 'titleAr', 'descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyTitleDescriptionDto>;

  constructor(
    private companyTitleDescriptionsService: CompanyTitleDescriptionsService,
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
    this.loadItems();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyTitleDescriptions.title',
      subtitle: 'admin.companyTitleDescriptions.subtitle',
      addButton: {
        label: 'admin.companyTitleDescriptions.addItem',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_TITLE_DESCRIPTIONS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyTitleDescriptions'),

      columns: [
        {
          key: 'code',
          header: 'admin.companyTitleDescriptions.code',
          sortable: true,
          cell: (row) => row.code
        },
        {
          key: 'itemTitle',
          header: 'admin.companyTitleDescriptions.itemTitle',
          cell: (row) => (this.isArabic ? row.titleAr : row.titleEn) || '-'
        },
        {
          key: 'description',
          header: 'admin.companyTitleDescriptions.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyTitleDescriptions.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyTitleDescriptions.edit',
          permission: Permissions.COMPANY_TITLE_DESCRIPTIONS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyTitleDescriptions.delete',
          permission: Permissions.COMPANY_TITLE_DESCRIPTIONS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteItem(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyTitleDescription', row.id])
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

  loadItems(): void {
    this.loading = true;
    const request: CompanyTitleDescriptionPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyTitleDescriptionsService.getCompanyTitleDescriptionsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.items = response.data.data;
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
    this.loadItems();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadItems();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadItems();
  }

  openCreateDialog(): void {
    const dialogData: CompanyTitleDescriptionDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyTitleDescriptionDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadItems();
      }
    });
  }

  openEditDialog(item: CompanyTitleDescriptionDto): void {
    const dialogData: CompanyTitleDescriptionDialogData = { mode: 'edit', companyId: this.companyId, item };
    const dialogRef = this.dialog.open(CompanyTitleDescriptionDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadItems();
      }
    });
  }

  deleteItem(item: CompanyTitleDescriptionDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyTitleDescriptions.deleteItem'),
      message: this.translate('admin.companyTitleDescriptions.confirmDelete'),
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
        this.companyTitleDescriptionsService.delete(item.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyTitleDescriptions.itemDeleted'));
            this.loadItems();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyTitleDescriptions.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
