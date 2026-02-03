import { Component, Input, Output, EventEmitter, OnInit, OnDestroy, OnChanges, SimpleChanges, forwardRef, ChangeDetectorRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, Subject, takeUntil } from 'rxjs';
import { ApiResponse } from '../../../models/api-response.model';

export interface SelectOption {
  value: any;
  label: string;
  disabled?: boolean;
}

export type OptionMapper<T> = (item: T) => SelectOption;

@Component({
  selector: 'app-async-select',
  standalone: false,
  templateUrl: './async-select.component.html',
  styleUrl: './async-select.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AsyncSelectComponent),
      multi: true
    }
  ]
})
export class AsyncSelectComponent<T> implements OnInit, OnDestroy, OnChanges, ControlValueAccessor {
  @Input() label = '';
  @Input() placeholder = '';
  @Input() icon = '';
  @Input() nullOptionLabel = '';
  @Input() showNullOption = true;
  @Input() loadingHint = 'common.loading';
  @Input() dataSource$!: Observable<ApiResponse<T[]>>;
  @Input() optionMapper!: OptionMapper<T>;
  @Input() filterFn?: (item: T) => boolean;
  @Input() resetOnDataSourceChange = true;

  @Output() selectionChange = new EventEmitter<any>();

  options: SelectOption[] = [];
  loading = false;
  value: any = null;
  disabled = false;

  private destroy$ = new Subject<void>();
  private onChange: (value: any) => void = () => {};
  private onTouched: () => void = () => {};
  private initialized = false;

  constructor(private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadData();
    this.initialized = true;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['dataSource$'] && !changes['dataSource$'].firstChange && this.initialized) {
      if (this.resetOnDataSourceChange) {
        this.value = null;
        this.onChange(null);
      }
      this.loadData();
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadData(): void {
    if (!this.dataSource$ || !this.optionMapper) {
      return;
    }

    this.loading = true;
    this.dataSource$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          let items = response.data;

          // Apply filter if provided
          if (this.filterFn) {
            items = items.filter(this.filterFn);
          }

          this.options = items.map(this.optionMapper);
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

  onSelectionChange(value: any): void {
    this.value = value;
    this.onChange(value);
    this.onTouched();
    this.selectionChange.emit(value);
  }

  // ControlValueAccessor implementation
  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
