import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import {
  CompanyHomeComponentSetupDto,
  CompanyHomeComponentSetupPagedRequest,
} from '../../../../models/company-home-component-setup.model';
import { CompanyHomeComponentSetupsService } from '../../../../services/company-home-component-setups.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { Permissions } from '../../../../models/auth.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import {
  CompanyHomeComponentSetupDialogComponent,
  CompanyHomeComponentSetupDialogData,
} from '../company-home-component-setup-dialog/company-home-component-setup-dialog.component';
import {
  ConfirmDialogComponent,
  ConfirmDialogData,
} from '../../../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-company-home-component-setups-component',
  standalone: false,
  templateUrl: './company-home-component-setups-component.html',
  styleUrl: './company-home-component-setups-component.scss',
})
export class CompanyHomeComponentSetupsComponent implements OnInit {
  setups: CompanyHomeComponentSetupDto[] = [];
  totalCount = 0;
  loading = false;
  companyId!: string;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['componentCode', 'companyTitleDescriptionCode', 'preferenceTitleCode'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<CompanyHomeComponentSetupDto>;

  constructor(
    private companyHomeComponentSetupsService: CompanyHomeComponentSetupsService,
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
    this.loadSetups();
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.companyHomeComponentSetups.title',
      subtitle: 'admin.companyHomeComponentSetups.subtitle',
      addButton: {
        label: 'admin.companyHomeComponentSetups.addSetup',
        icon: 'add',
        color: 'primary',
        permission: Permissions.COMPANY_HOME_COMPONENT_SETUPS_CREATE,
      },

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(
        this.translationService,
        this.searchProperties,
        'admin.companyHomeComponentSetups'
      ),

      columns: [
        {
          key: 'componentCode',
          header: 'admin.companyHomeComponentSetups.componentCode',
          sortable: true,
          cell: (row) => row.componentCode || '-',
        },
        {
          key: 'companyTitleDescriptionCode',
          header: 'admin.companyHomeComponentSetups.companyTitleDescriptionCode',
          cell: (row) => row.companyTitleDescriptionCode || '-',
        },
        {
          key: 'preferenceTitleCode',
          header: 'admin.companyHomeComponentSetups.preferenceTitleCode',
          cell: (row) => row.preferenceTitleCode || '-',
        },
        {
          key: 'preferenceDescriptionCode',
          header: 'admin.companyHomeComponentSetups.preferenceDescriptionCode',
          cell: (row) => row.preferenceDescriptionCode || '-',
        },
        {
          key: 'rank',
          header: 'admin.companyHomeComponentSetups.rank',
          sortable: true,
          cell: (row) => row.rank?.toString() ?? '0',
        },
        {
          key: 'createdAt',
          header: 'admin.companyHomeComponentSetups.created',
          type: 'date',
          sortable: true,
        },
      ],
      actions: [
        {
          icon: 'edit',
          tooltip: 'admin.companyHomeComponentSetups.edit',
          permission: Permissions.COMPANY_HOME_COMPONENT_SETUPS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row),
        },
        {
          icon: 'delete',
          tooltip: 'admin.companyHomeComponentSetups.delete',
          permission: Permissions.COMPANY_HOME_COMPONENT_SETUPS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteSetup(row),
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          primary: false,
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'CompanyHomeComponentSetup', row.id]),
        },
      ],
      showExport: false,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10,
    };
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadSetups(): void {
    this.loading = true;
    const request: CompanyHomeComponentSetupPagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending,
      companyId: this.companyId,
    };

    this.companyHomeComponentSetupsService.getCompanyHomeComponentSetups(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.setups = response.data.data;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }

  onPageChange(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadSetups();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadSetups();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadSetups();
  }

  openCreateDialog(): void {
    const dialogData: CompanyHomeComponentSetupDialogData = { mode: 'create', companyId: this.companyId };
    const dialogRef = this.dialog.open(CompanyHomeComponentSetupDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadSetups();
      }
    });
  }

  openEditDialog(setup: CompanyHomeComponentSetupDto): void {
    const dialogData: CompanyHomeComponentSetupDialogData = {
      mode: 'edit',
      companyId: this.companyId,
      setup,
    };
    const dialogRef = this.dialog.open(CompanyHomeComponentSetupDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadSetups();
      }
    });
  }

  deleteSetup(setup: CompanyHomeComponentSetupDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.companyHomeComponentSetups.deleteSetup'),
      message: this.translate('admin.companyHomeComponentSetups.confirmDelete'),
      confirmLabel: this.translate('common.delete'),
      cancelLabel: this.translate('common.cancel'),
      confirmColor: 'warn',
      icon: 'delete',
    };

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.loading = true;
        this.companyHomeComponentSetupsService.delete(setup.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.companyHomeComponentSetups.setupDeleted'));
            this.loadSetups();
          },
          error: (error) => {
            this.snackbar.error(
              error.message || this.translate('admin.companyHomeComponentSetups.errors.deleteFailed')
            );
            this.loading = false;
          },
        });
      }
    });
  }
}
