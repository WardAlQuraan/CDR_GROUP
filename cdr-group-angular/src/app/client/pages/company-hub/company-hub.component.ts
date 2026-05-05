import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompaniesService } from '../../../services/companies.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-company-hub',
  standalone: false,
  templateUrl: './company-hub.component.html',
  styleUrl: './company-hub.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CompanyHubComponent implements OnInit {
  private companiesService = inject(CompaniesService);
  private companyState = inject(CompanyStateService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);
  translationService = inject(TranslationService);

  parentCompany?: CompanyDto;
  children: CompanyDto[] = [];
  allCompanies: CompanyDto[] = [];
  loading = true;

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('companyId');
      if (!id) {
        this.router.navigate(['/']);
        return;
      }
      this.loadHub(id);
    });
  }

  private loadHub(companyId: string): void {
    this.loading = true;
    if (this.companyState.companies.length) {
      this.applyHub(companyId, this.companyState.companies);
      return;
    }
    this.companiesService.getTree().subscribe({
      next: (response) => {
        if (response.success && response.data?.length) {
          this.applyHub(companyId, response.data);
        } else {
          this.loading = false;
          this.cdr.markForCheck();
        }
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private applyHub(companyId: string, tree: CompanyDto[]): void {
    const found = this.companyState.findCompany(tree, companyId);
    if (!found) {
      this.router.navigate(['/']);
      return;
    }
    if (!found.parentId) {
      this.router.navigate(['/']);
      return;
    }
    this.parentCompany = found;
    this.companyState.setSelectedCompany(found);
    this.children = found.children;
    this.allCompanies = this.flatten(tree);
    this.loading = false;
    this.cdr.markForCheck();
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

  splitBrandName(company: CompanyDto): string[] {
    const name = this.getCompanyName(company) || '';
    if (this.isArabic) {
      return name.split(/(\s+)/).filter(s => s.length > 0);
    }
    return Array.from(name);
  }

  getCompanyTitle(company: CompanyDto): string {
    return (this.isArabic ? company.titleAr : company.titleEn) || '';
  }

  getCompanyDescription(company: CompanyDto): string {
    return (this.isArabic ? company.descriptionAr : company.descriptionEn) || '';
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

  goBack(): void {
    this.router.navigate(['/']);
  }
}
