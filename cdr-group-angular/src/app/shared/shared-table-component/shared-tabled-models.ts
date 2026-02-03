export interface TableColumn<T = any> {
  key:  string;
  header: string;
  sortable?: boolean;
  width?: string;
  type?: 'text' | 'date' | 'icon' | 'custom';
  cell?: (row: T) => string;
  permission?: string; // optional
}

export interface TableAction<T = any> {
  icon: string;
  tooltip: string;
  permission?: string;
  onClick: (row: T) => void;
}

export interface TableConfig<T = any> {
  columns: TableColumn<T>[];
  actions?: TableAction<T>[];
  selectable?: boolean;
  serverSide?: boolean;
}
