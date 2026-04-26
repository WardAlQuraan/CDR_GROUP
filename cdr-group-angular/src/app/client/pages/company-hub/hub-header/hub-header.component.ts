import { ChangeDetectorRef, Component, HostListener, OnInit, signal, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslationService } from '../../../../services/translation.service';
import { AuthService } from '../../../../services/auth.service';
import { CompaniesService } from '../../../../services/companies.service';
import { CompanyStateService } from '../../../../services/company-state.service';
import { CompanyDto } from '../../../../models/company.model';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-hub-header',
  standalone: false,
  templateUrl: './hub-header.component.html',
  styleUrl: './hub-header.component.scss',
})
export class HubHeaderComponent implements OnInit {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private companiesService = inject(CompaniesService);
  private cdr = inject(ChangeDetectorRef);
  translationService = inject(TranslationService);
  authService = inject(AuthService);
  companyState = inject(CompanyStateService);
  isSticky = signal(false);
  activeSection = signal('home');
  selectedCompanyId = '';
  headerCompanies: CompanyDto[] = [];
  private loadedForId?: string;

  private sections = ['section_2', 'section_events', 'section_team', 'section_5'];

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get isHomePage(): boolean {
    return this.router.url === '/' || this.router.url.startsWith('/?') || this.router.url.startsWith('/#');
  }

  get isCompanyRoute(): boolean {
    return this.router.url.includes('company=');
  }

  get headerBg(): string | null {
    const selected = this.companyState.selectedCompany;
    const fromState = selected?.id === this.selectedCompanyId ? selected?.secondaryColor : undefined;
    const fromTree = this.companyState.findCompany(this.companyState.companies, this.selectedCompanyId)?.primaryColor;
    return fromState || fromTree || null;
  }

  get logoBg(): string | null {
    const selected = this.companyState.selectedCompany;
    const fromState = selected?.id === this.selectedCompanyId ? selected?.primaryColor : undefined;
    const fromTree = this.companyState.findCompany(this.companyState.companies, this.selectedCompanyId)?.secondaryColor;
    return fromState || fromTree || null;
  }

  goBack(): void {
    const selected = this.companyState.selectedCompany;
    if (selected?.parentId) {
      this.router.navigate(['/', selected.parentId, 'group']);
    } else {
      this.router.navigate(['/']);
    }
    this.closeNavbar();
  }

  navigateToCompany(companyId: string): void {
    const company = this.companyState.findCompany(this.companyState.companies, companyId);
    if (company?.children?.length) {
      this.router.navigate(['/', companyId, 'group']);
    } else {
      this.router.navigate(['/', companyId]);
    }
    this.closeNavbar();
  }

  closeNavbar(): void {
    const navbarCollapse = document.getElementById('hubNavbarNav');
    if (navbarCollapse?.classList.contains('show')) {
      const toggler = document.querySelector('.hub-header .navbar-toggler') as HTMLElement;
      toggler?.click();
    }
  }

  get selectedCompanyName(): string {
    const company = this.companyState.findCompany(this.companyState.companies, this.selectedCompanyId);
    if (company) {
      return this.getCompanyName(company);
    }
    return '';
  }

  get selectedCompanyLogo(): string {
    const selected = this.companyState.selectedCompany;
    const fromState = selected?.id === this.selectedCompanyId ? selected?.logo : undefined;
    const fromTree = this.companyState.findCompany(this.companyState.companies, this.selectedCompanyId)?.logo;
    const logo = fromState || fromTree;
    if (!logo) return 'assets/images/logo.jpg';
    if (/^https?:\/\//i.test(logo)) return logo;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${logo.startsWith('/') ? '' : '/'}${logo}`;
  }

  ngOnInit() {
    this.updateActiveSection();
    this.route.paramMap.subscribe(params => {
      const companyId = params.get('companyId');
      if (companyId) {
        this.selectedCompanyId = companyId;
      } else if (this.companyState.companies.length > 0) {
        const lastId = this.companyState.getLastSelectedCompanyId() || environment.defaultCompanyId;
        const lastCompany = lastId ? this.companyState.findCompany(this.companyState.companies, lastId) : undefined;
        const resolved = lastCompany || this.companyState.companies[0];
        this.selectedCompanyId = this.companyState.getLeafCompany(resolved).id;
      }
    });
  }

  private loadHeaderCompanies(selected?: CompanyDto): void {
    if (!selected) return;
    const contextId = selected.parentId || selected.id;
    if (this.loadedForId === contextId) return;
    this.loadedForId = contextId;
    this.companiesService.getTreeByCompanyId(contextId).subscribe(response => {
      if (response.success && response.data) {
        this.headerCompanies = response.data;
        this.cdr.markForCheck();
      }
    });
  }

  get flattenedCompanies(): CompanyDto[] {
    const result: CompanyDto[] = [];
    const walk = (list: CompanyDto[]) => {
      for (const c of list) {
        if (c.children?.length) {
          walk(c.children);
        } else {
          result.push(c);
        }
      }
    };
    walk(this.headerCompanies);
    return result;
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  hasChildren(company: CompanyDto): boolean {
    return company.children && company.children.length > 0;
  }

  private openSubmenus = new Set<string>();

  openSubmenu(companyId: string): void {
    this.openSubmenus.add(companyId);
  }

  closeSubmenu(companyId: string): void {
    this.openSubmenus.delete(companyId);
  }

  toggleSubmenu(companyId: string): void {
    if (this.openSubmenus.has(companyId)) {
      this.openSubmenus.delete(companyId);
    } else {
      this.openSubmenus.add(companyId);
    }
  }

  isSubmenuOpen(companyId: string): boolean {
    return this.openSubmenus.has(companyId);
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isSticky.set(window.scrollY > 100);
    this.updateActiveSection();
  }

  private updateActiveSection() {
    const scrollPosition = window.scrollY + 150;

    for (const sectionId of this.sections) {
      const element = document.getElementById(sectionId);
      if (element) {
        const offsetTop = element.offsetTop;
        const offsetBottom = offsetTop + element.offsetHeight;

        if (scrollPosition >= offsetTop && scrollPosition < offsetBottom) {
          this.activeSection.set(sectionId);
          return;
        }
      }
    }

    this.activeSection.set(this.isHomePage ? 'home' : '');
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  onLogout() {
    this.authService.logout();
  }

  navigateToSection(sectionId: string) {
    const element = document.getElementById(sectionId);

    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    } else {
      this.router.navigate(['/', this.selectedCompanyId], { fragment: sectionId }).then(() => {
        setTimeout(() => {
          const el = document.getElementById(sectionId);
          if (el) {
            el.scrollIntoView({ behavior: 'smooth' });
          }
        }, 100);
      });
    }
  }
}
