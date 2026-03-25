import { Component, Input, Optional, Self, ChangeDetectorRef, ViewEncapsulation, inject } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { TranslationService } from '../../../services/translation.service';

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
  @Input() type: 'text' | 'number' | 'email' | 'password' | 'textarea' = 'text';
  @Input() dir = '';
  @Input() rows = 3;

  private translationService = inject(TranslationService);

  get effectiveDir(): string {
    return this.dir || (this.translationService.language() === 'ar' ? 'rtl' : 'ltr');
  }

  get isArabicUi(): boolean {
    return this.translationService.language() === 'ar';
  }

  get inputDirClass(): string {
    const textDir = this.effectiveDir;
    const uiDir = this.isArabicUi ? 'rtl' : 'ltr';
    const classes: string[] = [];
    classes.push(textDir === 'rtl' ? 'input-rtl' : 'input-ltr');
    if (textDir === 'rtl' && uiDir === 'ltr') classes.push('placeholder-ltr');
    if (textDir === 'ltr' && uiDir === 'rtl') classes.push('placeholder-rtl');
    return classes.join(' ');
  }

  @Input() errors: FieldError[] = [];
  @Input() required = false;

  value: any = '';
  disabled = false;
  hidePassword = true;

  get isRequired(): boolean {
    if (this.required) return true;
    const control = this.ngControl?.control;
    if (!control || !control.validator) return false;
    const result = control.validator({} as any);
    return result?.['required'] === true;
  }

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
