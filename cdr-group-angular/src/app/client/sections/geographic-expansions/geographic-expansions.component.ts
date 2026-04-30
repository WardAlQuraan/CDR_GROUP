import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyGeographicExpansionsService } from '../../../services/company-geographic-expansions.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyGeographicExpansionDto } from '../../../models/company-geographic-expansion.model';

@Component({
  selector: 'app-geographic-expansions',
  standalone: false,
  templateUrl: './geographic-expansions.component.html',
  styleUrl: './geographic-expansions.component.scss',
})
export class GeographicExpansionsComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  expansions: CompanyGeographicExpansionDto[] = [];
  loading = false;

  constructor(
    private companyGeographicExpansionsService: CompanyGeographicExpansionsService,
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
      this.loadExpansions();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasExpansions(): boolean {
    return this.expansions.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getTitle(item: CompanyGeographicExpansionDto): string {
    return this.isArabic ? item.titleAr : item.titleEn;
  }

  getDescription(item: CompanyGeographicExpansionDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  private loadExpansions(): void {
    this.loading = true;
    this.companyGeographicExpansionsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.expansions = response.success && response.data ? response.data : [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.expansions = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
