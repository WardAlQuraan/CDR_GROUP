import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface ConfirmDialogData {
  title: string;
  message: string;
  confirmLabel?: string;
  cancelLabel?: string;
  confirmColor?: 'primary' | 'accent' | 'warn';
  icon?: string;
}

@Component({
  selector: 'app-confirm-dialog',
  standalone: false,
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss'
})
export class ConfirmDialogComponent {
  title: string;
  message: string;
  confirmLabel: string;
  cancelLabel: string;
  confirmColor: 'primary' | 'accent' | 'warn';
  icon: string;

  constructor(
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: ConfirmDialogData
  ) {
    this.title = data.title;
    this.message = data.message;
    this.confirmLabel = data.confirmLabel || 'common.confirm';
    this.cancelLabel = data.cancelLabel || 'common.cancel';
    this.confirmColor = data.confirmColor || 'warn';
    this.icon = data.icon || 'warning';
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
