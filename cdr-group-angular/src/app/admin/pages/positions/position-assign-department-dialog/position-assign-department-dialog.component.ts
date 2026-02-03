import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { PositionDto } from '../../../../models/position.model';
import { DepartmentDto } from '../../../../models/department.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { PositionsService } from '../../../../services/positions.service';
import { DepartmentsService } from '../../../../services/departments.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';

@Component({
  selector: 'app-position-assign-department-dialog',
  standalone: false,
  templateUrl: './position-assign-department-dialog.component.html',
  styleUrl: './position-assign-department-dialog.component.scss'
})
export class PositionAssignDepartmentDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;

  departmentsDataSource$!: Observable<ApiResponse<DepartmentDto[]>>;

  departmentMapper = (dept: DepartmentDto): SelectOption => ({
    value: dept.id,
    label: `${this.isArabic ? dept.nameAr : dept.nameEn} (${dept.code})`
  });

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PositionAssignDepartmentDialogComponent>,
    private positionsService: PositionsService,
    private departmentsService: DepartmentsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public position: PositionDto
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.departmentsDataSource$ = this.departmentsService.getActiveDepartments();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initForm(): void {
    this.form = this.fb.group({
      departmentId: [this.position.departmentId || null]
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    this.loading = true;
    const departmentId = this.form.value.departmentId || null;

    this.positionsService.assignDepartment(this.position.id, departmentId).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.positions.departmentAssigned'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.positions.errors.assignDepartmentFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
