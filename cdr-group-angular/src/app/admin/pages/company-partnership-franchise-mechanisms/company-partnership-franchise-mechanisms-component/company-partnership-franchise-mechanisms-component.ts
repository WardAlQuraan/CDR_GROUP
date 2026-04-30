import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyPartnershipFranchiseMechanismsService } from '../../../../services/company-partnership-franchise-mechanisms.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyPartnershipFranchiseMechanismDto,
  CompanyPartnershipFranchiseMechanismPagedRequest
} from '../../../../models/company-partnership-franchise-mechanism.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyPartnershipFranchiseMechanismDialogComponent,
  CompanyPartnershipFranchiseMechanismDialogData
} from '../company-partnership-franchise-mechanism-dialog/company-partnership-franchise-mechanism-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-partnership-franchise-mechanisms-component',
  standalone: false,
  templateUrl: './company-partnership-franchise-mechanisms-component.html',
  styleUrl: './company-partnership-franchise-mechanisms-component.scss',
})
export class CompanyPartnershipFranchiseMechanismsComponent implements OnInit {
  mechanisms: CompanyPartnershipFranchiseMechanismDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyPartnershipFranchiseMechanismDto>;

  constructor(
    private companyPartnershipFranchiseMechanismsService: CompanyPartnershipFranchiseMechanismsService,
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
    this.loadMechanisms();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyPartnershipFranchiseMechanisms.title',
      subtitle: 'admin.companyPartnershipFranchiseMechanisms.subtitle',
      addButton: {
        label: 'admin.companyPartnershipFranchiseMechanisms.addMechanism',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyPartnershipFranchiseMechanisms'),

      columns: [
        {
          key: 'description',
          header: 'admin.companyPartnershipFranchiseMechanisms.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyPartnershipFranchiseMechanisms.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyPartnershipFranchiseMechanisms.edit',
          permission: Permissions.COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyPartnershipFranchiseMechanisms.delete',
          permission: Permissions.COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteMechanism(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyPartnershipFranchiseMechanism', row.id])
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

  loadMechanisms(): void {
    this.loading = true;
    const request: CompanyPartnershipFranchiseMechanismPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyPartnershipFranchiseMechanismsService.getCompanyPartnershipFranchiseMechanismsPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.mechanisms = response.data.data;
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
    this.loadMechanisms();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadMechanisms();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadMechanisms();
  }

  openCreateDialog(): void {
    const dialogData: CompanyPartnershipFranchiseMechanismDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyPartnershipFranchiseMechanismDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadMechanisms();
      }
    });
  }

  openEditDialog(mechanism: CompanyPartnershipFranchiseMechanismDto): void {
    const dialogData: CompanyPartnershipFranchiseMechanismDialogData = { mode: 'edit', companyId: this.companyId, mechanism };
    const dialogRef = this.dialog.open(CompanyPartnershipFranchiseMechanismDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadMechanisms();
      }
    });
  }

  deleteMechanism(mechanism: CompanyPartnershipFranchiseMechanismDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyPartnershipFranchiseMechanisms.deleteMechanism'),
      message: this.translate('admin.companyPartnershipFranchiseMechanisms.confirmDelete'),
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
        this.companyPartnershipFranchiseMechanismsService.delete(mechanism.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyPartnershipFranchiseMechanisms.mechanismDeleted'));
            this.loadMechanisms();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyPartnershipFranchiseMechanisms.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
