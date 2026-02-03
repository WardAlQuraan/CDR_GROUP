import { AfterViewInit, Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { TableConfig } from './shared-tabled-models';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-shared-table-component',
  standalone: false,
  templateUrl: './shared-table-component.html',
  styleUrl: './shared-table-component.scss',
})
export class SharedTableComponent<T> implements AfterViewInit {

  @Input() data: T[] = [];
  @Input() config!: TableConfig<T>;
  @Input() totalCount = 0;

  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<Sort>();
  @Output() filterChange = new EventEmitter<string>();


  displayedColumns: string[] = [];
  dataSource = new MatTableDataSource<T>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  
  ngAfterViewInit() {
    this.displayedColumns = [
      ...(this.config.selectable ? ['select'] : []),
      ...this.config.columns.map(c => c.key as string),
      ...(this.config.actions?.length ? ['actions'] : [])
    ];

    this.dataSource.data = this.data;

    if (!this.config.serverSide) {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }


  applyFilter(value: string) {
    this.filterChange.emit(value);
    if (!this.config.serverSide) {
      this.dataSource.filter = value.trim().toLowerCase();
    }
  }
}
