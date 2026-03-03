import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompaniesService } from '../../../services/companies.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  companies: CompanyDto[] = [];
  selectedCompany?: CompanyDto;
  selectedCompanyCode = 'CDR';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private companiesService: CompaniesService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const company = params['company'];
      if (company) {
        this.selectedCompanyCode = company;
      }
      this.loadSelectedCompany();
    });
    // this.loadCompanies();
  }

  private loadSelectedCompany(): void {
    this.companiesService.getByCode(this.selectedCompanyCode).subscribe({
      next: (response) => {
        debugger;
        if (response.success && response.data) {
          this.selectedCompany = response.data;
          this.cdr.markForCheck();
        }
      }
    });
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  onCompanyChange(companyCode: string): void {
    this.selectedCompanyCode = companyCode;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { company: companyCode },
      queryParamsHandling: 'merge'
    });
  }

  // private loadCompanies(): void {
  //   this.companiesService.getActiveCompanies().subscribe({
  //     next: (response) => {
  //       if (response.success && response.data) {
  //         this.companies = response.data;
  //       }
  //     }
  //   });
  // }
}
