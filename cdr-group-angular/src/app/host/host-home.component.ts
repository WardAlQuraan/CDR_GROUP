import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CompaniesService } from '../services/companies.service';
import { TranslationService } from '../services/translation.service';
import { CompanyDto } from '../models/company.model';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-host-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './host-home.component.html',
  styleUrl: './host-home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HostHomeComponent implements OnInit {
  private companiesService = inject(CompaniesService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);
  translationService = inject(TranslationService);

  rootCompany?: CompanyDto;
  children: CompanyDto[] = [];
  allCompanies: CompanyDto[] = [];
  loading = true;

  ngOnInit(): void {
    this.companiesService.getTree().subscribe({
      next: (response) => {
        if (response.success && response.data?.length) {
          this.rootCompany = response.data[0];
          this.children = response.data.slice(1);
          this.allCompanies = this.flatten(response.data);
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

  private flatten(companies: CompanyDto[]): CompanyDto[] {
    const result: CompanyDto[] = [];
    for (const company of companies) {
      result.push(company);
      if (company.children?.length) {
        result.push(...this.flatten(company.children));
      }
    }
    return result;
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get marqueeCompanies(): CompanyDto[] {
    return this.isArabic ? [...this.allCompanies].reverse() : this.allCompanies;
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  getCompanyTitle(company: CompanyDto): string {
    return (this.isArabic ? company.titleAr : company.titleEn) || '';
  }

  getLogoUrl(company?: CompanyDto): string {
    const logo = company?.logo;
    if (!logo) return 'assets/images/logo.jpg';
    if (/^https?:\/\//i.test(logo)) return logo;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${logo.startsWith('/') ? '' : '/'}${logo}`;
  }

  selectCompany(company: CompanyDto): void {
    this.router.navigate(['/', company.id]);
  }

  selectRoot(): void {
    if (this.rootCompany) this.selectCompany(this.rootCompany);
  }
}
