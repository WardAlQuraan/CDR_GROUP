import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { EventsService } from '../../../../services/events.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { EventDto } from '../../../../models/event.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { EventDialogComponent, EventDialogData } from '../event-dialog/event-dialog.component';
import { EventViewDialogComponent } from '../event-view-dialog/event-view-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { BulkUploadDialogComponent, BulkUploadDialogData } from '../../../../shared/components/bulk-upload-dialog/bulk-upload-dialog.component';
import { Permissions } from '../../../../models/auth.model';

@Component({
  selector: 'app-events-component',
  standalone: false,
  templateUrl: './events-component.html',
  styleUrl: './events-component.scss',
})
export class EventsComponent implements OnInit {
  events: EventDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<EventDto>;

  constructor(
    private eventsService: EventsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.initGridConfig();
    this.loadEvents();
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initGridConfig(): void {
    this.gridConfig = {
      // Header
      title: 'admin.eventsAdmin.title',
      subtitle: 'admin.eventsAdmin.subtitle',
      addButton: {
        label: 'admin.eventsAdmin.addEvent',
        icon: 'add',
        color: 'primary',
        permission: Permissions.EVENTS_CREATE
      },

      // Filters
      showSearch: true,
      searchPlaceholder: this.translate('admin.eventsAdmin.searchPlaceholder'),
      columns: [
        {
          key: 'title',
          header: 'admin.eventsAdmin.title',
          sortable: true,
          cell: (row) => this.isArabic ? row.titleAr : row.titleEn
        },
        {
          key: 'company',
          header: 'admin.eventsAdmin.company',
          cell: (row) => {
            if (!row.companyId) return '-';
            return (this.isArabic ? row.companyNameAr : row.companyNameEn) || '-';
          }
        },
        {
          key: 'eventUrl',
          header: 'admin.eventsAdmin.eventUrl',
          cell: (row) => row.eventUrl || '-'
        },
        {
          key: 'eventDate',
          header: 'admin.eventsAdmin.eventDate',
          type: 'date',
          dateFormat: 'dd/MM/yyyy',
          sortable: true
        }
      ],
      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.eventsAdmin.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'edit',
          tooltip: 'admin.eventsAdmin.edit',
          permission: Permissions.EVENTS_UPDATE,
          color: 'info',
          onClick: (row) => this.openEditDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.eventsAdmin.delete',
          permission: Permissions.EVENTS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteEvent(row)
        },
        {
          icon: 'cloud_upload',
          tooltip: 'common.bulkUpload',
          permission: Permissions.EVENTS_UPDATE,
          color: 'primary',
          onClick: (row) => this.openBulkUploadDialog(row)
        }
      ],
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  loadEvents(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: ['titleEn', 'titleAr'],
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.eventsService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.events = response.data.data;
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
    this.loadEvents();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadEvents();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadEvents();
  }

  openCreateDialog(): void {
    const dialogData: EventDialogData = { mode: 'create' };
    const dialogRef = this.dialog.open(EventDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEvents();
      }
    });
  }

  openEditDialog(event: EventDto): void {
    const dialogData: EventDialogData = { mode: 'edit', event };
    const dialogRef = this.dialog.open(EventDialogComponent, {
      width: '700px',
      data: dialogData,
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadEvents();
      }
    });
  }

  openViewDialog(event: EventDto): void {
    this.dialog.open(EventViewDialogComponent, {
      width: '700px',
      data: event
    });
  }

  openBulkUploadDialog(event: EventDto): void {
    const dialogData: BulkUploadDialogData = {
      title: 'common.bulkUpload',
      entityId: event.id,
      entityType: 'Event',
      acceptedTypes: 'image/*'
    };

    this.dialog.open(BulkUploadDialogComponent, {
      width: '550px',
      data: dialogData,
    });
  }

  deleteEvent(event: EventDto): void {
    const isArabic = this.translationService.language() === 'ar';
    const eventTitle = isArabic ? event.titleAr : event.titleEn;

    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.eventsAdmin.deleteEvent'),
      message: this.translate('admin.eventsAdmin.confirmDelete'),
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
        this.eventsService.delete(event.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.eventsAdmin.eventDeleted'));
            this.loadEvents();
          },
          error: (error) => {
            this.cdr.markForCheck();
            this.loading = false;
          }
        });
      }
    });
  }
}
