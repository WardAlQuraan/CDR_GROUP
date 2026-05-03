import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { Router } from '@angular/router';
import { CompaniesService } from '../../../services/companies.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-sub-companies',
  standalone: false,
  templateUrl: './sub-companies.component.html',
  styleUrl: './sub-companies.component.scss',
})
export class SubCompaniesComponent implements OnChanges {
  @Input() companyId = '';

  private companiesService = inject(CompaniesService);
  private companyState = inject(CompanyStateService);
  private translationService = inject(TranslationService);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  children: CompanyDto[] = [];
  loading = false;

  constructor() {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadChildren();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasChildren(): boolean {
    return this.children.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
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
    this.companyState.setSelectedCompany(company);
    this.router.navigate(['/', company.id, 'group']);
  }

  private loadChildren(): void {
    if (this.companyState.companies.length) {
      const root = this.findCompany(this.companyState.companies, this.companyId);
      this.children = root?.children ?? [];
      this.cdr.markForCheck();
      return;
    }

    this.loading = true;
    this.companiesService.getTree().subscribe({
      next: (response) => {
        if (response.success && response.data?.length) {
          const root = this.findCompany(response.data, this.companyId);
          this.children = root?.children ?? [];
        } else {
          this.children = [];
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.children = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private findCompany(tree: CompanyDto[], id: string): CompanyDto | undefined {
    for (const company of tree) {
      if (company.id === id) return company;
      if (company.children?.length) {
        const found = this.findCompany(company.children, id);
        if (found) return found;
      }
    }
    return undefined;
  }
}
