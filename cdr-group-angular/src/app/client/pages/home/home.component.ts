import { ChangeDetectorRef, Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Router } from '@angular/router';
import { TranslationService } from '../../../services/translation.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { CompanyHomeComponentSetupsService } from '../../../services/company-home-component-setups.service';
import { CompanyDto } from '../../../models/company.model';
import { CompanyHomeComponentSetupDto } from '../../../models/company-home-component-setup.model';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  private router = inject(Router);
  private translationService = inject(TranslationService);
  private homeComponentSetupsService = inject(CompanyHomeComponentSetupsService);
  private destroyRef = inject(DestroyRef);
  private cdr = inject(ChangeDetectorRef);
  companyState = inject(CompanyStateService);

  setups: CompanyHomeComponentSetupDto[] = [];

  get selectedCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  get selectedCompanyId(): string {
    return this.companyState.selectedCompany?.id || '';
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  ngOnInit(): void {
    this.companyState.selectedCompany$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(company => {
        if (!company?.id) {
          this.setups = [];
          this.cdr.markForCheck();
          return;
        }
        this.loadSetups(company.id);
      });
  }

  private loadSetups(companyId: string): void {
    this.homeComponentSetupsService.getByCompany(companyId).subscribe({
      next: response => {
        if (response.success && response.data) {
          this.setups = [...response.data].sort((a, b) => a.rank - b.rank);
        } else {
          this.setups = [];
        }
        this.cdr.markForCheck();
      },
      error: () => {
        this.setups = [];
        this.cdr.markForCheck();
      }
    });
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  onCompanyChange(companyId: string): void {
    this.router.navigate(['/', companyId]);
  }
}
