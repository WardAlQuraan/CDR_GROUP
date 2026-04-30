import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { CompanyFinancialClausesRightsDto, CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto } from '../../../../models/company-financial-clauses-rights.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CompanyFinancialClausesRightsService } from '../../../../services/company-financial-clauses-rights.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyFinancialClausesRightsDialogMode = 'create' | 'edit';

export interface CompanyFinancialClausesRightsDialogData {
  mode: CompanyFinancialClausesRightsDialogMode;
  companyId: string;
  financialClause?: CompanyFinancialClausesRightsDto;
}

@Component({
  selector: 'app-company-financial-clauses-rights-dialog',
  standalone: false,
  templateUrl: './company-financial-clauses-rights-dialog.component.html',
  styleUrl: './company-financial-clauses-rights-dialog.component.scss',
})
export class CompanyFinancialClausesRightsDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyFinancialClausesRightsDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyFinancialClausesRightsDialogComponent>,
    private companyFinancialClausesRightsService: CompanyFinancialClausesRightsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyFinancialClausesRightsDialogData
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
    return this.isCreateMode ? 'admin.companyFinancialClausesRights.createFinancialClause' : 'admin.companyFinancialClausesRights.editFinancialClause';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.financialClause;
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
      this.updateFinancialClause();
    } else {
      this.createFinancialClause();
    }
  }

  private createFinancialClause(): void {
    const dto: CreateCompanyFinancialClausesRightsDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyFinancialClausesRightsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyFinancialClausesRights.financialClauseCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateFinancialClause(): void {
    const dto: UpdateCompanyFinancialClausesRightsDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyFinancialClausesRightsService.update(this.data.financialClause!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyFinancialClausesRights.financialClauseUpdated'));
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
