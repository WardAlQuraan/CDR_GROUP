import { Component, Input, forwardRef, ElementRef, ViewChild, OnDestroy, ViewEncapsulation, TemplateRef, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Overlay, OverlayRef, ConnectedPosition } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';

@Component({
  selector: 'app-time-picker',
  standalone: false,
  templateUrl: './time-picker.component.html',
  styleUrl: './time-picker.component.scss',
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TimePickerComponent),
      multi: true
    }
  ]
})
export class TimePickerComponent implements ControlValueAccessor, OnDestroy {
  @Input() label = '';
  @ViewChild('dropdownTpl') dropdownTpl!: TemplateRef<any>;
  @ViewChild('trigger', { read: ElementRef }) trigger!: ElementRef;

  value = '';
  displayValue = '';
  disabled = false;
  isOpen = false;

  selectedHour = '12';
  selectedMinute = '00';
  selectedPeriod: 'AM' | 'PM' = 'AM';

  hours: string[] = [];
  minutes: string[] = [];

  private overlayRef: OverlayRef | null = null;
  private onChange: (value: string) => void = () => {};
  private onTouched: () => void = () => {};

  constructor(
    private overlay: Overlay,
    private viewContainerRef: ViewContainerRef
  ) {
    this.hours = Array.from({ length: 12 }, (_, i) => (i + 1).toString().padStart(2, '0'));
    this.minutes = Array.from({ length: 60 }, (_, i) => i.toString().padStart(2, '0'));
  }

  toggleDropdown(): void {
    if (this.disabled) return;
    this.isOpen ? this.close() : this.open();
  }

  open(): void {
    if (this.isOpen) return;
    this.parseValue();

    const positions: ConnectedPosition[] = [
      { originX: 'start', originY: 'bottom', overlayX: 'start', overlayY: 'top' },
      { originX: 'start', originY: 'top', overlayX: 'start', overlayY: 'bottom' }
    ];

    const positionStrategy = this.overlay
      .position()
      .flexibleConnectedTo(this.trigger)
      .withPositions(positions);

    this.overlayRef = this.overlay.create({
      positionStrategy,
      hasBackdrop: true,
      backdropClass: 'cdk-overlay-transparent-backdrop',
      scrollStrategy: this.overlay.scrollStrategies.reposition()
    });

    const portal = new TemplatePortal(this.dropdownTpl, this.viewContainerRef);
    this.overlayRef.attach(portal);
    this.overlayRef.backdropClick().subscribe(() => this.close());
    this.isOpen = true;
  }

  close(): void {
    if (this.overlayRef) {
      this.overlayRef.dispose();
      this.overlayRef = null;
    }
    this.isOpen = false;
  }

  selectHour(h: string): void {
    this.selectedHour = h;
  }

  selectMinute(m: string): void {
    this.selectedMinute = m;
  }

  selectPeriod(p: 'AM' | 'PM'): void {
    this.selectedPeriod = p;
  }

  confirm(): void {
    // Convert 12h to 24h for stored value
    let hour24 = parseInt(this.selectedHour, 10);
    if (this.selectedPeriod === 'AM' && hour24 === 12) {
      hour24 = 0;
    } else if (this.selectedPeriod === 'PM' && hour24 !== 12) {
      hour24 += 12;
    }
    this.value = `${hour24.toString().padStart(2, '0')}:${this.selectedMinute}`;
    this.updateDisplayValue();
    this.onChange(this.value);
    this.onTouched();
    this.close();
  }

  cancel(): void {
    this.close();
  }

  private parseValue(): void {
    if (this.value) {
      const parts = this.value.split(':');
      let hour24 = parseInt(parts[0], 10);
      this.selectedMinute = parts[1] || '00';

      // Convert 24h to 12h
      if (hour24 === 0) {
        this.selectedHour = '12';
        this.selectedPeriod = 'AM';
      } else if (hour24 < 12) {
        this.selectedHour = hour24.toString().padStart(2, '0');
        this.selectedPeriod = 'AM';
      } else if (hour24 === 12) {
        this.selectedHour = '12';
        this.selectedPeriod = 'PM';
      } else {
        this.selectedHour = (hour24 - 12).toString().padStart(2, '0');
        this.selectedPeriod = 'PM';
      }
    }
  }

  private updateDisplayValue(): void {
    if (this.value) {
      this.displayValue = `${this.selectedHour}:${this.selectedMinute} ${this.selectedPeriod}`;
    } else {
      this.displayValue = '';
    }
  }

  writeValue(value: string): void {
    this.value = value || '';
    this.parseValue();
    this.updateDisplayValue();
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnDestroy(): void {
    this.close();
  }
}
