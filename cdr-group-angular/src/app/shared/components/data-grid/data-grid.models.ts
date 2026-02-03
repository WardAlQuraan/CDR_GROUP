export type FilterType = 'text' | 'select' | 'date' | 'dateRange' | 'checkbox';

export interface FilterOption {
  value: string | number | boolean;
  label: string;
}

export interface FilterConfig {
  key: string;
  label: string;
  type: FilterType;
  placeholder?: string;
  options?: FilterOption[];  // For select type
  icon?: string;             // Optional icon suffix
  defaultValue?: any;
  width?: string;            // CSS width (e.g., '200px', '25%')
}

export interface FilterValues {
  [key: string]: any;
}

export interface HeaderAction {
  label: string;
  icon?: string;
  color?: 'primary' | 'accent' | 'warn';
  permission?: string;
}

export interface DataGridConfig<T = any> {
  // Header
  title?: string;
  subtitle?: string;
  addButton?: HeaderAction;

  // Filters
  filters?: FilterConfig[];
  showSearch?: boolean;
  searchPlaceholder?: string;

  // Table
  columns: GridColumn<T>[];
  actions?: GridAction<T>[];
  selectable?: boolean;
  serverSide?: boolean;

  // Pagination
  pageSizeOptions?: number[];
  defaultPageSize?: number;
}

export interface BadgeConfig {
  text: string;
  color: 'primary' | 'accent' | 'warn' | 'success' | 'info';
}

export interface GridColumn<T = any> {
  key: string;
  header: string;
  sortable?: boolean;
  width?: string;
  type?: 'text' | 'date' | 'icon' | 'badge' | 'custom';
  dateFormat?: string;
  cell?: (row: T) => string;
  badge?: (row: T) => BadgeConfig;
  permission?: string;
}

export interface GridAction<T = any> {
  icon: string;
  tooltip: string;
  permission?: string;
  color?: 'primary' | 'accent' | 'warn' | 'success' | 'info';
  onClick: (row: T) => void;
  visible?: (row: T) => boolean;
}
