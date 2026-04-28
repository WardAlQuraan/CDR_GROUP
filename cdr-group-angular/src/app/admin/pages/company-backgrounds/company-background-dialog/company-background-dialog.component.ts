import { Component, Inject, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyBackgroundDto,
  CreateCompanyBackgroundDto,
  UpdateCompanyBackgroundDto
} from '../../../../models/company-background.model';
import { CompanyBackgroundsService } from '../../../../services/company-backgrounds.service';
import { FilesService } from '../../../../services/files.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

export type CompanyBackgroundDialogMode = 'create' | 'edit';

export interface CompanyBackgroundDialogData {
  mode: CompanyBackgroundDialogMode;
  companyId: string;
  background?: CompanyBackgroundDto;
}

@Component({
  selector: 'app-company-background-dialog',
  standalone: false,
  templateUrl: './company-background-dialog.component.html',
  styleUrl: './company-background-dialog.component.scss'
})
export class CompanyBackgroundDialogComponent {
  @ViewChild('imageInput') imageInput!: ElementRef<HTMLInputElement>;

  mode: CompanyBackgroundDialogMode;
  loading = false;

  pendingFile: File | null = null;
  pendingPreview: string | null = null;
  existingImageUrl?: string;

  constructor(
    private dialogRef: MatDialogRef<CompanyBackgroundDialogComponent>,
    private companyBackgroundsService: CompanyBackgroundsService,
    private filesService: FilesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyBackgroundDialogData
  ) {
    this.mode = data.mode;
    this.existingImageUrl = data.background?.imageUrl;
  }

  get isCreateMode(): boolean {
    return this.mode === 'create';
  }

  get isEditMode(): boolean {
    return this.mode === 'edit';
  }

  get dialogTitle(): string {
    return this.isCreateMode ? 'admin.companyBackgrounds.createBackground' : 'admin.companyBackgrounds.editBackground';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  get saveDisabled(): boolean {
    if (this.loading) return true;
    if (this.isCreateMode) return !this.pendingFile;
    return false;
  }

  get fullExistingUrl(): string | undefined {
    if (!this.existingImageUrl) return undefined;
    if (/^https?:\/\//i.test(this.existingImageUrl)) return this.existingImageUrl;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${this.existingImageUrl.startsWith('/') ? '' : '/'}${this.existingImageUrl}`;
  }

  triggerPick(): void {
    this.imageInput.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const file = input.files[0];
    input.value = '';

    if (this.pendingPreview) {
      URL.revokeObjectURL(this.pendingPreview);
    }

    this.pendingFile = file;
    this.pendingPreview = URL.createObjectURL(file);
    this.cdr.markForCheck();
  }

  discardPending(): void {
    if (this.pendingPreview) {
      URL.revokeObjectURL(this.pendingPreview);
    }
    this.pendingFile = null;
    this.pendingPreview = null;
    this.cdr.markForCheck();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSave(): void {
    if (this.saveDisabled) return;

    if (this.pendingFile) {
      this.uploadAndPersist(this.pendingFile);
    } else if (this.isEditMode) {
      this.persist(this.existingImageUrl!);
    }
  }

  private uploadAndPersist(file: File): void {
    this.loading = true;
    this.filesService.upload({
      file,
      entityId: this.data.companyId,
      entityType: 'CompanyBackground'
    }).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          const url = response.data.fileUrl || response.data.path;
          this.persist(url);
        } else {
          this.loading = false;
          this.cdr.markForCheck();
        }
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private persist(imageUrl: string): void {
    if (this.isCreateMode) {
      const dto: CreateCompanyBackgroundDto = { imageUrl, companyId: this.data.companyId };
      this.companyBackgroundsService.create(dto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.companyBackgrounds.backgroundCreated'));
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.cdr.markForCheck();
        }
      });
    } else {
      const dto: UpdateCompanyBackgroundDto = { imageUrl, companyId: this.data.companyId };
      this.companyBackgroundsService.update(this.data.background!.id, dto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.companyBackgrounds.backgroundUpdated'));
          this.dialogRef.close(true);
        },
        error: () => {
          this.loading = false;
          this.cdr.markForCheck();
        }
      });
    }
  }

  onCancel(): void {
    if (this.pendingPreview) {
      URL.revokeObjectURL(this.pendingPreview);
    }
    this.dialogRef.close();
  }
}
