import { Component, Input, Optional, Self, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

export interface FieldError {
  name: string;
  message: string;
}

@Component({
  selector: 'app-form-field',
  standalone: false,
  templateUrl: './form-field.component.html',
  styleUrl: './form-field.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class FormFieldComponent implements ControlValueAccessor {
  @Input() label = '';
  @Input() placeholder = '';
  @Input() icon = '';
  @Input() textPrefix = '';
  @Input() type: 'text' | 'number' | 'email' | 'password' | 'textarea' | 'color' = 'text';
  @Input() dir = '';
  @Input() rows = 3;
  @Input() errors: FieldError[] = [];
  @Input() required = false;

  value: any = '';
  disabled = false;
  hidePassword = true;

  private onChange: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  constructor(
    private cdr: ChangeDetectorRef,
    @Optional() @Self() public ngControl: NgControl
  ) {
    if (this.ngControl) {
      this.ngControl.valueAccessor = this;
    }
  }

  onInput(event: Event): void {
    const target = event.target as HTMLInputElement | HTMLTextAreaElement;
    const val = this.type === 'number' ? (target.value === '' ? null : +target.value) : target.value;
    this.value = val;
    this.onChange(val);
  }

  // ControlValueAccessor implementation
  writeValue(value: any): void {
    this.value = value ?? '';
    this.cdr.markForCheck();
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
    this.cdr.markForCheck();
  }
}
