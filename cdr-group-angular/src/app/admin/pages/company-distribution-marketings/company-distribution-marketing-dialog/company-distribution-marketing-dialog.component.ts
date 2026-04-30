import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyDistributionMarketingDto,
  CreateCompanyDistributionMarketingDto,
  UpdateCompanyDistributionMarketingDto
} from '../../../../models/company-distribution-marketing.model';
import { CompanyDistributionMarketingsService } from '../../../../services/company-distribution-marketings.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyDistributionMarketingDialogMode = 'create' | 'edit';

export interface CompanyDistributionMarketingDialogData {
  mode: CompanyDistributionMarketingDialogMode;
  companyId: string;
  marketing?: CompanyDistributionMarketingDto;
}

@Component({
  selector: 'app-company-distribution-marketing-dialog',
  standalone: false,
  templateUrl: './company-distribution-marketing-dialog.component.html',
  styleUrl: './company-distribution-marketing-dialog.component.scss'
})
export class CompanyDistributionMarketingDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyDistributionMarketingDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyDistributionMarketingDialogComponent>,
    private companyDistributionMarketingsService: CompanyDistributionMarketingsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyDistributionMarketingDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.initForm();
  }

  get isCreateMode(): boolean {
    return this.mode === 'create';
  }

  get isEditMode(): boolean {
    return this.mode === 'edit';
  }

  get dialogTitle(): string {
    return this.isCreateMode ? 'admin.companyDistributionMarketings.createMarketing' : 'admin.companyDistributionMarketings.editMarketing';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.marketing;
    this.form = this.fb.group({
      titleEn: [existing?.titleEn ?? '', [Validators.required, Validators.maxLength(500)]],
      titleAr: [existing?.titleAr ?? '', [Validators.required, Validators.maxLength(500)]],
      descriptionEn: [existing?.descriptionEn ?? '', [Validators.maxLength(2000)]],
      descriptionAr: [existing?.descriptionAr ?? '', [Validators.maxLength(2000)]]
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    if (this.isEditMode) {
      this.updateMarketing();
    } else {
      this.createMarketing();
    }
  }

  private createMarketing(): void {
    const dto: CreateCompanyDistributionMarketingDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyDistributionMarketingsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyDistributionMarketings.marketingCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateMarketing(): void {
    const dto: UpdateCompanyDistributionMarketingDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyDistributionMarketingsService.update(this.data.marketing!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyDistributionMarketings.marketingUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
