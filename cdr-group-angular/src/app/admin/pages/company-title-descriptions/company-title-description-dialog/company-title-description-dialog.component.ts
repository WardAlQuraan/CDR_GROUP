import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyTitleDescriptionDto,
  CreateCompanyTitleDescriptionDto,
  UpdateCompanyTitleDescriptionDto
} from '../../../../models/company-title-description.model';
import { CompanyTitleDescriptionsService } from '../../../../services/company-title-descriptions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

export type CompanyTitleDescriptionDialogMode = 'create' | 'edit';

export interface CompanyTitleDescriptionDialogData {
  mode: CompanyTitleDescriptionDialogMode;
  companyId: string;
  item?: CompanyTitleDescriptionDto;
}

@Component({
  selector: 'app-company-title-description-dialog',
  standalone: false,
  templateUrl: './company-title-description-dialog.component.html',
  styleUrl: './company-title-description-dialog.component.scss'
})
export class CompanyTitleDescriptionDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyTitleDescriptionDialogMode;
  loading = false;
  allowedCodes: string[] = (environment as { companyTitleDescriptionCodes?: string[] }).companyTitleDescriptionCodes ?? [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyTitleDescriptionDialogComponent>,
    private companyTitleDescriptionsService: CompanyTitleDescriptionsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyTitleDescriptionDialogData
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
    return this.isCreateMode ? 'admin.companyTitleDescriptions.createItem' : 'admin.companyTitleDescriptions.editItem';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.item;
    this.form = this.fb.group({
      code: [{ value: existing?.code ?? '', disabled: this.isEditMode }, [Validators.required, Validators.maxLength(100)]],
      titleEn: [existing?.titleEn ?? '', [Validators.maxLength(500)]],
      titleAr: [existing?.titleAr ?? '', [Validators.maxLength(500)]],
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
      this.updateItem();
    } else {
      this.createItem();
    }
  }

  private createItem(): void {
    const dto: CreateCompanyTitleDescriptionDto = {
      code: this.form.value.code,
      titleEn: this.form.value.titleEn?.trim() || undefined,
      titleAr: this.form.value.titleAr?.trim() || undefined,
      descriptionEn: this.form.value.descriptionEn?.trim() || undefined,
      descriptionAr: this.form.value.descriptionAr?.trim() || undefined,
      companyId: this.data.companyId
    };

    this.companyTitleDescriptionsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyTitleDescriptions.itemCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateItem(): void {
    const dto: UpdateCompanyTitleDescriptionDto = {
      titleEn: this.form.value.titleEn?.trim() || undefined,
      titleAr: this.form.value.titleAr?.trim() || undefined,
      descriptionEn: this.form.value.descriptionEn?.trim() || undefined,
      descriptionAr: this.form.value.descriptionAr?.trim() || undefined,
      companyId: this.data.companyId
    };

    this.companyTitleDescriptionsService.update(this.data.item!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyTitleDescriptions.itemUpdated'));
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
