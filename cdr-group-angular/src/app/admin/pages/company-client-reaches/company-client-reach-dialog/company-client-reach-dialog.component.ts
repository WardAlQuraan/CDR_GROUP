import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import {
  CompanyClientReachDto,
  CreateCompanyClientReachDto,
  UpdateCompanyClientReachDto
} from '../../../../models/company-client-reach.model';
import { CompanyClientReachesService } from '../../../../services/company-client-reaches.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyClientReachDialogMode = 'create' | 'edit';

export interface CompanyClientReachDialogData {
  mode: CompanyClientReachDialogMode;
  companyId: string;
  reach?: CompanyClientReachDto;
}

@Component({
  selector: 'app-company-client-reach-dialog',
  standalone: false,
  templateUrl: './company-client-reach-dialog.component.html',
  styleUrl: './company-client-reach-dialog.component.scss'
})
export class CompanyClientReachDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyClientReachDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyClientReachDialogComponent>,
    private companyClientReachesService: CompanyClientReachesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyClientReachDialogData
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
    return this.isCreateMode ? 'admin.companyClientReaches.createReach' : 'admin.companyClientReaches.editReach';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  get saveDisabled(): boolean {
    return this.loading || this.form?.invalid;
  }

  private initForm(): void {
    const existing = this.data.reach;
    this.form = this.fb.group({
      clientNameEn: [existing?.clientNameEn ?? '', [Validators.required, Validators.maxLength(200)]],
      clientNameAr: [existing?.clientNameAr ?? '', [Validators.required, Validators.maxLength(200)]],
      reach: [existing?.reach ?? '', [Validators.required, Validators.maxLength(200)]],
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
      this.updateReach();
    } else {
      this.createReach();
    }
  }

  private createReach(): void {
    const dto: CreateCompanyClientReachDto = {
      clientNameEn: this.form.value.clientNameEn,
      clientNameAr: this.form.value.clientNameAr,
      reach: this.form.value.reach,
      descriptionEn: this.form.value.descriptionEn?.trim() || undefined,
      descriptionAr: this.form.value.descriptionAr?.trim() || undefined,
      companyId: this.data.companyId
    };

    this.companyClientReachesService.create(dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyClientReaches.reachCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateReach(): void {
    const dto: UpdateCompanyClientReachDto = {
      clientNameEn: this.form.value.clientNameEn,
      clientNameAr: this.form.value.clientNameAr,
      reach: this.form.value.reach,
      descriptionEn: this.form.value.descriptionEn?.trim() || undefined,
      descriptionAr: this.form.value.descriptionAr?.trim() || undefined,
      companyId: this.data.companyId
    };

    this.companyClientReachesService.update(this.data.reach!.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyClientReaches.reachUpdated'));
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
