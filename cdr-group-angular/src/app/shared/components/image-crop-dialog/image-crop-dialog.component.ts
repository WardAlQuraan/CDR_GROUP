import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ImageCroppedEvent, ImageCropperComponent, OutputFormat } from 'ngx-image-cropper';
import { TranslatePipe } from '../../../pipes/translate.pipe';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MatSliderModule } from '@angular/material/slider';

export interface ImageCropDialogData {
  imageFile: File;
  roundCropper?: boolean;
  aspectRatio?: number;
  format?: OutputFormat;
  title?: string;
}

@Component({
  selector: 'app-image-crop-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ImageCropperComponent,
    TranslatePipe,
    MatButtonModule,
    MatIconModule,
    MatSliderModule
  ],
  templateUrl: './image-crop-dialog.component.html',
  styleUrl: './image-crop-dialog.component.scss'
})
export class ImageCropDialogComponent {
  imageFile: File;
  roundCropper: boolean;
  aspectRatio: number;
  format: OutputFormat;
  title: string;
  croppedBlob: Blob | null = null;
  imageLoaded = false;

  constructor(
    private dialogRef: MatDialogRef<ImageCropDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: ImageCropDialogData
  ) {
    this.imageFile = data.imageFile;
    this.roundCropper = data.roundCropper ?? true;
    this.aspectRatio = data.aspectRatio ?? 1;
    this.format = data.format ?? 'png';
    this.title = data.title ?? 'common.cropImage';
  }

  onImageCropped(event: ImageCroppedEvent): void {
    this.croppedBlob = event.blob ?? null;
  }

  onImageLoaded(): void {
    this.imageLoaded = true;
  }

  onConfirm(): void {
    if (this.croppedBlob) {
      const extension = this.format === 'jpeg' ? 'jpg' : this.format;
      const croppedFile = new File(
        [this.croppedBlob],
        `cropped-avatar.${extension}`,
        { type: `image/${this.format}` }
      );
      this.dialogRef.close(croppedFile);
    }
  }

  onCancel(): void {
    this.dialogRef.close(null);
  }
}
