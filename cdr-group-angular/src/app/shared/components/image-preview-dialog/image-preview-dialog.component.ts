import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export interface ImagePreviewData {
  imageUrl: string;
}

@Component({
  selector: 'app-image-preview-dialog',
  standalone: false,
  templateUrl: './image-preview-dialog.component.html',
  styleUrl: './image-preview-dialog.component.scss',
})
export class ImagePreviewDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<ImagePreviewDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ImagePreviewData
  ) {}

  close(): void {
    this.dialogRef.close();
  }
}
