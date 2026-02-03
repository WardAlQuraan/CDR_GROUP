import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PositionDto } from '../../../../models/position.model';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-position-view-dialog',
  standalone: false,
  templateUrl: './position-view-dialog.component.html',
  styleUrl: './position-view-dialog.component.scss'
})
export class PositionViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<PositionViewDialogComponent>,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public position: PositionDto
  ) {}

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getDepartmentName(): string {
    if (!this.position.departmentId) return '-';
    return this.isArabic ? (this.position.departmentNameAr || '-') : (this.position.departmentNameEn || '-');
  }

  getSalaryRange(): string {
    if (!this.position.minSalary && !this.position.maxSalary) return '-';
    const min = this.position.minSalary?.toLocaleString() || '0';
    const max = this.position.maxSalary?.toLocaleString() || '∞';
    return `${min} - ${max}`;
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
