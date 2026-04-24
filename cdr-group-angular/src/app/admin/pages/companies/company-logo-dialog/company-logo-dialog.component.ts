import { Component, Inject, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CompanyDto } from '../../../../models/company.model';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-company-logo-dialog',
  standalone: false,
  templateUrl: './company-logo-dialog.component.html',
  styleUrl: './company-logo-dialog.component.scss'
})
export class CompanyLogoDialogComponent {
  @ViewChild('logoInput') logoInput!: ElementRef<HTMLInputElement>;

  company: CompanyDto;
  logo?: string;
  uploading = false;
  changed = false;
  cacheBust = '';

  pendingFile: File | null = null;
  pendingPreview: string | null = null;

  constructor(
    private dialogRef: MatDialogRef<CompanyLogoDialogComponent>,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    public translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) data: CompanyDto
  ) {
    this.company = data;
    this.logo = data.logo;
  }

  get fullLogoUrl(): string | undefined {
    if (!this.logo) return undefined;
    const url = /^https?:\/\//i.test(this.logo)
      ? this.logo
      : `${environment.apiUrl.replace(/\/api\/?$/, '')}${this.logo.startsWith('/') ? '' : '/'}${this.logo}`;
    return this.cacheBust ? `${url}${url.includes('?') ? '&' : '?'}v=${this.cacheBust}` : url;
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
    this.companiesService.uploadLogo(this.company.id, this.pendingFile).subscribe({
      next: () => {
        this.snackbar.success(this.translationService.translate('admin.companies.logoUploaded'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.uploading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onClose(): void {
    this.dialogRef.close(this.changed);
  }
}
