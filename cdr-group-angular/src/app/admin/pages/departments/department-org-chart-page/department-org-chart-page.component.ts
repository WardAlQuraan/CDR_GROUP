import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslationService } from '../../../../services/translation.service';
import { DepartmentsService } from '../../../../services/departments.service';
import { DepartmentDto } from '../../../../models/department.model';

@Component({
  selector: 'app-department-org-chart-page',
  standalone: false,
  templateUrl: './department-org-chart-page.component.html',
  styleUrl: './department-org-chart-page.component.scss'
})
export class DepartmentOrgChartPageComponent implements OnInit {
  departmentId: string | null = null;
  department: DepartmentDto | null = null;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private translationService: TranslationService,
    private departmentsService: DepartmentsService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.departmentId = this.route.snapshot.paramMap.get('id');
    if (this.departmentId) {
      this.loadDepartment();
    } else {
      this.loading = false;
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get departmentName(): string {
    if (!this.department) return '';
    return this.isArabic ? this.department.nameAr : this.department.nameEn;
  }

  private loadDepartment(): void {
    this.departmentsService.getById(this.departmentId!).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.department = response.data;
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }
}
