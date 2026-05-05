import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-promise-banner',
  standalone: false,
  templateUrl: './promise-banner.component.html',
  styleUrl: './promise-banner.component.scss',
})
export class PromiseBannerComponent implements OnChanges {
  @Input() companyId = '';
  @Input() titleCode = '';
  @Input() descriptionCode = '';

  private companyState = inject(CompanyStateService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private translationService = inject(TranslationService);
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);

  private titleEn = '';
  private titleAr = '';
  private descriptionEn = '';
  private descriptionAr = '';

  constructor() {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ((changes['companyId'] || changes['titleCode']) && this.companyId && this.titleCode) {
      this.loadTitle();
    }
    if ((changes['companyId'] || changes['descriptionCode']) && this.companyId && this.descriptionCode) {
      this.loadDescription();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#D9A93E';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#3E423D';
  }

  get hasContent(): boolean {
    return Boolean(this.title || this.description);
  }

  get title(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeTitle(): SafeHtml | null {
    return this.title ? this.sanitizer.bypassSecurityTrustHtml(this.title) : null;
  }

  get description(): string {
    return (this.isArabic ? this.descriptionAr : this.descriptionEn) || '';
  }

  get safeDescription(): SafeHtml | null {
    return this.description ? this.sanitizer.bypassSecurityTrustHtml(this.description) : null;
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    this.companyPreferencesService.getByCompanyAndCode(this.companyId, this.titleCode).subscribe({
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

  private loadDescription(): void {
    this.descriptionEn = '';
    this.descriptionAr = '';
    this.companyPreferencesService.getByCompanyAndCode(this.companyId, this.descriptionCode).subscribe({
      next: (response) => {
        if (response.success && response.data?.id) {
          this.descriptionEn = response.data.valueEn ?? '';
          this.descriptionAr = response.data.valueAr ?? '';
          this.cdr.markForCheck();
        }
      },
      error: () => {}
    });
  }
}
