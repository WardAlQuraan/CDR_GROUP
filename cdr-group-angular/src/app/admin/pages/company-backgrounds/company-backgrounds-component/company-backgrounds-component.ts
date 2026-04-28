import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyBackgroundsService } from '../../../../services/company-backgrounds.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyBackgroundDto, CompanyBackgroundPagedRequest } from '../../../../models/company-background.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyBackgroundDialogComponent, CompanyBackgroundDialogData } from '../company-background-dialog/company-background-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { downloadExcelBlob } from '../../../../utils/export.utils';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-company-backgrounds-component',
  standalone: false,
  templateUrl: './company-backgrounds-component.html',
  styleUrl: './company-backgrounds-component.scss',
})
export class CompanyBackgroundsComponent implements OnInit {
  backgrounds: CompanyBackgroundDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyBackgroundDto>;

  constructor(
    private companyBackgroundsService: CompanyBackgroundsService,
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
    this.loadBackgrounds();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private resolveImageUrl(url?: string): string | undefined {
    if (!url) return undefined;
    if (/^https?:\/\//i.test(url)) return url;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${url.startsWith('/') ? '' : '/'}${url}`;
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyBackgrounds.title',
      subtitle: 'admin.companyBackgrounds.subtitle',
      addButton: {
        label: 'admin.companyBackgrounds.addBackground',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_BACKGROUNDS_CREATE
      },

      showSearch: false,

      columns: [
        {
          key: 'imageUrl',
          header: 'admin.companyBackgrounds.image',
          type: 'image',
          imageUrl: (row) => this.resolveImageUrl(row.imageUrl)
        },
        {
          key: 'company',
          header: 'admin.companyBackgrounds.company',
          cell: (row) => this.isArabic ? row.companyNameAr : row.companyNameEn
        },
        {
          key: 'createdAt',
          header: 'admin.companyBackgrounds.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyBackgrounds.edit',
          permission: Permissions.COMPANY_BACKGROUNDS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyBackgrounds.delete',
          permission: Permissions.COMPANY_BACKGROUNDS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteBackground(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyBackground', row.id])
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

  loadBackgrounds(): void {
    this.loading = true;
    const request: CompanyBackgroundPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyBackgroundsService.getCompanyBackgroundsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.backgrounds = response.data.data;
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
    this.loadBackgrounds();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadBackgrounds();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadBackgrounds();
  }

  openCreateDialog(): void {
    const dialogData: CompanyBackgroundDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyBackgroundDialogComponent, {
      width: '550px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBackgrounds();
      }
    });
  }

  openEditDialog(background: CompanyBackgroundDto): void {
    const dialogData: CompanyBackgroundDialogData = { mode: 'edit', companyId: this.companyId, background };
    const dialogRef = this.dialog.open(CompanyBackgroundDialogComponent, {
      width: '550px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBackgrounds();
      }
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.companyBackgroundsService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'CompanyBackgrounds');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteBackground(background: CompanyBackgroundDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyBackgrounds.deleteBackground'),
      message: this.translate('admin.companyBackgrounds.confirmDelete'),
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
        this.companyBackgroundsService.delete(background.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyBackgrounds.backgroundDeleted'));
            this.loadBackgrounds();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyBackgrounds.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
