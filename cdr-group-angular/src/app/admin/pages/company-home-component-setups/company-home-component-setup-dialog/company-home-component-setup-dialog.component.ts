import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyHomeComponentSetupDto,
  CreateCompanyHomeComponentSetupDto,
  UpdateCompanyHomeComponentSetupDto,
} from '../../../../models/company-home-component-setup.model';
import { CompanyHomeComponentSetupsService } from '../../../../services/company-home-component-setups.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { environment } from '../../../../../environments/environment';

export type CompanyHomeComponentSetupDialogMode = 'create' | 'edit';

export interface CompanyHomeComponentSetupDialogData {
  mode: CompanyHomeComponentSetupDialogMode;
  companyId: string;
  setup?: CompanyHomeComponentSetupDto;
}

@Component({
  selector: 'app-company-home-component-setup-dialog',
  standalone: false,
  templateUrl: './company-home-component-setup-dialog.component.html',
  styleUrl: './company-home-component-setup-dialog.component.scss',
})
export class CompanyHomeComponentSetupDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyHomeComponentSetupDialogMode;
  loading = false;
  componentCodes = environment.componentCodes;
  preferenceCodes = environment.companyPreferenceCodes;
  titleDescriptionCodes = environment.companyTitleDescriptionCodes;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyHomeComponentSetupDialogComponent>,
    private companyHomeComponentSetupsService: CompanyHomeComponentSetupsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyHomeComponentSetupDialogData
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
    return this.isCreateMode
      ? 'admin.companyHomeComponentSetups.createSetup'
      : 'admin.companyHomeComponentSetups.editSetup';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.setup;
    this.form = this.fb.group({
      componentCode: [existing?.componentCode ?? '', [Validators.required, Validators.maxLength(100)]],
      companyTitleDescriptionCode: [existing?.companyTitleDescriptionCode ?? '', [Validators.maxLength(100)]],
      preferenceTitleCode: [existing?.preferenceTitleCode ?? '', [Validators.maxLength(100)]],
      preferenceDescriptionCode: [existing?.preferenceDescriptionCode ?? '', [Validators.maxLength(100)]],
      rank: [existing?.rank ?? 0, [Validators.required]],
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
      this.updateSetup();
    } else {
      this.createSetup();
    }
  }

  private createSetup(): void {
    const dto: CreateCompanyHomeComponentSetupDto = {
      componentCode: this.form.value.componentCode,
      companyTitleDescriptionCode: this.form.value.companyTitleDescriptionCode || undefined,
      preferenceTitleCode: this.form.value.preferenceTitleCode || undefined,
      preferenceDescriptionCode: this.form.value.preferenceDescriptionCode || undefined,
      rank: this.form.value.rank ?? 0,
      companyId: this.data.companyId,
    };

    this.companyHomeComponentSetupsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyHomeComponentSetups.setupCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }

  private updateSetup(): void {
    const dto: UpdateCompanyHomeComponentSetupDto = {
      componentCode: this.form.value.componentCode,
      companyTitleDescriptionCode: this.form.value.companyTitleDescriptionCode || undefined,
      preferenceTitleCode: this.form.value.preferenceTitleCode || undefined,
      preferenceDescriptionCode: this.form.value.preferenceDescriptionCode || undefined,
      rank: this.form.value.rank ?? 0,
      companyId: this.data.companyId,
    };

    this.companyHomeComponentSetupsService.update(this.data.setup!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyHomeComponentSetups.setupUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
