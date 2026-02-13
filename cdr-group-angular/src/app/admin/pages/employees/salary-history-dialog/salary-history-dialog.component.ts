import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeDto } from '../../../../models/employee.model';
import { SalaryHistoryDto } from '../../../../models/salary-history.model';
import { SalaryHistoriesService } from '../../../../services/salary-histories.service';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-salary-history-dialog',
  standalone: false,
  templateUrl: './salary-history-dialog.component.html',
  styleUrl: './salary-history-dialog.component.scss'
})
export class SalaryHistoryDialogComponent implements OnInit {
  salaryHistories: SalaryHistoryDto[] = [];
  loading = false;

  displayedColumns = ['effectiveDate', 'oldSalary', 'newSalary', 'reason', 'createdAt'];

  constructor(
    private dialogRef: MatDialogRef<SalaryHistoryDialogComponent>,
    private salaryHistoriesService: SalaryHistoriesService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public employee: EmployeeDto
  ) {}

  ngOnInit(): void {
    this.loadHistory();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get employeeName(): string {
    return this.isArabic ? this.employee.fullNameAr : this.employee.fullNameEn;
  }

  private loadHistory(): void {
    this.loading = true;
    this.salaryHistoriesService.getByEmployee(this.employee.id).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.salaryHistories = response.data;
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
