import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { DepartmentDto, CreateDepartmentDto, UpdateDepartmentDto } from '../../../../models/department.model';
import { DepartmentsService } from '../../../../services/departments.service';
import { EmployeesService } from '../../../../services/employees.service';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { EmployeeDto } from '../../../../models/employee.model';
import { CompanyDto } from '../../../../models/company.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';

export type DepartmentDialogMode = 'create' | 'edit';

export interface DepartmentDialogData {
  mode: DepartmentDialogMode;
  department?: DepartmentDto;
}

@Component({
  selector: 'app-department-dialog',
  standalone: false,
  templateUrl: './department-dialog.component.html',
  styleUrl: './department-dialog.component.scss'
})
export class DepartmentDialogComponent implements OnInit {
  form!: FormGroup;
  mode: DepartmentDialogMode;
  loading = false;

  // Data sources for async selects
  companies$: Observable<ApiResponse<CompanyDto[]>>;
  departments$: Observable<ApiResponse<DepartmentDto[]>>;
  employees$: Observable<ApiResponse<EmployeeDto[]>>;

  // Option mappers
  companyMapper = (company: CompanyDto): SelectOption => ({
    value: company.id,
    label: `${this.isArabic ? company.nameAr : company.nameEn} (${company.code})`
  });

  departmentMapper = (dept: DepartmentDto): SelectOption => ({
    value: dept.id,
    label: `${this.isArabic ? dept.nameAr : dept.nameEn} (${dept.code})`
  });

  employeeMapper = (emp: EmployeeDto): SelectOption => ({
    value: emp.id,
    label: `${this.isArabic ? emp.fullNameAr : emp.fullNameEn} (${emp.employeeCode})`
  });

  // Filter functions
  departmentFilter = (dept: DepartmentDto): boolean => dept.id !== this.data.department?.id;
  employeeFilter = (emp: EmployeeDto): boolean => emp.isActive;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DepartmentDialogComponent>,
    private departmentsService: DepartmentsService,
    private employeesService: EmployeesService,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: DepartmentDialogData,
    private cdr: ChangeDetectorRef
  ) {
    this.mode = data.mode;
    this.companies$ = this.companiesService.getActiveCompanies();
    this.departments$ = this.departmentsService.getActiveDepartments();
    this.employees$ = this.employeesService.getAll();
  }

  ngOnInit(): void {
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
    return this.isCreateMode ? 'admin.departments.createDepartment' : 'admin.departments.editDepartment';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const dept = this.data.department!;
      this.form = this.fb.group({
        code: [dept.code, [Validators.required, Validators.maxLength(50)]],
        nameEn: [dept.nameEn, [Validators.required, Validators.maxLength(200)]],
        nameAr: [dept.nameAr, [Validators.required, Validators.maxLength(200)]],
        descriptionEn: [dept.descriptionEn, [Validators.maxLength(500)]],
        descriptionAr: [dept.descriptionAr, [Validators.maxLength(500)]],
        isActive: [dept.isActive],
        parentDepartmentId: [dept.parentDepartmentId],
        managerId: [dept.managerId],
        companyId: [dept.companyId]
      });
    } else {
      this.form = this.fb.group({
        code: ['', [Validators.required, Validators.maxLength(50)]],
        nameEn: ['', [Validators.required, Validators.maxLength(200)]],
        nameAr: ['', [Validators.required, Validators.maxLength(200)]],
        descriptionEn: ['', [Validators.maxLength(500)]],
        descriptionAr: ['', [Validators.maxLength(500)]],
        isActive: [true],
        parentDepartmentId: [null],
        managerId: [null],
        companyId: [null]
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
      this.updateDepartment();
    } else {
      this.createDepartment();
    }
  }

  private createDepartment(): void {
    const createDto: CreateDepartmentDto = {
      code: this.form.value.code,
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      isActive: this.form.value.isActive,
      parentDepartmentId: this.form.value.parentDepartmentId || undefined,
      managerId: this.form.value.managerId || undefined,
      companyId: this.form.value.companyId || undefined
    };

    this.departmentsService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.departments.departmentCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateDepartment(): void {
    const updateDto: UpdateDepartmentDto = {
      code: this.form.value.code,
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      isActive: this.form.value.isActive,
      parentDepartmentId: this.form.value.parentDepartmentId || undefined,
      managerId: this.form.value.managerId || undefined,
      companyId: this.form.value.companyId || undefined
    };

    this.departmentsService.update(this.data.department!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.departments.departmentUpdated'));
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
