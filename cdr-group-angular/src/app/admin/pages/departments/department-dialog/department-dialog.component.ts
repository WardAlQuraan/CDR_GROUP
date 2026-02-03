import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DepartmentDto, DepartmentBasicDto, CreateDepartmentDto, UpdateDepartmentDto } from '../../../../models/department.model';
import { DepartmentsService } from '../../../../services/departments.service';
import { EmployeesService } from '../../../../services/employees.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { EmployeeDto } from '../../../../models/employee.model';

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
  loadingDepartments = false;
  loadingEmployees = false;

  departments: DepartmentDto[] = [];
  employees: EmployeeDto[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DepartmentDialogComponent>,
    private departmentsService: DepartmentsService,
    private employeesService: EmployeesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: DepartmentDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.initForm();
    this.loadDependencies();
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
        managerId: [dept.managerId]
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
        managerId: [null]
      });
    }
  }

  private loadDependencies(): void {
    this.loadDepartments();
    this.loadEmployees();
  }

  private loadDepartments(): void {
    this.loadingDepartments = true;
    this.departmentsService.getActiveDepartments().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          // Exclude current department from parent list
          this.departments = response.data.filter(d => d.id !== this.data.department?.id);
        }
        this.loadingDepartments = false;
      },
      error: () => {
        this.loadingDepartments = false;
      }
    });
  }

  private loadEmployees(): void {
    this.loadingEmployees = true;
    this.employeesService.getAll().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.employees = response.data.filter(e => e.isActive);
        }
        this.loadingEmployees = false;
      },
      error: () => {
        this.loadingEmployees = false;
      }
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  getDepartmentName(dept: DepartmentDto | DepartmentBasicDto): string {
    return this.isArabic ? dept.nameAr : dept.nameEn;
  }

  getEmployeeName(emp: EmployeeDto): string {
    return this.isArabic ? emp.fullNameAr : emp.fullNameEn;
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
      managerId: this.form.value.managerId || undefined
    };

    this.departmentsService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.departments.departmentCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.departments.errors.createFailed'));
        this.loading = false;
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
      managerId: this.form.value.managerId || undefined
    };

    this.departmentsService.update(this.data.department!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.departments.departmentUpdated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.departments.errors.updateFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
