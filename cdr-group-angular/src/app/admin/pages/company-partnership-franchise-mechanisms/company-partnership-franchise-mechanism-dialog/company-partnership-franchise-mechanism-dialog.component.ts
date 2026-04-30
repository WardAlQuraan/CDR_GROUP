import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyPartnershipFranchiseMechanismDto,
  CreateCompanyPartnershipFranchiseMechanismDto,
  UpdateCompanyPartnershipFranchiseMechanismDto
} from '../../../../models/company-partnership-franchise-mechanism.model';
import { CompanyPartnershipFranchiseMechanismsService } from '../../../../services/company-partnership-franchise-mechanisms.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyPartnershipFranchiseMechanismDialogMode = 'create' | 'edit';

export interface CompanyPartnershipFranchiseMechanismDialogData {
  mode: CompanyPartnershipFranchiseMechanismDialogMode;
  companyId: string;
  mechanism?: CompanyPartnershipFranchiseMechanismDto;
}

@Component({
  selector: 'app-company-partnership-franchise-mechanism-dialog',
  standalone: false,
  templateUrl: './company-partnership-franchise-mechanism-dialog.component.html',
  styleUrl: './company-partnership-franchise-mechanism-dialog.component.scss'
})
export class CompanyPartnershipFranchiseMechanismDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyPartnershipFranchiseMechanismDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyPartnershipFranchiseMechanismDialogComponent>,
    private companyPartnershipFranchiseMechanismsService: CompanyPartnershipFranchiseMechanismsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyPartnershipFranchiseMechanismDialogData
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
    return this.isCreateMode ? 'admin.companyPartnershipFranchiseMechanisms.createMechanism' : 'admin.companyPartnershipFranchiseMechanisms.editMechanism';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.mechanism;
    this.form = this.fb.group({
      descriptionEn: [existing?.descriptionEn ?? '', [Validators.required, Validators.maxLength(2000)]],
      descriptionAr: [existing?.descriptionAr ?? '', [Validators.required, Validators.maxLength(2000)]]
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
      this.updateMechanism();
    } else {
      this.createMechanism();
    }
  }

  private createMechanism(): void {
    const dto: CreateCompanyPartnershipFranchiseMechanismDto = {
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyPartnershipFranchiseMechanismsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPartnershipFranchiseMechanisms.mechanismCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateMechanism(): void {
    const dto: UpdateCompanyPartnershipFranchiseMechanismDto = {
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyPartnershipFranchiseMechanismsService.update(this.data.mechanism!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPartnershipFranchiseMechanisms.mechanismUpdated'));
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
