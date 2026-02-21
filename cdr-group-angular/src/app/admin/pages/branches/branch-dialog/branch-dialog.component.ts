import { Component, Inject, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { BranchDto, CreateBranchDto, UpdateBranchDto } from '../../../../models/branch.model';
import { CompanyDto } from '../../../../models/company.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { BranchesService } from '../../../../services/branches.service';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { MapPickerComponent, MapLocation } from '../../../../shared/components/map-picker/map-picker.component';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';

export type BranchDialogMode = 'create' | 'edit';

export interface BranchDialogData {
  mode: BranchDialogMode;
  branch?: BranchDto;
}

@Component({
  selector: 'app-branch-dialog',
  standalone: false,
  templateUrl: './branch-dialog.component.html',
  styleUrl: './branch-dialog.component.scss'
})
export class BranchDialogComponent implements OnInit {
  @ViewChild(MapPickerComponent) mapPicker!: MapPickerComponent;

  form!: FormGroup;
  mode: BranchDialogMode;
  loading = false;

  companiesDataSource$!: Observable<ApiResponse<CompanyDto[]>>;

  companyMapper = (company: CompanyDto): SelectOption => ({
    value: company.id,
    label: this.isArabic ? company.nameAr : company.nameEn
  });

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<BranchDialogComponent>,
    private branchesService: BranchesService,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: BranchDialogData
  ) {
    this.mode = data.mode;

    this.dialogRef.afterOpened().subscribe(() => {
      setTimeout(() => {
        if (this.mapPicker) {
          this.mapPicker.invalidateMapSize();

          // If editing a branch with address but no coordinates, geocode the address
          if (this.isEditMode && data.branch?.address && data.branch.latitude == null && data.branch.longitude == null) {
            this.mapPicker.geocodeAddress(data.branch.address);
          }
        }
      }, 100);
    });
  }

  ngOnInit(): void {
    this.initForm();
    this.companiesDataSource$ = this.companiesService.getActiveCompanies();
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
    return this.isCreateMode ? 'admin.branches.createBranch' : 'admin.branches.editBranch';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const branch = this.data.branch!;
      const location: MapLocation | null = (branch.latitude != null && branch.longitude != null)
        ? { latitude: branch.latitude, longitude: branch.longitude, address: branch.address || '' }
        : null;

      this.form = this.fb.group({
        code: [branch.code, [Validators.required, Validators.maxLength(50)]],
        location: [location],
        isPrimary: [branch.isPrimary],
        isActive: [branch.isActive],
        companyId: [branch.companyId, [Validators.required]]
      });
    } else {
      this.form = this.fb.group({
        code: ['', [Validators.required, Validators.maxLength(50)]],
        location: [null],
        isPrimary: [false],
        isActive: [true],
        companyId: ['', [Validators.required]]
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
      this.updateBranch();
    } else {
      this.createBranch();
    }
  }

  private createBranch(): void {
    const location = this.form.value.location as MapLocation | null;
    const createDto: CreateBranchDto = {
      code: this.form.value.code,
      address: location?.address || undefined,
      latitude: location?.latitude || undefined,
      longitude: location?.longitude || undefined,
      isPrimary: this.form.value.isPrimary,
      isActive: this.form.value.isActive,
      companyId: this.form.value.companyId
    };

    this.branchesService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.branches.branchCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateBranch(): void {
    const location = this.form.value.location as MapLocation | null;
    const updateDto: UpdateBranchDto = {
      code: this.form.value.code,
      address: location?.address || undefined,
      latitude: location?.latitude || undefined,
      longitude: location?.longitude || undefined,
      isPrimary: this.form.value.isPrimary,
      isActive: this.form.value.isActive,
      companyId: this.form.value.companyId
    };

    this.branchesService.update(this.data.branch!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.branches.branchUpdated'));
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
