import { ChangeDetectorRef, Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
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
  private route = inject(ActivatedRoute);
  private translationService = inject(TranslationService);
  private homeComponentSetupsService = inject(CompanyHomeComponentSetupsService);
  private destroyRef = inject(DestroyRef);
  private cdr = inject(ChangeDetectorRef);
  companyState = inject(CompanyStateService);

  setups: CompanyHomeComponentSetupDto[] = [];
  loading = false;
  private lastLoadedCompanyId?: string;

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
          this.lastLoadedCompanyId = undefined;
          this.cdr.markForCheck();
          return;
        }
        this.loadSetups(company.id);
      });

    // Reload setups whenever the :companyId route segment changes — covers
    // back/forward navigation where the home component is reused but the
    // selectedCompany$ chain may not re-emit.
    // this.route.paramMap
    //   .pipe(takeUntilDestroyed(this.destroyRef))
    //   .subscribe(params => {
    //     const companyId = params.get('companyId');
    //     if (companyId) {
    //       this.loadSetups(companyId);
    //     }
    //   });
  }

  private loadSetups(companyId: string): void {
    if (this.lastLoadedCompanyId === companyId) {
      return;
    }
    this.lastLoadedCompanyId = companyId;
    this.loading = true;
    this.cdr.markForCheck();
    this.homeComponentSetupsService.getByCompany(companyId).subscribe({
      next: response => {
        if (response.success && response.data) {
          this.setups = [...response.data].sort((a, b) => a.rank - b.rank);
        } else {
          this.setups = [];
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.setups = [];
        this.loading = false;
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
