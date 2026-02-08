import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ContactUsDto } from '../../../../models/contact-us.model';

@Component({
  selector: 'app-contact-us-view-dialog',
  standalone: false,
  templateUrl: './contact-us-view-dialog.component.html',
  styleUrl: './contact-us-view-dialog.component.scss'
})
export class ContactUsViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<ContactUsViewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public message: ContactUsDto
  ) {}

  onClose(): void {
    this.dialogRef.close();
  }
}
