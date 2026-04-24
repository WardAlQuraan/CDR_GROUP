import { ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CompaniesService } from '../services/companies.service';
import { CompanyStateService } from '../services/company-state.service';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-client-layout',
  standalone: false,
  templateUrl: './client-layout.component.html',
  styleUrl: './client-layout.component.scss',
})
export class ClientLayoutComponent implements OnInit, OnDestroy {

  constructor(
    private route: ActivatedRoute,
    private companiesService: CompaniesService,
    private companyState: CompanyStateService,
    private cdr: ChangeDetectorRef) {}

  private sub?: Subscription;
  loading = true;

  ngOnInit(): void {
    this.companiesService.getTree().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.companyState.setCompanies(response.data);
        }
        this.loading = false;
        this.cdr.markForCheck();
        this.subscribeToQueryParams();
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  private subscribeToQueryParams(): void {
    this.sub = this.route.paramMap.subscribe(params => {
      const companyId = params.get('companyId');
      if (companyId) {
        this.companyState.selectById(companyId);
      } else if (this.companyState.companies.length > 0) {
        const lastId = this.companyState.getLastSelectedCompanyId() || environment.defaultCompanyId;
        const lastCompany = lastId ? this.companyState.findCompany(this.companyState.companies, lastId) : undefined;
        const resolved = lastCompany || this.companyState.companies[0];
        this.companyState.setSelectedCompany(this.companyState.getLeafCompany(resolved));
      }
      const company = this.companyState.selectedCompany;
      if (company) {
        this.applyColors(company.primaryColor, company.secondaryColor);
      }
    });
  }

  private applyColors(primary?: string, secondary?: string): void {
    const root = document.documentElement;
    const p = primary || '#D9A93E';
    const s = secondary || '#3E423D';
    root.style.setProperty('--primary-color', p);
    root.style.setProperty('--custom-btn-bg-color', p);
    root.style.setProperty('--custom-btn-bg-hover-color', this.darkenColor(p, 15));
    root.style.setProperty('--link-hover-color', p);
    root.style.setProperty('--secondary-color', s);
  }

  private darkenColor(hex: string, percent: number): string {
    const num = parseInt(hex.replace('#', ''), 16);
    const r = Math.max(0, (num >> 16) - Math.round(2.55 * percent));
    const g = Math.max(0, ((num >> 8) & 0x00FF) - Math.round(2.55 * percent));
    const b = Math.max(0, (num & 0x0000FF) - Math.round(2.55 * percent));
    return `#${(r << 16 | g << 8 | b).toString(16).padStart(6, '0')}`;
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
    const root = document.documentElement;
    root.style.removeProperty('--primary-color');
    root.style.removeProperty('--custom-btn-bg-color');
    root.style.removeProperty('--custom-btn-bg-hover-color');
    root.style.removeProperty('--link-hover-color');
    root.style.removeProperty('--secondary-color');
  }
}
