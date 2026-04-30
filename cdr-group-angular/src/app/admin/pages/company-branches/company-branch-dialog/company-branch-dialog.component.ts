import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import {
  CompanyBranchDto,
  CreateCompanyBranchDto,
  UpdateCompanyBranchDto
} from '../../../../models/company-branch.model';
import { CityDto } from '../../../../models/city.model';
import { CountryDto } from '../../../../models/country.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';
import { CompanyBranchesService } from '../../../../services/company-branches.service';
import { CitiesService } from '../../../../services/cities.service';
import { CountriesService } from '../../../../services/countries.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyBranchDialogMode = 'create' | 'edit';

export interface CompanyBranchDialogData {
  mode: CompanyBranchDialogMode;
  companyId: string;
  branch?: CompanyBranchDto;
}

@Component({
  selector: 'app-company-branch-dialog',
  standalone: false,
  templateUrl: './company-branch-dialog.component.html',
  styleUrl: './company-branch-dialog.component.scss'
})
export class CompanyBranchDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyBranchDialogMode;
  loading = false;
  showCityFilter = false;

  countriesDataSource$!: Observable<ApiResponse<CountryDto[]>>;
  citiesDataSource$!: Observable<ApiResponse<CityDto[]>>;

  selectedCountryId: string | null = null;

  countryMapper = (country: CountryDto): SelectOption => ({
    value: country.id,
    label: this.isArabic ? country.nameAr : country.nameEn
  });

  cityMapper = (city: CityDto): SelectOption => ({
    value: city.id,
    label: this.isArabic ? city.nameAr : city.nameEn
  });

  cityFilter = (city: CityDto): boolean => {
    if (!this.selectedCountryId) return true;
    return city.countryId === this.selectedCountryId;
  };

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyBranchDialogComponent>,
    private companyBranchesService: CompanyBranchesService,
    private citiesService: CitiesService,
    private countriesService: CountriesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyBranchDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.countriesDataSource$ = this.countriesService.getCountriesHaveCities();
    this.citiesDataSource$ = this.citiesService.getAllCached();
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
    return this.isCreateMode ? 'admin.companyBranches.createBranch' : 'admin.companyBranches.editBranch';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const branch = this.data.branch!;
      this.form = this.fb.group({
        nameEn: [branch.nameEn, [Validators.required, Validators.maxLength(200)]],
        nameAr: [branch.nameAr, [Validators.required, Validators.maxLength(200)]],
        nickNameEn: [branch.nickNameEn ?? '', [Validators.maxLength(200)]],
        nickNameAr: [branch.nickNameAr ?? '', [Validators.maxLength(200)]],
        descriptionEn: [branch.descriptionEn ?? '', [Validators.maxLength(2000)]],
        descriptionAr: [branch.descriptionAr ?? '', [Validators.maxLength(2000)]],
        openingDate: [branch.openingDate ? new Date(branch.openingDate) : null, [Validators.required]],
        countryId: [null],
        cityId: [branch.cityId, [Validators.required]]
      });
      this.citiesService.getAllCached().subscribe(res => {
        const city = res.data?.find(c => c.id === branch.cityId);
        if (city) {
          this.selectedCountryId = city.countryId;
          this.form.get('countryId')?.setValue(city.countryId);
        }
        this.showCityFilter = true;
        this.cdr.markForCheck();
      });
    } else {
      this.form = this.fb.group({
        nameEn: ['', [Validators.required, Validators.maxLength(200)]],
        nameAr: ['', [Validators.required, Validators.maxLength(200)]],
        nickNameEn: ['', [Validators.maxLength(200)]],
        nickNameAr: ['', [Validators.maxLength(200)]],
        descriptionEn: ['', [Validators.maxLength(2000)]],
        descriptionAr: ['', [Validators.maxLength(2000)]],
        openingDate: [null, [Validators.required]],
        countryId: [null],
        cityId: [null, [Validators.required]]
      });
      this.showCityFilter = true;
    }
  }

  onCountryChange(countryId: string | null): void {
    this.selectedCountryId = countryId;
    this.form.get('cityId')?.setValue(null);
    this.showCityFilter = false;
    setTimeout(() => {
      this.citiesDataSource$ = this.citiesService.getAllCached();
      this.showCityFilter = true;
    }, 1);
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
      this.updateBranch();
    } else {
      this.createBranch();
    }
  }

  private createBranch(): void {
    const createDto: CreateCompanyBranchDto = {
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      nickNameEn: this.form.value.nickNameEn || undefined,
      nickNameAr: this.form.value.nickNameAr || undefined,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      openingDate: this.form.value.openingDate,
      companyId: this.data.companyId,
      cityId: this.form.value.cityId
    };

    this.companyBranchesService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyBranches.branchCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateBranch(): void {
    const updateDto: UpdateCompanyBranchDto = {
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      nickNameEn: this.form.value.nickNameEn || undefined,
      nickNameAr: this.form.value.nickNameAr || undefined,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      openingDate: this.form.value.openingDate,
      companyId: this.data.companyId,
      cityId: this.form.value.cityId
    };

    this.companyBranchesService.update(this.data.branch!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyBranches.branchUpdated'));
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
