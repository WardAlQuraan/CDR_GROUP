import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyPreContractStudyDto,
  CreateCompanyPreContractStudyDto,
  UpdateCompanyPreContractStudyDto
} from '../../../../models/company-pre-contract-study.model';
import { CompanyPreContractStudiesService } from '../../../../services/company-pre-contract-studies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyPreContractStudyDialogMode = 'create' | 'edit';

export interface CompanyPreContractStudyDialogData {
  mode: CompanyPreContractStudyDialogMode;
  companyId: string;
  study?: CompanyPreContractStudyDto;
}

@Component({
  selector: 'app-company-pre-contract-study-dialog',
  standalone: false,
  templateUrl: './company-pre-contract-study-dialog.component.html',
  styleUrl: './company-pre-contract-study-dialog.component.scss'
})
export class CompanyPreContractStudyDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyPreContractStudyDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyPreContractStudyDialogComponent>,
    private companyPreContractStudiesService: CompanyPreContractStudiesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyPreContractStudyDialogData
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
    return this.isCreateMode ? 'admin.companyPreContractStudies.createStudy' : 'admin.companyPreContractStudies.editStudy';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.study;
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
      this.updateStudy();
    } else {
      this.createStudy();
    }
  }

  private createStudy(): void {
    const dto: CreateCompanyPreContractStudyDto = {
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyPreContractStudiesService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPreContractStudies.studyCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateStudy(): void {
    const dto: UpdateCompanyPreContractStudyDto = {
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      companyId: this.data.companyId
    };

    this.companyPreContractStudiesService.update(this.data.study!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyPreContractStudies.studyUpdated'));
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
