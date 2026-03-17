import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CompanyDto, CreateCompanyDto, UpdateCompanyDto } from '../../../../models/company.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyDialogMode = 'create' | 'edit';

export interface CompanyDialogData {
  mode: CompanyDialogMode;
  company?: CompanyDto;
}

@Component({
  selector: 'app-company-dialog',
  standalone: false,
  templateUrl: './company-dialog.component.html',
  styleUrl: './company-dialog.component.scss'
})
export class CompanyDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyDialogMode;
  loading = false;
  companiesDataSource$!: Observable<ApiResponse<CompanyDto[]>>;

  weekDays = [
    { value: 'Sunday', labelEn: 'Sunday', labelAr: 'الأحد' },
    { value: 'Monday', labelEn: 'Monday', labelAr: 'الاثنين' },
    { value: 'Tuesday', labelEn: 'Tuesday', labelAr: 'الثلاثاء' },
    { value: 'Wednesday', labelEn: 'Wednesday', labelAr: 'الأربعاء' },
    { value: 'Thursday', labelEn: 'Thursday', labelAr: 'الخميس' },
    { value: 'Friday', labelEn: 'Friday', labelAr: 'الجمعة' },
    { value: 'Saturday', labelEn: 'Saturday', labelAr: 'السبت' }
  ];

  companyMapper = (company: CompanyDto): SelectOption => ({
    value: company.id,
    label: this.isArabic ? company.nameAr : company.nameEn
  });

  parentFilter = (company: CompanyDto): boolean => {
    if (this.isEditMode && this.data.company) {
      return company.id !== this.data.company.id;
    }
    return true;
  };

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyDialogComponent>,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.companiesDataSource$ = this.companiesService.getActiveCompanies();
    this.initForm();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get isCreateMode(): boolean {
    return this.mode === 'create';
  }

  get isEditMode(): boolean {
    return this.mode === 'edit';
  }

  get dialogTitle(): string {
    return this.isCreateMode ? 'admin.companies.createCompany' : 'admin.companies.editCompany';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const company = this.data.company!;
      this.form = this.fb.group({
        nameEn: [company.nameEn, [Validators.required, Validators.maxLength(200)]],
        nameAr: [company.nameAr, [Validators.required, Validators.maxLength(200)]],
        descriptionEn: [company.descriptionEn, [Validators.required, Validators.maxLength(500)]],
        descriptionAr: [company.descriptionAr, [Validators.required, Validators.maxLength(500)]],
        storyEn: [company.storyEn, [Validators.required, Validators.maxLength(2000)]],
        storyAr: [company.storyAr, [Validators.required, Validators.maxLength(2000)]],
        missionEn: [company.missionEn, [Validators.required, Validators.maxLength(1000)]],
        missionAr: [company.missionAr, [Validators.required, Validators.maxLength(1000)]],
        visionEn: [company.visionEn, [Validators.required, Validators.maxLength(1000)]],
        visionAr: [company.visionAr, [Validators.required, Validators.maxLength(1000)]],
        titleEn: [company.titleEn, [Validators.required, Validators.maxLength(500)]],
        titleAr: [company.titleAr, [Validators.required, Validators.maxLength(500)]],
        primaryColor: [company.primaryColor || '#000000', [Validators.required]],
        secondaryColor: [company.secondaryColor || '#000000', [Validators.required]],
        openingStartDay: [company.openingStartDay, [Validators.required]],
        openingEndDay: [company.openingEndDay, [Validators.required]],
        openingStartTime: [company.openingStartTime, [Validators.required]],
        openingEndTime: [company.openingEndTime, [Validators.required]],
        openingHoursNoteEn: [company.openingHoursNoteEn],
        openingHoursNoteAr: [company.openingHoursNoteAr],
        numberOfEmployees: [company.numberOfEmployees],
        partnershipFormUrl: [company.partnershipFormUrl],
        parentId: [company.parentId],
        isActive: [company.isActive]
      });
    } else {
      this.form = this.fb.group({
        nameEn: ['', [Validators.required, Validators.maxLength(200)]],
        nameAr: ['', [Validators.required, Validators.maxLength(200)]],
        descriptionEn: ['', [Validators.required, Validators.maxLength(500)]],
        descriptionAr: ['', [Validators.required, Validators.maxLength(500)]],
        storyEn: ['', [Validators.required, Validators.maxLength(2000)]],
        storyAr: ['', [Validators.required, Validators.maxLength(2000)]],
        missionEn: ['', [Validators.required, Validators.maxLength(1000)]],
        missionAr: ['', [Validators.required, Validators.maxLength(1000)]],
        visionEn: ['', [Validators.required, Validators.maxLength(1000)]],
        visionAr: ['', [Validators.required, Validators.maxLength(1000)]],
        titleEn: ['', [Validators.required, Validators.maxLength(500)]],
        titleAr: ['', [Validators.required, Validators.maxLength(500)]],
        primaryColor: ['#000000', [Validators.required]],
        secondaryColor: ['#000000', [Validators.required]],
        openingStartDay: ['', [Validators.required]],
        openingEndDay: ['', [Validators.required]],
        openingStartTime: ['', [Validators.required]],
        openingEndTime: ['', [Validators.required]],
        openingHoursNoteEn: [''],
        openingHoursNoteAr: [''],
        numberOfEmployees: [null],
        partnershipFormUrl: [''],
        parentId: [null],
        isActive: [true]
      });
    }
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
      this.updateCompany();
    } else {
      this.createCompany();
    }
  }

  private createCompany(): void {
    const createDto: CreateCompanyDto = {
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      storyEn: this.form.value.storyEn || undefined,
      storyAr: this.form.value.storyAr || undefined,
      missionEn: this.form.value.missionEn || undefined,
      missionAr: this.form.value.missionAr || undefined,
      visionEn: this.form.value.visionEn || undefined,
      visionAr: this.form.value.visionAr || undefined,
      titleEn: this.form.value.titleEn || undefined,
      titleAr: this.form.value.titleAr || undefined,
      primaryColor: this.form.value.primaryColor || undefined,
      secondaryColor: this.form.value.secondaryColor || undefined,
      openingStartDay: this.form.value.openingStartDay,
      openingEndDay: this.form.value.openingEndDay,
      openingStartTime: this.form.value.openingStartTime,
      openingEndTime: this.form.value.openingEndTime,
      openingHoursNoteEn: this.form.value.openingHoursNoteEn || undefined,
      openingHoursNoteAr: this.form.value.openingHoursNoteAr || undefined,
      numberOfEmployees: this.form.value.numberOfEmployees || undefined,
      partnershipFormUrl: this.form.value.partnershipFormUrl || undefined,
      parentId: this.form.value.parentId || undefined,
      isActive: this.form.value.isActive
    };

    this.companiesService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companies.companyCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateCompany(): void {
    const updateDto: UpdateCompanyDto = {
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      storyEn: this.form.value.storyEn || undefined,
      storyAr: this.form.value.storyAr || undefined,
      missionEn: this.form.value.missionEn || undefined,
      missionAr: this.form.value.missionAr || undefined,
      visionEn: this.form.value.visionEn || undefined,
      visionAr: this.form.value.visionAr || undefined,
      titleEn: this.form.value.titleEn || undefined,
      titleAr: this.form.value.titleAr || undefined,
      primaryColor: this.form.value.primaryColor || undefined,
      secondaryColor: this.form.value.secondaryColor || undefined,
      openingStartDay: this.form.value.openingStartDay,
      openingEndDay: this.form.value.openingEndDay,
      openingStartTime: this.form.value.openingStartTime,
      openingEndTime: this.form.value.openingEndTime,
      openingHoursNoteEn: this.form.value.openingHoursNoteEn || undefined,
      openingHoursNoteAr: this.form.value.openingHoursNoteAr || undefined,
      numberOfEmployees: this.form.value.numberOfEmployees || undefined,
      partnershipFormUrl: this.form.value.partnershipFormUrl || undefined,
      parentId: this.form.value.parentId || undefined,
      isActive: this.form.value.isActive
    };

    this.companiesService.update(this.data.company!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companies.companyUpdated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
