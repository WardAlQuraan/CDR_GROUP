import { ChangeDetectorRef, Component, OnInit, effect } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompaniesService } from '../../../services/companies.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-company-detail',
  standalone: false,
  templateUrl: './company-detail.component.html',
  styleUrl: './company-detail.component.scss',
})
export class CompanyDetailComponent implements OnInit {
  loading = true;
  error = false;
  company: CompanyDto | null = null;
  companyCode: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private companiesService: CompaniesService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const code = params.get('code');
      if (code && code !== this.companyCode) {
        this.companyCode = code;
        this.company = null;
        this.loading = true;
        this.error = false;
        this.loadCompany();
      } else if (!code) {
        this.loading = false;
        this.error = true;
      }
    });
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyName(): string {
    if (!this.company) return '';
    return this.isArabic ? this.company.nameAr : this.company.nameEn;
  }

  get companyDescription(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.descriptionAr : this.company.descriptionEn) || '';
  }

  private loadCompany(): void {
    this.companiesService.getByCode(this.companyCode!).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.company = response.data;
        } else {
          this.error = true;
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.error = true;
        this.cdr.detectChanges();
      }
    });
  }
}
