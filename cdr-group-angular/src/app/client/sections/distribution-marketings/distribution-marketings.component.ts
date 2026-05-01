import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyDistributionMarketingsService } from '../../../services/company-distribution-marketings.service';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDistributionMarketingDto } from '../../../models/company-distribution-marketing.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-distribution-marketings',
  standalone: false,
  templateUrl: './distribution-marketings.component.html',
  styleUrl: './distribution-marketings.component.scss',
})
export class DistributionMarketingsComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private sanitizer = inject(DomSanitizer);


  marketings: CompanyDistributionMarketingDto[] = [];
  loading = false;

  private titleEn = '';
  private titleAr = '';

  constructor(
    private companyDistributionMarketingsService: CompanyDistributionMarketingsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadMarketings();
      this.loadTitle();
    }
  }

  get sectionTitle(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeSectionTitle(): SafeHtml | null {
    const value = this.sectionTitle;
    return value ? this.sanitizer.bypassSecurityTrustHtml(value) : null;
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    if (!this.companyId) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, 'MARKETING_DESCRIPTION')
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.titleEn = response.data.valueEn ?? '';
            this.titleAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasMarketings(): boolean {
    return this.marketings.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getTitle(item: CompanyDistributionMarketingDto): string {
    return this.isArabic ? item.titleAr : item.titleEn;
  }

  getDescription(item: CompanyDistributionMarketingDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  private loadMarketings(): void {
    this.loading = true;
    this.companyDistributionMarketingsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.marketings = response.success && response.data ? response.data : [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.marketings = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
