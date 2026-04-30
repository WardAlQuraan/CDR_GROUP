import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyDistributionMarketingsService } from '../../../services/company-distribution-marketings.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDistributionMarketingDto } from '../../../models/company-distribution-marketing.model';

@Component({
  selector: 'app-distribution-marketings',
  standalone: false,
  templateUrl: './distribution-marketings.component.html',
  styleUrl: './distribution-marketings.component.scss',
})
export class DistributionMarketingsComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  marketings: CompanyDistributionMarketingDto[] = [];
  loading = false;

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
    }
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
