import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import { ComplaintsService } from '../../../../services/complaints.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { ComplaintDto } from '../../../../models/complaint.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { ComplaintViewDialogComponent } from '../complaint-view-dialog/complaint-view-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Permissions } from '../../../../models/auth.model';
import { buildSearchPlaceholder } from '../../../../utils/search.utils';
import { downloadExcelBlob } from '../../../../utils/export.utils';

@Component({
  selector: 'app-complaints-component',
  standalone: false,
  templateUrl: './complaints-component.html',
  styleUrl: './complaints-component.scss',
})
export class ComplaintsComponent implements OnInit {
  complaints: ComplaintDto[] = [];
  totalCount = 0;
  loading = false;
  exporting = false;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;
  searchProperties: string[] = ['fullName', 'email', 'subject', 'message'];

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<ComplaintDto>;

  constructor(
    private complaintsService: ComplaintsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadComplaints();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.complaints.title',
      subtitle: 'admin.complaints.subtitle',

      showSearch: true,
      searchPlaceholder: buildSearchPlaceholder(this.translationService, this.searchProperties, 'admin.complaints'),

      columns: [
        { key: 'fullName', header: 'admin.complaints.fullName', sortable: true },
        { key: 'email', header: 'admin.complaints.email', sortable: true },
        { key: 'subject', header: 'admin.complaints.subject', sortable: true },
        {
          key: 'message',
          header: 'admin.complaints.message',
          cell: (row) => row.message.length > 60 ? row.message.substring(0, 60) + '...' : row.message
        },
        {
          key: 'companyName',
          header: 'admin.complaints.company',
          cell: (row) => (this.isArabic ? row.companyNameAr : row.companyNameEn) || '-'
        },
        {
          key: 'createdAt',
          header: 'admin.complaints.date',
          type: 'date',
          sortable: true,
          dateFormat: 'medium'
        }
      ],

      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.complaints.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.complaints.delete',
          permission: Permissions.COMPLAINTS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteComplaint(row)
        },
        {
          icon: 'history',
          tooltip: 'admin.auditLogs.history',
          permission: Permissions.AUDIT_LOGS_READ,
          color: 'accent',
          onClick: (row) => this.router.navigate(['/admin/audit-logs', 'Complaint', row.id])
        }
      ],

      showExport: true,
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  loadComplaints(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: this.searchProperties,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.complaintsService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.complaints = response.data.data;
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
    this.loadComplaints();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadComplaints();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadComplaints();
  }

  openViewDialog(complaint: ComplaintDto): void {
    this.dialog.open(ComplaintViewDialogComponent, {
      width: '600px',
      data: complaint
    });
  }

  exportToExcel(): void {
    this.exporting = true;
    this.complaintsService.export().subscribe({
      next: (blob) => {
        downloadExcelBlob(blob, 'Complaints');
        this.exporting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.exporting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteComplaint(complaint: ComplaintDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.complaints.deleteComplaint'),
      message: this.translate('admin.complaints.confirmDelete'),
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
        this.complaintsService.delete(complaint.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.complaints.complaintDeleted'));
            this.loadComplaints();
          },
          error: () => {
            this.loading = false;
            this.cdr.markForCheck();
          }
        });
      }
    });
  }
}
