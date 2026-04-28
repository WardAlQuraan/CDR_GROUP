import { Component, Inject, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyFormDto,
  CreateCompanyFormDto,
  UpdateCompanyFormDto
} from '../../../../models/company-form.model';
import { CompanyFormsService } from '../../../../services/company-forms.service';
import { FilesService } from '../../../../services/files.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyFormDialogMode = 'create' | 'edit';

export interface CompanyFormDialogData {
  mode: CompanyFormDialogMode;
  companyId: string;
  form?: CompanyFormDto;
}

@Component({
  selector: 'app-company-form-dialog',
  standalone: false,
  templateUrl: './company-form-dialog.component.html',
  styleUrl: './company-form-dialog.component.scss'
})
export class CompanyFormDialogComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  form!: FormGroup;
  mode: CompanyFormDialogMode;
  loading = false;
  uploading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyFormDialogComponent>,
    private companyFormsService: CompanyFormsService,
    private filesService: FilesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyFormDialogData
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
    return this.isCreateMode ? 'admin.companyForms.createForm' : 'admin.companyForms.editForm';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    const existing = this.data.form;
    this.form = this.fb.group({
      formNameEn: [existing?.formNameEn ?? '', [Validators.required, Validators.maxLength(200)]],
      formNameAr: [existing?.formNameAr ?? '', [Validators.required, Validators.maxLength(200)]],
      formUrl: [existing?.formUrl ?? '', [Validators.required, Validators.maxLength(500)]]
    });
  }

  triggerPick(): void {
    this.fileInput.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const file = input.files[0];
    input.value = '';

    this.uploading = true;
    this.filesService.upload({
      file,
      entityId: this.data.companyId,
      entityType: 'CompanyForm'
    }).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          const url = response.data.fileUrl || response.data.path;
          this.form.patchValue({ formUrl: url });
        }
        this.uploading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.uploading = false;
        this.cdr.markForCheck();
      }
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
      this.updateForm();
    } else {
      this.createForm();
    }
  }

  private createForm(): void {
    const dto: CreateCompanyFormDto = {
      formUrl: this.form.value.formUrl,
      formNameEn: this.form.value.formNameEn,
      formNameAr: this.form.value.formNameAr,
      companyId: this.data.companyId
    };

    this.companyFormsService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyForms.formCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateForm(): void {
    const dto: UpdateCompanyFormDto = {
      formUrl: this.form.value.formUrl,
      formNameEn: this.form.value.formNameEn,
      formNameAr: this.form.value.formNameAr,
      companyId: this.data.companyId
    };

    this.companyFormsService.update(this.data.form!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyForms.formUpdated'));
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
