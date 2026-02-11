import { Component, Inject, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, Subject } from 'rxjs';
import { EmployeeDto, CreateEmployeeDto, UpdateEmployeeDto } from '../../../../models/employee.model';
import { DepartmentDto } from '../../../../models/department.model';
import { PositionDto } from '../../../../models/position.model';
import { EmployeesService } from '../../../../services/employees.service';
import { DepartmentsService } from '../../../../services/departments.service';
import { PositionsService } from '../../../../services/positions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';
import { ApiResponse } from '../../../../models/api-response.model';

export type EmployeeDialogMode = 'create' | 'edit';

export interface EmployeeDialogData {
  mode: EmployeeDialogMode;
  employee?: EmployeeDto;
}

@Component({
  selector: 'app-employee-dialog',
  standalone: false,
  templateUrl: './employee-dialog.component.html',
  styleUrl: './employee-dialog.component.scss'
})
export class EmployeeDialogComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  mode: EmployeeDialogMode;
  loading = false;

  // Data sources for async-selects (cached to prevent infinite loops)
  departmentsDataSource$!: Observable<ApiResponse<DepartmentDto[]>>;
  positionsDataSource$!: Observable<ApiResponse<PositionDto[]>>;
  managersDataSource$!: Observable<ApiResponse<EmployeeDto[]>>;

  private destroy$ = new Subject<void>();

  // Mapper functions for async-select
  departmentMapper = (dept: DepartmentDto): SelectOption => ({
    value: dept.id,
    label: `${this.isArabic ? dept.nameAr : dept.nameEn} (${(this.isArabic ? dept.companyNameAr : dept.companyName) || dept.code})`
  });

  positionMapper = (pos: PositionDto): SelectOption => ({
    value: pos.id,
    label: `${this.isArabic ? pos.nameAr : pos.nameEn} (${pos.code})`
  });

  positionFilter = (pos: PositionDto): boolean => pos.isActive;

  managerMapper = (emp: EmployeeDto): SelectOption => ({
    value: emp.id,
    label: `${this.isArabic ? emp.fullNameAr : emp.fullNameEn} (${emp.employeeCode})`
  });

  // Filter function for managers
  managerFilter = (emp: EmployeeDto): boolean =>
    emp.id !== this.data.employee?.id && emp.isActive;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeDialogComponent>,
    public employeesService: EmployeesService,
    public departmentsService: DepartmentsService,
    public positionsService: PositionsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: EmployeeDialogData,
    private cdr: ChangeDetectorRef
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.initForm();
    this.initDataSources();
  }

  private initDataSources(): void {
    // Cache observables to prevent infinite loops from method calls in template
    this.departmentsDataSource$ = this.departmentsService.getActiveDepartments();
    this.managersDataSource$ = this.employeesService.getAll();
    // Don't reset position on initial load (preserves existing value in edit mode)
    this.onDepartmentChange(this.form.get('departmentId')?.value, false);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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
    return this.isCreateMode ? 'admin.employees.createEmployee' : 'admin.employees.editEmployee';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const emp = this.data.employee!;
      this.form = this.fb.group({
        employeeCode: [emp.employeeCode, [Validators.required, Validators.maxLength(50)]],
        firstNameEn: [emp.firstNameEn, [Validators.required, Validators.maxLength(100)]],
        lastNameEn: [emp.lastNameEn, [Validators.required, Validators.maxLength(100)]],
        firstNameAr: [emp.firstNameAr, [Validators.required, Validators.maxLength(100)]],
        lastNameAr: [emp.lastNameAr, [Validators.required, Validators.maxLength(100)]],
        email: [emp.email, [Validators.email, Validators.maxLength(256)]],
        phone: [emp.phone, [Validators.maxLength(20)]],
        departmentId: [emp.departmentId, [Validators.required]],
        positionId: [emp.positionId],
        hireDate: [emp.hireDate ? new Date(emp.hireDate) : null],
        salary: [emp.salary, [Validators.min(0)]],
        isActive: [emp.isActive],
        managerId: [emp.managerId]
      });
    } else {
      this.form = this.fb.group({
        employeeCode: ['', [Validators.required, Validators.maxLength(50)]],
        firstNameEn: ['', [Validators.required, Validators.maxLength(100)]],
        lastNameEn: ['', [Validators.required, Validators.maxLength(100)]],
        firstNameAr: ['', [Validators.required, Validators.maxLength(100)]],
        lastNameAr: ['', [Validators.required, Validators.maxLength(100)]],
        email: ['', [Validators.email, Validators.maxLength(256)]],
        phone: ['', [Validators.maxLength(20)]],
        departmentId: [null, [Validators.required]],
        positionId: [null],
        hireDate: [null],
        salary: [null, [Validators.min(0)]],
        isActive: [true],
        managerId: [null]
      });
    }
  }

  onDepartmentChange(departmentId: string | null, resetPosition = true): void {
    // Reset position when department changes (but not on initial load)
    if (resetPosition) {
      this.form.get('positionId')?.setValue(null);
    }
    this.positionsDataSource$ = departmentId
      ? this.positionsService.getByDepartmentId(departmentId)
      : this.positionsService.getAll();
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
      this.updateEmployee();
    } else {
      this.createEmployee();
    }
  }

  private createEmployee(): void {
    const createDto: CreateEmployeeDto = {
      employeeCode: this.form.value.employeeCode,
      firstNameEn: this.form.value.firstNameEn,
      lastNameEn: this.form.value.lastNameEn,
      firstNameAr: this.form.value.firstNameAr,
      lastNameAr: this.form.value.lastNameAr,
      email: this.form.value.email || undefined,
      phone: this.form.value.phone || undefined,
      departmentId: this.form.value.departmentId || undefined,
      positionId: this.form.value.positionId || undefined,
      hireDate: this.form.value.hireDate || undefined,
      salary: this.form.value.salary || undefined,
      isActive: this.form.value.isActive,
      managerId: this.form.value.managerId || undefined
    };

    this.employeesService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.employees.employeeCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.cdr.markForCheck();
        this.loading = false;
      }
    });
  }

  private updateEmployee(): void {
    const updateDto: UpdateEmployeeDto = {
      employeeCode: this.form.value.employeeCode,
      firstNameEn: this.form.value.firstNameEn,
      lastNameEn: this.form.value.lastNameEn,
      firstNameAr: this.form.value.firstNameAr,
      lastNameAr: this.form.value.lastNameAr,
      email: this.form.value.email || undefined,
      phone: this.form.value.phone || undefined,
      departmentId: this.form.value.departmentId || undefined,
      positionId: this.form.value.positionId || undefined,
      hireDate: this.form.value.hireDate || undefined,
      salary: this.form.value.salary || undefined,
      isActive: this.form.value.isActive,
      managerId: this.form.value.managerId || undefined
    };

    this.employeesService.update(this.data.employee!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.employees.employeeUpdated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.cdr.markForCheck();
        this.loading = false;

      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
