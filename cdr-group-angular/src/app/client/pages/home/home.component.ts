import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslationService } from '../../../services/translation.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private translationService = inject(TranslationService);
  companyState = inject(CompanyStateService);

  get selectedCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  get selectedCompanyCode(): string {
    return this.companyState.selectedCompany?.code || 'CDR';
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  onCompanyChange(companyCode: string): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { company: companyCode },
      queryParamsHandling: 'merge'
    });
  }
}
