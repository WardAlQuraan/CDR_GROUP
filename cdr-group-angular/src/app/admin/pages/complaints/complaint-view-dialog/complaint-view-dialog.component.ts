import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ComplaintDto } from '../../../../models/complaint.model';

@Component({
  selector: 'app-complaint-view-dialog',
  standalone: false,
  templateUrl: './complaint-view-dialog.component.html',
  styleUrl: './complaint-view-dialog.component.scss'
})
export class ComplaintViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<ComplaintViewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public complaint: ComplaintDto
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
