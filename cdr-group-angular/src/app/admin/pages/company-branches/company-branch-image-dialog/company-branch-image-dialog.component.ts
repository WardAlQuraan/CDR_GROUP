import { Component, Inject, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyBranchDto,
  UpdateCompanyBranchDto
} from '../../../../models/company-branch.model';
import { CompanyBranchesService } from '../../../../services/company-branches.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

export interface CompanyBranchImageDialogData {
  companyId: string;
  branch: CompanyBranchDto;
}

@Component({
  selector: 'app-company-branch-image-dialog',
  standalone: false,
  templateUrl: './company-branch-image-dialog.component.html',
  styleUrl: './company-branch-image-dialog.component.scss'
})
export class CompanyBranchImageDialogComponent {
  @ViewChild('imageInput') imageInput!: ElementRef<HTMLInputElement>;

  loading = false;

  pendingFile: File | null = null;
  pendingPreview: string | null = null;
  existingImageUrl?: string;
  removed = false;

  constructor(
    private dialogRef: MatDialogRef<CompanyBranchImageDialogComponent>,
    private companyBranchesService: CompanyBranchesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyBranchImageDialogData
  ) {
    this.existingImageUrl = data.branch.imageUrl;
  }

  get dialogTitle(): string {
    return 'admin.companyBranches.uploadImage';
  }

  get saveLabel(): string {
    return 'common.save';
  }

  get saveDisabled(): boolean {
    if (this.loading) return true;
    if (this.pendingFile) return false;
    if (this.removed && this.existingImageUrl !== undefined) return false;
    return true;
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
    this.removed = false;
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

  removeExisting(): void {
    this.removed = true;
    this.cdr.markForCheck();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSave(): void {
    if (this.saveDisabled) return;

    if (this.pendingFile) {
      this.uploadImage(this.pendingFile);
    } else if (this.removed) {
      this.clearImage();
    }
  }

  private uploadImage(file: File): void {
    this.loading = true;
    this.companyBranchesService.uploadImage(this.data.branch.id, file).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyBranches.imageUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private clearImage(): void {
    this.loading = true;
    const branch = this.data.branch;
    const dto: UpdateCompanyBranchDto = {
      nameEn: branch.nameEn,
      nameAr: branch.nameAr,
      nickNameEn: branch.nickNameEn || undefined,
      nickNameAr: branch.nickNameAr || undefined,
      descriptionEn: branch.descriptionEn || undefined,
      descriptionAr: branch.descriptionAr || undefined,
      openingDate: branch.openingDate,
      companyId: this.data.companyId,
      cityId: branch.cityId,
      locationUrl: branch.locationUrl || undefined,
      imageUrl: undefined
    };

    this.companyBranchesService.update(branch.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyBranches.imageUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    if (this.pendingPreview) {
      URL.revokeObjectURL(this.pendingPreview);
    }
    this.dialogRef.close();
  }
}
