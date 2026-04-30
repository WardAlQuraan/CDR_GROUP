import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanySuccessReasonDto,
  CreateCompanySuccessReasonDto,
  UpdateCompanySuccessReasonDto
} from '../../../../models/company-success-reason.model';
import { CompanySuccessReasonsService } from '../../../../services/company-success-reasons.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanySuccessReasonDialogMode = 'create' | 'edit';

export interface CompanySuccessReasonDialogData {
  mode: CompanySuccessReasonDialogMode;
  companyId: string;
  successReason?: CompanySuccessReasonDto;
}

@Component({
  selector: 'app-company-success-reason-dialog',
  standalone: false,
  templateUrl: './company-success-reason-dialog.component.html',
  styleUrl: './company-success-reason-dialog.component.scss'
})
export class CompanySuccessReasonDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanySuccessReasonDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanySuccessReasonDialogComponent>,
    private companySuccessReasonsService: CompanySuccessReasonsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanySuccessReasonDialogData
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
    return this.isCreateMode ? 'admin.companySuccessReasons.createReason' : 'admin.companySuccessReasons.editReason';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.successReason;
    this.form = this.fb.group({
      reasonEn: [existing?.reasonEn ?? '', [Validators.required, Validators.maxLength(1000)]],
      reasonAr: [existing?.reasonAr ?? '', [Validators.required, Validators.maxLength(1000)]]
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
      this.updateSuccessReason();
    } else {
      this.createSuccessReason();
    }
  }

  private createSuccessReason(): void {
    const dto: CreateCompanySuccessReasonDto = {
      reasonEn: this.form.value.reasonEn,
      reasonAr: this.form.value.reasonAr,
      companyId: this.data.companyId
    };

    this.companySuccessReasonsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companySuccessReasons.reasonCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateSuccessReason(): void {
    const dto: UpdateCompanySuccessReasonDto = {
      reasonEn: this.form.value.reasonEn,
      reasonAr: this.form.value.reasonAr,
      companyId: this.data.companyId
    };

    this.companySuccessReasonsService.update(this.data.successReason!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companySuccessReasons.reasonUpdated'));
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
