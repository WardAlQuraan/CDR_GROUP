import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-base-dialog',
  standalone: false,
  templateUrl: './base-dialog.component.html',
  styleUrl: './base-dialog.component.scss'
})
export class BaseDialogComponent {
  @Input() title = '';
  @Input() saveLabel = 'common.save';
  @Input() cancelLabel = 'common.cancel';
  @Input() saveDisabled = false;
  @Input() loading = false;
  @Input() showActions = true;

  @Output() save = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onSave(): void {
    this.save.emit();
  }

  onCancel(): void {
    this.cancel.emit();
  }
}
