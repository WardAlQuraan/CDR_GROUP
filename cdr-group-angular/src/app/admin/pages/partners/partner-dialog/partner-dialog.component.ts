import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { PartnerDto, CreatePartnerDto, UpdatePartnerDto } from '../../../../models/partner.model';
import { CompanyDto } from '../../../../models/company.model';
import { CityDto } from '../../../../models/city.model';
import { CountryDto } from '../../../../models/country.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';
import { PartnersService } from '../../../../services/partners.service';
import { CompaniesService } from '../../../../services/companies.service';
import { CitiesService } from '../../../../services/cities.service';
import { CountriesService } from '../../../../services/countries.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type PartnerDialogMode = 'create' | 'edit';

export interface PartnerDialogData {
  mode: PartnerDialogMode;
  partner?: PartnerDto;
}

@Component({
  selector: 'app-partner-dialog',
  standalone: false,
  templateUrl: './partner-dialog.component.html',
  styleUrl: './partner-dialog.component.scss'
})
export class PartnerDialogComponent implements OnInit {
  form!: FormGroup;
  mode: PartnerDialogMode;
  loading = false;
  showCityFilter = false;
  companiesDataSource$!: Observable<ApiResponse<CompanyDto[]>>;
  countriesDataSource$!: Observable<ApiResponse<CountryDto[]>>;
  citiesDataSource$!: Observable<ApiResponse<CityDto[]>>;

  selectedCountryId: string | null = null;

  statusOptions = [
    { value: 'Present', label: 'admin.partners.present' },
    { value: 'Available', label: 'admin.partners.available' },
    // { value: 'NotAvailable', label: 'admin.partners.notAvailable' }
  ];

  companyMapper = (company: CompanyDto): SelectOption => ({
    value: company.id,
    label: this.isArabic ? company.nameAr : company.nameEn
  });

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
    private dialogRef: MatDialogRef<PartnerDialogComponent>,
    private partnersService: PartnersService,
    private companiesService: CompaniesService,
    private citiesService: CitiesService,
    private countriesService: CountriesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: PartnerDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.companiesDataSource$ = this.companiesService.getAll();
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
    return this.isCreateMode ? 'admin.partners.createPartner' : 'admin.partners.editPartner';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const partner = this.data.partner!;
      this.form = this.fb.group({
        companyId: [partner.companyId, [Validators.required]],
        countryId: [null],
        cityId: [partner.cityId, [Validators.required]],
        status: [partner.status, [Validators.required]]
      });
      // Resolve countryId from the city data
      this.citiesService.getAllCached().subscribe(res => {
        const city = res.data?.find(c => c.id === partner.cityId);
        if (city) {
          this.selectedCountryId = city.countryId;
          this.form.get('countryId')?.setValue(city.countryId);
        }
        this.showCityFilter = true;
        this.cdr.markForCheck();
      });
    } else {
      this.form = this.fb.group({
        companyId: [null, [Validators.required]],
        countryId: [null],
        cityId: [null, [Validators.required]],
        status: ['Present', [Validators.required]]
      });
    }
  }

  onCountryChange(countryId: string | null): void {
    this.selectedCountryId = countryId;
    this.form.get('cityId')?.setValue(null);
    this.showCityFilter = false;
    setTimeout(() => {
      this.citiesDataSource$ = this.citiesService.getAllCached();
      this.showCityFilter = true;
    },1);
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
      this.updatePartner();
    } else {
      this.createPartner();
    }
  }

  private createPartner(): void {
    const createDto: CreatePartnerDto = {
      companyId: this.form.value.companyId,
      cityId: this.form.value.cityId,
      status: this.form.value.status
    };

    this.partnersService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.partners.partnerCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updatePartner(): void {
    const updateDto: UpdatePartnerDto = {
      companyId: this.form.value.companyId,
      cityId: this.form.value.cityId,
      status: this.form.value.status
    };

    this.partnersService.update(this.data.partner!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.partners.partnerUpdated'));
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
