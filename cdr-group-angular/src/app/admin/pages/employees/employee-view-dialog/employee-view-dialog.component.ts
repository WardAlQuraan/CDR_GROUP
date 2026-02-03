import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeDto } from '../../../../models/employee.model';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-employee-view-dialog',
  standalone: false,
  templateUrl: './employee-view-dialog.component.html',
  styleUrl: './employee-view-dialog.component.scss'
})
export class EmployeeViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<EmployeeViewDialogComponent>,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public employee: EmployeeDto
  ) {}

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getFullName(): string {
    return this.isArabic ? this.employee.fullNameAr : this.employee.fullNameEn;
  }

  getManagerName(): string {
    if (!this.employee.manager) return '-';
    return this.isArabic ? this.employee.manager.fullNameAr : this.employee.manager.fullNameEn;
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
