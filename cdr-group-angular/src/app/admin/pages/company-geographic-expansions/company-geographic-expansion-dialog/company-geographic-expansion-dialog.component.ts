import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyGeographicExpansionDto,
  CreateCompanyGeographicExpansionDto,
  UpdateCompanyGeographicExpansionDto
} from '../../../../models/company-geographic-expansion.model';
import { CompanyGeographicExpansionsService } from '../../../../services/company-geographic-expansions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyGeographicExpansionDialogMode = 'create' | 'edit';

export interface CompanyGeographicExpansionDialogData {
  mode: CompanyGeographicExpansionDialogMode;
  companyId: string;
  expansion?: CompanyGeographicExpansionDto;
}

@Component({
  selector: 'app-company-geographic-expansion-dialog',
  standalone: false,
  templateUrl: './company-geographic-expansion-dialog.component.html',
  styleUrl: './company-geographic-expansion-dialog.component.scss'
})
export class CompanyGeographicExpansionDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyGeographicExpansionDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyGeographicExpansionDialogComponent>,
    private companyGeographicExpansionsService: CompanyGeographicExpansionsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyGeographicExpansionDialogData
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
    return this.isCreateMode ? 'admin.companyGeographicExpansions.createExpansion' : 'admin.companyGeographicExpansions.editExpansion';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.expansion;
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
      this.updateExpansion();
    } else {
      this.createExpansion();
    }
  }

  private createExpansion(): void {
    const dto: CreateCompanyGeographicExpansionDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyGeographicExpansionsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyGeographicExpansions.expansionCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateExpansion(): void {
    const dto: UpdateCompanyGeographicExpansionDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyGeographicExpansionsService.update(this.data.expansion!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyGeographicExpansions.expansionUpdated'));
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
