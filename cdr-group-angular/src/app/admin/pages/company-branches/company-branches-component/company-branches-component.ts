import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyBranchesService } from '../../../../services/company-branches.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyBranchDto, CompanyBranchPagedRequest } from '../../../../models/company-branch.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { CompanyBranchDialogComponent, CompanyBranchDialogData } from '../company-branch-dialog/company-branch-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-company-branches-component',
  standalone: false,
  templateUrl: './company-branches-component.html',
  styleUrl: './company-branches-component.scss',
})
export class CompanyBranchesComponent implements OnInit {
  branches: CompanyBranchDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['nameEn', 'nameAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyBranchDto>;

  constructor(
    private companyBranchesService: CompanyBranchesService,
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
    this.loadBranches();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyBranches.title',
      subtitle: 'admin.companyBranches.subtitle',
      addButton: {
        label: 'admin.companyBranches.addBranch',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_BRANCHES_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyBranches'),

      columns: [
        {
          key: 'name',
          header: 'admin.companyBranches.nameEn',
          sortable: true,
          sortBy: this.isArabic ? 'nameAr' : 'nameEn',
          cell: (row) => this.isArabic ? row.nameAr : row.nameEn
        },
        {
          key: 'city',
          header: 'admin.companyBranches.city',
          cell: (row) => this.isArabic ? row.cityNameAr : row.cityNameEn
        },
        {
          key: 'company',
          header: 'admin.companyBranches.company',
          cell: (row) => this.isArabic ? row.companyNameAr : row.companyNameEn
        },
        {
          key: 'openingDate',
          header: 'admin.companyBranches.openingDate',
          type: 'date',
          sortable: true
        },
        {
          key: 'createdAt',
          header: 'admin.companyBranches.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyBranches.edit',
          permission: Permissions.COMPANY_BRANCHES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyBranch', row.id])
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyBranches.delete',
          permission: Permissions.COMPANY_BRANCHES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteBranch(row)
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

  loadBranches(): void {
    this.loading = true;
    const request: CompanyBranchPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyBranchesService.getPagedWithFilters(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.branches = response.data.data;
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
    this.loadBranches();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadBranches();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadBranches();
  }

  openCreateDialog(): void {
    const dialogData: CompanyBranchDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyBranchDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBranches();
      }
    });
  }

  openEditDialog(branch: CompanyBranchDto): void {
    const dialogData: CompanyBranchDialogData = { mode: 'edit', companyId: this.companyId, branch };
    const dialogRef = this.dialog.open(CompanyBranchDialogComponent, {
      width: '600px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadBranches();
      }
    });
  }

  exportToExcel(): void {
    this.companyBranchesService.export().subscribe(blob => downloadExcelBlob(blob, 'CompanyBranches'));
  }

  deleteBranch(branch: CompanyBranchDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyBranches.deleteBranch'),
      message: this.translate('admin.companyBranches.confirmDelete'),
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
        this.companyBranchesService.delete(branch.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyBranches.branchDeleted'));
            this.loadBranches();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyBranches.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
