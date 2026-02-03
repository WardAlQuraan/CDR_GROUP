import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeDto } from '../../../../models/employee.model';
import { EmployeesService } from '../../../../services/employees.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-employee-assign-manager-dialog',
  standalone: false,
  templateUrl: './employee-assign-manager-dialog.component.html',
  styleUrl: './employee-assign-manager-dialog.component.scss'
})
export class EmployeeAssignManagerDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  loadingManagers = false;
  managers: EmployeeDto[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeAssignManagerDialogComponent>,
    private employeesService: EmployeesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public employee: EmployeeDto
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadManagers();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initForm(): void {
    this.form = this.fb.group({
      managerId: [this.employee.managerId || null]
    });
  }

  private loadManagers(): void {
    this.loadingManagers = true;
    this.employeesService.getAll().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.managers = response.data.filter(e =>
            e.id !== this.employee.id && e.isActive
          );
        }
        this.loadingManagers = false;
      },
      error: () => {
        this.loadingManagers = false;
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

    this.employeesService.assignManager(this.employee.id, managerId).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.employees.managerAssigned'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.employees.errors.assignManagerFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
