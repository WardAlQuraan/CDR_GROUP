import { Component, Input, Output, EventEmitter, forwardRef, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-date-picker',
  standalone: false,
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.scss',
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatePickerComponent),
      multi: true
    }
  ]
})
export class DatePickerComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() icon = 'event';
  @Input() placeholder = '';
  @Input() minDate: Date | null = null;
  @Input() maxDate: Date | null = null;
  @Input() startView: 'month' | 'year' | 'multi-year' = 'month';

  @Output() dateChange = new EventEmitter<Date | null>();

  value: Date | null = null;
  disabled = false;

  private onChange: (value: Date | null) => void = () => {};
  private onTouched: () => void = () => {};

  onDateChange(date: Date | null): void {
    this.value = date;
    this.onChange(date);
    this.onTouched();
    this.dateChange.emit(date);
  }

  // ControlValueAccessor implementation
  writeValue(value: Date | null): void {
    this.value = value;
  }

  registerOnChange(fn: (value: Date | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
