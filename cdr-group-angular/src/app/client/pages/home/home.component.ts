import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
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
  private router = inject(Router);
  private translationService = inject(TranslationService);
  companyState = inject(CompanyStateService);

  get selectedCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  get selectedCompanyId(): string {
    return this.companyState.selectedCompany?.id || '';
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  onCompanyChange(companyId: string): void {
    this.router.navigate(['/', companyId]);
  }
}
