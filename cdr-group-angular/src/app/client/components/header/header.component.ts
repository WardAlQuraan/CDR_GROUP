import { Component, HostListener, OnInit, signal, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslationService } from '../../../services/translation.service';
import { AuthService } from '../../../services/auth.service';
import { CompaniesService } from '../../../services/companies.service';
import { CompanyDto } from '../../../models/company.model';

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
  private companiesService = inject(CompaniesService);

  isSticky = signal(false);
  activeSection = signal('home');
  companies: CompanyDto[] = [];
  selectedCompanyCode = 'CDR';

  private sections = ['section_2', 'section_events', 'section_team', 'section_5'];

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get isCompanyRoute(): boolean {
    return this.router.url.includes('company=');
  }

  navigateToCompany(companyCode: string): void {
    this.router.navigate(['/'], { queryParams: { company: companyCode } });
  }

  get selectedCompanyName(): string {
    const company = this.companies.find(c => c.code === this.selectedCompanyCode);
    if (company) {
      return this.getCompanyName(company);
    }
    return this.selectedCompanyCode;
  }

  ngOnInit() {
    this.updateActiveSection();
    this.loadCompanies();
    this.route.queryParams.subscribe(params => {
      if (params['company']) {
        this.selectedCompanyCode = params['company'];
      }
    });
  }

  private loadCompanies(): void {
    this.companiesService.getActiveCompanies().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.companies = response.data;
        }
      }
    });
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
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

    // Default to home when at the top or no section is active
    this.activeSection.set('home');
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
      this.router.navigate(['/'], { fragment: sectionId }).then(() => {
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
