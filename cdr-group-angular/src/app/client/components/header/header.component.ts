import { Component, HostListener, OnInit, signal, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslationService } from '../../../services/translation.service';
import { AuthService } from '../../../services/auth.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { CompanyDto } from '../../../models/company.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  translationService = inject(TranslationService);
  authService = inject(AuthService);
  companyState = inject(CompanyStateService);
  isSticky = signal(false);
  activeSection = signal('home');
  selectedCompanyId = '';

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

  navigateToCompany(companyId: string): void {
    this.router.navigate(['/', companyId]);
    this.closeNavbar();
  }

  closeNavbar(): void {
    const navbarCollapse = document.getElementById('navbarNav');
    if (navbarCollapse?.classList.contains('show')) {
      const toggler = document.querySelector('.navbar-toggler') as HTMLElement;
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
    const company = this.companyState.findCompany(this.companyState.companies, this.selectedCompanyId);
    const logo = company?.logo;
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
        this.router.navigate(['/', this.selectedCompanyId]);
      }
    });
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

    // Default to home only when on the home page
    this.activeSection.set(this.isHomePage ? 'home' : '');
  }

  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  onLogout() {
    this.authService.logout();
  }

  navigateToSection(sectionId: string) {
    // Check if element exists on current page (means we're on home)
    const element = document.getElementById(sectionId);

    if (element) {
      // Element exists, scroll to it
      element.scrollIntoView({ behavior: 'smooth' });
    } else {
      // Navigate to home page with fragment
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
