import { Component, Inject, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CompanyClientReachDto } from '../../../../models/company-client-reach.model';
import { CompanyClientReachesService } from '../../../../services/company-client-reaches.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-company-client-reach-logo-dialog',
  standalone: false,
  templateUrl: './company-client-reach-logo-dialog.component.html',
  styleUrl: './company-client-reach-logo-dialog.component.scss'
})
export class CompanyClientReachLogoDialogComponent {
  @ViewChild('logoInput') logoInput!: ElementRef<HTMLInputElement>;

  reach: CompanyClientReachDto;
  logo?: string;
  uploading = false;
  changed = false;

  pendingFile: File | null = null;
  pendingPreview: string | null = null;

  constructor(
    private dialogRef: MatDialogRef<CompanyClientReachLogoDialogComponent>,
    private companyClientReachesService: CompanyClientReachesService,
    private snackbar: SnackbarService,
    public translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) data: CompanyClientReachDto
  ) {
    this.reach = data;
    this.logo = data.clientLogoUrl;
  }

  get displayName(): string {
    return this.translationService.language() === 'ar' ? this.reach.clientNameAr : this.reach.clientNameEn;
  }

  get fullLogoUrl(): string | undefined {
    if (!this.logo) return undefined;
    if (/^https?:\/\//i.test(this.logo)) return this.logo;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${this.logo.startsWith('/') ? '' : '/'}${this.logo}`;
  }

  triggerPick(): void {
    this.logoInput.nativeElement.click();
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
  }

  confirmUpload(): void {
    if (!this.pendingFile) return;

    this.uploading = true;
    this.companyClientReachesService.uploadLogo(this.reach.id, this.pendingFile).subscribe({
      next: () => {
        this.snackbar.success(this.translationService.translate('admin.companyClientReaches.logoUploaded'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.uploading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onClose(): void {
    if (this.pendingPreview) {
      URL.revokeObjectURL(this.pendingPreview);
    }
    this.dialogRef.close(this.changed);
  }
}
