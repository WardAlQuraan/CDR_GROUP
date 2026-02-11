import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { ContactUsService } from '../../../../services/contact-us.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { ContactUsDto } from '../../../../models/contact-us.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { ConfirmDialogComponent, ConfirmDialogData } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { ContactUsViewDialogComponent } from '../contact-us-view-dialog/contact-us-view-dialog.component';
import { Permissions } from '../../../../models/auth.model';

@Component({
  selector: 'app-contact-us-component',
  standalone: false,
  templateUrl: './contact-us-component.html',
  styleUrl: './contact-us-component.scss',
})
export class ContactUsAdminComponent implements OnInit {
  messages: ContactUsDto[] = [];
  totalCount = 0;
  loading = false;

  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = false;

  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<ContactUsDto>;

  constructor(
    private contactUsService: ContactUsService,
    private snackbar: SnackbarService,
    private dialog: MatDialog,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initGridConfig();
    this.loadMessages();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.contactUs.title',
      subtitle: 'admin.contactUs.subtitle',

      showSearch: true,
      searchPlaceholder: this.translate('admin.contactUs.searchPlaceholder'),

      columns: [
        { key: 'fullName', header: 'admin.contactUs.fullName', sortable: true },
        { key: 'email', header: 'admin.contactUs.email', sortable: true },
        {
          key: 'message',
          header: 'admin.contactUs.message',
          cell: (row) => row.message.length > 80 ? row.message.substring(0, 80) + '...' : row.message
        },
        {
          key: 'createdAt',
          header: 'admin.contactUs.date',
          type: 'date',
          sortable: true,
          dateFormat: 'medium'
        }
      ],

      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.contactUs.view',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        },
        {
          icon: 'delete',
          tooltip: 'admin.contactUs.delete',
          permission: Permissions.CONTACTUS_DELETE,
          color: 'warn',
          onClick: (row) => this.deleteMessage(row)
        }
      ],

      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  loadMessages(): void {
    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      searchTerm: this.filterValues['searchTerm'] || undefined,
      searchProperties: ['fullName', 'email', 'message'],
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.contactUsService.getPaged(request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.messages = response.data.data;
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
    this.loadMessages();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadMessages();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadMessages();
  }

  openViewDialog(message: ContactUsDto): void {
    this.dialog.open(ContactUsViewDialogComponent, {
      width: '600px',
      data: message
    });
  }

  deleteMessage(message: ContactUsDto): void {
    const dialogData: ConfirmDialogData = {
      title: this.translate('admin.contactUs.deleteMessage'),
      message: this.translate('admin.contactUs.confirmDelete'),
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
        this.contactUsService.delete(message.id).subscribe({
          next: () => {
            this.snackbar.success(this.translate('admin.contactUs.messageDeleted'));
            this.loadMessages();
          },
          error: (error) => {
            this.loading = false;
            this.cdr.markForCheck();
          }
        });
      }
    });
  }
}
