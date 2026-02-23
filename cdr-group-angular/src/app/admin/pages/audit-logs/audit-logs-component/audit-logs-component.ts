import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { AuditLogsService } from '../../../../services/audit-logs.service';
import { AuditLogDto } from '../../../../models/audit-log.model';
import { PagedRequest } from '../../../../models/paged.model';
import { DataGridConfig, FilterValues } from '../../../../shared/components/data-grid/data-grid.models';
import { AuditLogViewDialogComponent } from '../audit-log-view-dialog/audit-log-view-dialog.component';

@Component({
  selector: 'app-audit-logs-component',
  standalone: false,
  templateUrl: './audit-logs-component.html',
  styleUrl: './audit-logs-component.scss',
})
export class AuditLogsComponent implements OnInit {
  auditLogs: AuditLogDto[] = [];
  totalCount = 0;
  loading = false;

  // Pagination & Sorting
  pageNumber = 1;
  pageSize = 10;
  sortBy?: string;
  sortDescending = true;

  // Entity context
  entityName = '';
  entityId = '';

  // Filters
  filterValues: FilterValues = {};

  gridConfig!: DataGridConfig<AuditLogDto>;

  constructor(
    private auditLogsService: AuditLogsService,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const entityName = params.get('entityName');
      const entityId = params.get('entityId');
      if (entityName && entityId) {
        this.entityName = entityName;
        this.entityId = entityId;
        this.initGridConfig();
        this.loadAuditLogs();
      }
    });
  }

  private initGridConfig(): void {
    this.gridConfig = {
      title: 'admin.auditLogs.title',
      subtitle: 'admin.auditLogs.subtitle',
      showSearch: false,

      columns: [
        {
          key: 'actionType',
          header: 'admin.auditLogs.actionType',
          sortable: true,
          type: 'badge',
          badge: (row) => {
            const colorMap: Record<string, 'success' | 'info' | 'warn' | 'primary'> = {
              'Create': 'success',
              'Insert': 'success',
              'Update': 'info',
              'Delete': 'warn'
            };
            return {
              text: `admin.auditLogs.actions.${row.actionType.toLowerCase()}`,
              color: colorMap[row.actionType] || 'primary'
            };
          }
        },
        {
          key: 'performedBy',
          header: 'admin.auditLogs.performedBy',
          sortable: true,
          cell: (row) => row.performedBy || '-'
        },
        {
          key: 'timestamp',
          header: 'admin.auditLogs.timestamp',
          sortable: true,
          type: 'date',
          dateFormat: 'medium'
        }
      ],
      actions: [
        {
          icon: 'visibility',
          tooltip: 'admin.auditLogs.viewDetails',
          color: 'info',
          onClick: (row) => this.openViewDialog(row)
        }
      ],
      serverSide: true,
      pageSizeOptions: [5, 10, 25, 50],
      defaultPageSize: 10
    };
  }

  loadAuditLogs(): void {
    if (!this.entityName || !this.entityId) return;

    this.loading = true;
    const request: PagedRequest = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      sortBy: this.sortBy,
      sortDescending: this.sortDescending
    };

    this.auditLogsService.getPaged(this.entityName, this.entityId, request).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.auditLogs = response.data.data;
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
    this.loadAuditLogs();
  }

  onSortChange(sort: Sort): void {
    this.sortBy = sort.active;
    this.sortDescending = sort.direction === 'desc';
    this.loadAuditLogs();
  }

  onFilterChange(filters: FilterValues): void {
    this.filterValues = filters;
    this.pageNumber = 1;
    this.loadAuditLogs();
  }

  openViewDialog(log: AuditLogDto): void {
    this.dialog.open(AuditLogViewDialogComponent, {
      width: '700px',
      data: log
    });
  }
}
