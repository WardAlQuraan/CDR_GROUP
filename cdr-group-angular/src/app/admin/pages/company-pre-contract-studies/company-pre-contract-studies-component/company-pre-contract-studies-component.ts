import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyPreContractStudiesService } from '../../../../services/company-pre-contract-studies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import {
  CompanyPreContractStudyDto,
  CompanyPreContractStudyPagedRequest
} from '../../../../models/company-pre-contract-study.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import {
  CompanyPreContractStudyDialogComponent,
  CompanyPreContractStudyDialogData
} from '../company-pre-contract-study-dialog/company-pre-contract-study-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';

@Component({
  selector: 'app-company-pre-contract-studies-component',
  standalone: false,
  templateUrl: './company-pre-contract-studies-component.html',
  styleUrl: './company-pre-contract-studies-component.scss',
})
export class CompanyPreContractStudiesComponent implements OnInit {
  studies: CompanyPreContractStudyDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['descriptionEn', 'descriptionAr'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyPreContractStudyDto>;

  constructor(
    private companyPreContractStudiesService: CompanyPreContractStudiesService,
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
    this.loadStudies();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyPreContractStudies.title',
      subtitle: 'admin.companyPreContractStudies.subtitle',
      addButton: {
        label: 'admin.companyPreContractStudies.addStudy',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_PRE_CONTRACT_STUDIES_CREATE
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.companyPreContractStudies'),

      columns: [
        {
          key: 'description',
          header: 'admin.companyPreContractStudies.description',
          cell: (row) => (this.isArabic ? row.descriptionAr : row.descriptionEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.companyPreContractStudies.created',
          type: 'date',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyPreContractStudies.edit',
          permission: Permissions.COMPANY_PRE_CONTRACT_STUDIES_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyPreContractStudies.delete',
          permission: Permissions.COMPANY_PRE_CONTRACT_STUDIES_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteStudy(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyPreContractStudy', row.id])
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

  loadStudies(): void {
    this.loading = true;
    const request: CompanyPreContractStudyPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId
    };

    this.companyPreContractStudiesService.getCompanyPreContractStudiesPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.studies = response.data.data;
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
    this.loadStudies();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadStudies();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadStudies();
  }

  openCreateDialog(): void {
    const dialogData: CompanyPreContractStudyDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyPreContractStudyDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadStudies();
      }
    });
  }

  openEditDialog(study: CompanyPreContractStudyDto): void {
    const dialogData: CompanyPreContractStudyDialogData = { mode: 'edit', companyId: this.companyId, study };
    const dialogRef = this.dialog.open(CompanyPreContractStudyDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadStudies();
      }
    });
  }

  deleteStudy(study: CompanyPreContractStudyDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyPreContractStudies.deleteStudy'),
      message: this.translate('admin.companyPreContractStudies.confirmDelete'),
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
        this.companyPreContractStudiesService.delete(study.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyPreContractStudies.studyDeleted'));
            this.loadStudies();
          },
          error: (error) => {
            this.snackbar.error(error.message || this.translate('admin.companyPreContractStudies.errors.deleteFailed'));
            this.loading = false;
          }
        });
      }
    });
  }
}
