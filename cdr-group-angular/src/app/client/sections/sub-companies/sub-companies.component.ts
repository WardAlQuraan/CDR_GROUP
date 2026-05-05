import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CompaniesService } from '../../../services/companies.service';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
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
  private companyPreferencesService = inject(CompanyPreferencesService);
  private companyState = inject(CompanyStateService);
  private translationService = inject(TranslationService);
  private router = inject(Router);
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);

  private static readonly DESCRIPTION_CODE = 'SUB_COMPANY_SECTION_DESCRIPTION';

  children: CompanyDto[] = [];
  loading = false;
  loadingDescriptions = false;
  // Per-child preference overrides for the description. Falls back to the
  // company's own descriptionEn/Ar when a code-specific preference is missing.
  private descriptionByChild = new Map<string, { en: string; ar: string }>();
  // Tracks which children have had their description preference resolved
  // (whether or not one was found) so per-card spinners can stop individually.
  private resolvedDescriptionIds = new Set<string>();

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
    const override = this.descriptionByChild.get(company.id);
    const overrideValue = this.isArabic ? override?.ar : override?.en;
    if (overrideValue) return overrideValue;
    return (this.isArabic ? company.descriptionAr : company.descriptionEn) || '';
  }

  // Description from preferences may contain trusted HTML (CMS-authored),
  // so render it via [innerHTML] using DomSanitizer.
  getSafeCompanyDescription(company: CompanyDto): SafeHtml | null {
    const desc = this.getCompanyDescription(company);
    return desc ? this.sanitizer.bypassSecurityTrustHtml(desc) : null;
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
    this.router.navigate(['/', company.id]);
  }

  private loadChildren(): void {
    if (this.companyState.companies.length) {
      const root = this.findCompany(this.companyState.companies, this.companyId);
      this.children = root?.children ?? [];
      this.loadDescriptionOverrides();
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
        this.loadDescriptionOverrides();
        this.cdr.markForCheck();
      },
      error: () => {
        this.children = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  isDescriptionLoading(child: CompanyDto): boolean {
    return this.loadingDescriptions && !this.resolvedDescriptionIds.has(child.id);
  }

  private loadDescriptionOverrides(): void {
    this.descriptionByChild.clear();
    this.resolvedDescriptionIds.clear();
    if (!this.children.length) {
      this.loadingDescriptions = false;
      return;
    }

    const code = SubCompaniesComponent.DESCRIPTION_CODE;
    this.loadingDescriptions = true;
    let pending = this.children.length;

    // Per-child subscription so each card's spinner can stop on its own
    // request completion instead of waiting for the slowest one.
    for (const child of this.children) {
      this.companyPreferencesService.getByCompanyAndCode(child.id, code).pipe(
        map(response => ({
          en: response.success && response.data?.id ? (response.data.valueEn || '') : '',
          ar: response.success && response.data?.id ? (response.data.valueAr || '') : ''
        })),
        catchError(() => of({ en: '', ar: '' }))
      ).subscribe(result => {
        if (result.en || result.ar) {
          this.descriptionByChild.set(child.id, { en: result.en, ar: result.ar });
        }
        this.resolvedDescriptionIds.add(child.id);
        pending--;
        if (pending <= 0) {
          this.loadingDescriptions = false;
        }
        this.cdr.markForCheck();
      });
    }
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
