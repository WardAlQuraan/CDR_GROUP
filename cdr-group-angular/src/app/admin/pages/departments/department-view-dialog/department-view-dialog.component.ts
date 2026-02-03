import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DepartmentDto } from '../../../../models/department.model';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-department-view-dialog',
  standalone: false,
  templateUrl: './department-view-dialog.component.html',
  styleUrl: './department-view-dialog.component.scss'
})
export class DepartmentViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<DepartmentViewDialogComponent>,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public department: DepartmentDto
  ) {}

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getParentDepartmentName(): string {
    if (!this.department.parentDepartment) return '-';
    return this.isArabic ? this.department.parentDepartment.nameAr : this.department.parentDepartment.nameEn;
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
