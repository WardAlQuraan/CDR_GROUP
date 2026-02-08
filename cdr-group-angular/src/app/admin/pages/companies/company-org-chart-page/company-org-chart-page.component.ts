import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslationService } from '../../../../services/translation.service';
import { CompaniesService } from '../../../../services/companies.service';
import { CompanyDto } from '../../../../models/company.model';

@Component({
  selector: 'app-company-org-chart-page',
  standalone: false,
  templateUrl: './company-org-chart-page.component.html',
  styleUrl: './company-org-chart-page.component.scss'
})
export class CompanyOrgChartPageComponent implements OnInit {
  companyCode: string | null = null;
  company: CompanyDto | null = null;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private translationService: TranslationService,
    private companiesService: CompaniesService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.companyCode = this.route.snapshot.paramMap.get('id');
    if (this.companyCode) {
      this.loadCompany();
    } else {
      this.loading = false;
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyName(): string {
    if (!this.company) return '';
    return this.isArabic ? this.company.nameAr : this.company.nameEn;
  }

  private loadCompany(): void {
    this.companiesService.getByCode(this.companyCode!).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.company = response.data;
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
