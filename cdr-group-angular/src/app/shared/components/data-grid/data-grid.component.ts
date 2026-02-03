import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DataGridConfig, FilterValues } from './data-grid.models';

@Component({
  selector: 'app-data-grid',
  standalone: false,
  templateUrl: './data-grid.component.html',
  styleUrl: './data-grid.component.scss'
})
export class DataGridComponent<T> implements OnChanges {

  @Input() data: T[] = [];
  @Input() config!: DataGridConfig<T>;
  @Input() totalCount = 0;
  @Input() loading = false;
  @Input() pageSize = 10;
  @Input() pageIndex = 0;

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<Sort>();
  @Output() filterChange = new EventEmitter<FilterValues>();
  @Output() searchChange = new EventEmitter<string>();
  @Output() addClick = new EventEmitter<void>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<T>();
  filterValues: FilterValues = {};
  searchTerm = '';

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.dataSource.data = [...this.data];
      this.cdr.detectChanges();
    }

    if (changes['config'] && this.config) {
      this.initColumns();
      this.initFilterDefaults();
    }
  }

  private initColumns(): void {
    this.displayedColumns = [
      ...(this.config.selectable ? ['select'] : []),
      ...this.config.columns.map(c => c.key),
      ...(this.config.actions?.length ? ['actions'] : [])
    ];
  }

  private initFilterDefaults(): void {
    if (this.config.filters) {
      this.config.filters.forEach(filter => {
        if (filter.defaultValue !== undefined) {
          this.filterValues[filter.key] = filter.defaultValue;
        }
      });
    }
  }

  onFilterChange(key: string, value: any): void {
    this.filterValues[key] = value;
  }

  applyFilters(): void {
    this.filterChange.emit({ ...this.filterValues, search: this.searchTerm });
  }

  onSearch(): void {
    this.searchChange.emit(this.searchTerm);
    this.applyFilters();
  }

  onSearchKeyup(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.onSearch();
    }
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.filterValues = {};
    this.initFilterDefaults();
    this.filterChange.emit({ ...this.filterValues, search: '' });
  }

  onPageChange(event: PageEvent): void {
    this.pageChange.emit(event);
  }

  onSortChange(sortState: Sort): void {
    this.sortChange.emit(sortState);
  }

  isActionVisible(action: any, row: T): boolean {
    return action.visible ? action.visible(row) : true;
  }
}
