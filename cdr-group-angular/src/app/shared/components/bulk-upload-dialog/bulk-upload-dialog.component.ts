import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FilesService } from '../../../services/files.service';
import { SnackbarService } from '../../../services/snackbar.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { BulkFileOperationItemDto, BulkFileOperationResultDto, FileAttachmentDto } from '../../../models/file-attachment.model';
import { environment } from '../../../../environments/environment';

export interface BulkUploadDialogData {
  entityId?: string;
  entityType?: string;
  title?: string;
  acceptedTypes?: string;
}

export interface FileItem {
  file: File;
  status: 'pending' | 'uploading' | 'success' | 'error';
  message?: string;
  isPrimary?: boolean;
}

@Component({
  selector: 'app-bulk-upload-dialog',
  standalone: false,
  templateUrl: './bulk-upload-dialog.component.html',
  styleUrl: './bulk-upload-dialog.component.scss'
})
export class BulkUploadDialogComponent implements OnInit {
  title: string;
  entityId?: string;
  entityType?: string;
  acceptedTypes: string;

  files: FileItem[] = [];
  existingFiles: FileAttachmentDto[] = [];
  loadingExisting = false;
  deletingFileId?: string;
  updatingPrimaryId?: string;
  uploading = false;
  uploadComplete = false;
  results: BulkFileOperationResultDto[] = [];

  constructor(
    private dialogRef: MatDialogRef<BulkUploadDialogComponent>,
    private filesService: FilesService,
    @Inject(MAT_DIALOG_DATA) data: BulkUploadDialogData,
    private cdr: ChangeDetectorRef,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {
    this.title = data.title || 'common.bulkUpload';
    this.entityId = data.entityId;
    this.entityType = data.entityType;
    this.acceptedTypes = data.acceptedTypes || '*/*';
  }

  ngOnInit(): void {
    this.loadExistingFiles();
  }

  loadExistingFiles(): void {
    if (!this.entityId || !this.entityType) return;

    this.loadingExisting = true;
    this.filesService.getByEntity(this.entityId, this.entityType).subscribe({
      next: (response) => {
        this.existingFiles = response.data || [];
        this.loadingExisting = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingExisting = false;
        this.cdr.markForCheck();
      }
    });
  }

  deleteExistingFile(file: FileAttachmentDto): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'common.confirmDelete',
        message: 'common.confirmDeleteFile',
        confirmLabel: 'common.delete',
        cancelLabel: 'common.cancel',
        confirmColor: 'warn',
        icon: 'delete'
      }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.deletingFileId = file.id;
        this.filesService.delete(file.id).subscribe({
          next: () => {
            this.existingFiles = this.existingFiles.filter(f => f.id !== file.id);
            this.deletingFileId = undefined;
            this.snackbar.success('File deleted successfully');
            this.cdr.markForCheck();
          },
          error: () => {
            this.deletingFileId = undefined;
            this.snackbar.error('Failed to delete file');
            this.cdr.markForCheck();
          }
        });
      }
    });
  }

  isViewable(file: FileAttachmentDto): boolean {
    const viewableTypes = ['image/', 'application/pdf'];
    return viewableTypes.some(type => file.contentType?.startsWith(type));
  }

  viewOrDownloadFile(file: FileAttachmentDto): void {
    if (this.isViewable(file)) {
      window.open(file.fileUrl, '_blank');
    } else {
      const link = document.createElement('a');
      link.href = file.fileUrl;
      link.download = file.fileName;
      link.click();
    }
  }

  onFilesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    for (let i = 0; i < input.files.length; i++) {
      const file = input.files[i];
      if (!this.files.some(f => f.file.name === file.name && f.file.size === file.size)) {
        this.files.push({ file, status: 'pending' });
      }
    }
    input.value = '';
  }

  removeFile(index: number): void {
    this.files.splice(index, 1);
  }

  clearAll(): void {
    this.files = [];
  }

  setAsPrimary(index: number): void {
    const isCurrentlyPrimary = this.files[index].isPrimary;
    // Clear primary from all files (new and existing)
    this.files.forEach(f => f.isPrimary = false);
    this.existingFiles.forEach(f => f.isPrimary = false);
    // Toggle: if it was primary, leave all unchecked; otherwise set this one as primary
    if (!isCurrentlyPrimary) {
      this.files[index].isPrimary = true;
    }
  }

  setExistingAsPrimary(file: FileAttachmentDto): void {
    const isCurrentlyPrimary = file.isPrimary;
    const newPrimaryValue = !isCurrentlyPrimary;

    // Clear primary from all files (new and existing)
    this.files.forEach(f => f.isPrimary = false);
    this.existingFiles.forEach(f => f.isPrimary = false);

    // Toggle: if it was primary, leave all unchecked; otherwise set this one as primary
    if (newPrimaryValue) {
      file.isPrimary = true;
    }

    // Update on server
    this.updatingPrimaryId = file.id;
    this.filesService.update(file.id, { isPrimary: newPrimaryValue }).subscribe({
      next: () => {
        this.updatingPrimaryId = undefined;
        this.snackbar.success('Primary file updated successfully');
        this.cdr.markForCheck();
      },
      error: () => {
        // Revert on error
        file.isPrimary = isCurrentlyPrimary;
        this.updatingPrimaryId = undefined;
        this.snackbar.error('Failed to update primary file');
        this.cdr.markForCheck();
      }
    });
  }

  formatFileSize(bytes: number): string {
    if (bytes === 0) return '0 B';
    const k = 1024;
    const sizes = ['B', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(1)) + ' ' + sizes[i];
  }

  upload(): void {
    if (this.files.length === 0) return;
    this.uploadFiles(this.files);
  }

  retryFailed(): void {
    const failedFiles = this.failedFiles;
    if (failedFiles.length === 0) return;
    this.uploadFiles(failedFiles);
  }

  private uploadFiles(filesToUpload: FileItem[]): void {
    this.uploading = true;
    this.uploadComplete = false;
    filesToUpload.forEach(f => {
      f.status = 'uploading';
      f.message = undefined;
    });

    const items: BulkFileOperationItemDto[] = filesToUpload.map(f => ({
      file: f.file,
      entityId: this.entityId!,
      entityType: this.entityType!,
      isPrimary: f.isPrimary
    }));

    this.filesService.bulkUpload(items).subscribe({
      next: (response) => {
        this.uploading = false;
        this.uploadComplete = true;
        this.results = response.data || [];

        this.results.forEach((result, index) => {
          const fileItem = filesToUpload[index];
          if (fileItem) {
            fileItem.status = result.operation === 'uploaded' ? 'success' : 'error';
            fileItem.message = result.errorMessage;
          }
        });
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.uploading = false;
        this.uploadComplete = true;
        filesToUpload.forEach(f => {
          f.status = 'error';
          f.message = error.message || 'Upload failed';
        });
        this.cdr.markForCheck();
      }
    });
  }

  get successCount(): number {
    return this.files.filter(f => f.status === 'success').length;
  }

  get errorCount(): number {
    return this.files.filter(f => f.status === 'error').length;
  }

  get failedFiles(): FileItem[] {
    return this.files.filter(f => f.status === 'error');
  }

  onClose(): void {
    this.dialogRef.close();
  }

}
