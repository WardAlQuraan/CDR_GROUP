import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyPreferenceDto,
  CreateCompanyPreferenceDto,
  UpdateCompanyPreferenceDto
} from '../../../../models/company-preference.model';
import { CompanyPreferencesService } from '../../../../services/company-preferences.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

export type CompanyPreferenceDialogMode = 'create' | 'edit';

export interface CompanyPreferenceDialogData {
  mode: CompanyPreferenceDialogMode;
  companyId: string;
  preference?: CompanyPreferenceDto;
}

@Component({
  selector: 'app-company-preference-dialog',
  standalone: false,
  templateUrl: './company-preference-dialog.component.html',
  styleUrl: './company-preference-dialog.component.scss'
})
export class CompanyPreferenceDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyPreferenceDialogMode;
  loading = false;
  allowedCodes: string[] = environment.companyPreferenceCodes ?? [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyPreferenceDialogComponent>,
    private companyPreferencesService: CompanyPreferencesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyPreferenceDialogData
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
    return this.isCreateMode ? 'admin.companyPreferences.createPreference' : 'admin.companyPreferences.editPreference';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.preference;
    this.form = this.fb.group({
      code: [{ value: existing?.code ?? '', disabled: this.isEditMode }, [Validators.required, Validators.maxLength(100)]],
      valueEn: [existing?.valueEn ?? '', [Validators.required, Validators.maxLength(1000)]],
      valueAr: [existing?.valueAr ?? '', [Validators.required, Validators.maxLength(1000)]]
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
      this.updatePreference();
    } else {
      this.createPreference();
    }
  }

  private createPreference(): void {
    const dto: CreateCompanyPreferenceDto = {
      code: this.form.value.code,
      valueEn: this.form.value.valueEn,
      valueAr: this.form.value.valueAr,
      companyId: this.data.companyId
    };

    this.companyPreferencesService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPreferences.preferenceCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updatePreference(): void {
    const dto: UpdateCompanyPreferenceDto = {
      valueEn: this.form.value.valueEn,
      valueAr: this.form.value.valueAr,
      companyId: this.data.companyId
    };

    this.companyPreferencesService.update(this.data.preference!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPreferences.preferenceUpdated'));
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
