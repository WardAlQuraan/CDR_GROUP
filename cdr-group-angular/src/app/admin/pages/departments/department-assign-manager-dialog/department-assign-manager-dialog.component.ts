import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DepartmentDto } from '../../../../models/department.model';
import { EmployeeDto } from '../../../../models/employee.model';
import { DepartmentsService } from '../../../../services/departments.service';
import { EmployeesService } from '../../../../services/employees.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-department-assign-manager-dialog',
  standalone: false,
  templateUrl: './department-assign-manager-dialog.component.html',
  styleUrl: './department-assign-manager-dialog.component.scss'
})
export class DepartmentAssignManagerDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  loadingEmployees = false;
  employees: EmployeeDto[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<DepartmentAssignManagerDialogComponent>,
    private departmentsService: DepartmentsService,
    private employeesService: EmployeesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public department: DepartmentDto
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadEmployees();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initForm(): void {
    this.form = this.fb.group({
      managerId: [this.department.managerId || null]
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

  getEmployeeName(emp: EmployeeDto): string {
    return this.isArabic ? emp.fullNameAr : emp.fullNameEn;
  }

  onSubmit(): void {
    this.loading = true;
    const managerId = this.form.value.managerId || null;

    this.departmentsService.assignManager(this.department.id, managerId).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.departments.managerAssigned'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.departments.errors.assignManagerFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
