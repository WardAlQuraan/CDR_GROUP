import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyDistinguishDto,
  CreateCompanyDistinguishDto,
  UpdateCompanyDistinguishDto
} from '../../../../models/company-distinguish.model';
import { CompanyDistinguishesService } from '../../../../services/company-distinguishes.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyDistinguishDialogMode = 'create' | 'edit';

export interface CompanyDistinguishDialogData {
  mode: CompanyDistinguishDialogMode;
  companyId: string;
  distinguish?: CompanyDistinguishDto;
}

@Component({
  selector: 'app-company-distinguish-dialog',
  standalone: false,
  templateUrl: './company-distinguish-dialog.component.html',
  styleUrl: './company-distinguish-dialog.component.scss'
})
export class CompanyDistinguishDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyDistinguishDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyDistinguishDialogComponent>,
    private companyDistinguishesService: CompanyDistinguishesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyDistinguishDialogData
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
    return this.isCreateMode ? 'admin.companyDistinguishes.createDistinguish' : 'admin.companyDistinguishes.editDistinguish';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.distinguish;
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
      this.updateDistinguish();
    } else {
      this.createDistinguish();
    }
  }

  private createDistinguish(): void {
    const dto: CreateCompanyDistinguishDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyDistinguishesService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyDistinguishes.distinguishCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateDistinguish(): void {
    const dto: UpdateCompanyDistinguishDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyDistinguishesService.update(this.data.distinguish!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyDistinguishes.distinguishUpdated'));
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
