import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-comparison',
  standalone: false,
  templateUrl: './comparison.component.html',
  styleUrl: './comparison.component.scss',
})
export class ComparisonComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private translationService = inject(TranslationService);
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);

  private static readonly LEFT_TITLE_CODE = 'COMPARISON_LEFT_TITLE';
  private static readonly LEFT_CONTENT_CODE = 'COMPARISON_LEFT_CONTENT';
  private static readonly RIGHT_TITLE_CODE = 'COMPARISON_RIGHT_TITLE';
  private static readonly RIGHT_CONTENT_CODE = 'COMPARISON_RIGHT_CONTENT';

  private leftTitleEn = '';
  private leftTitleAr = '';
  private leftContentEn = '';
  private leftContentAr = '';
  private rightTitleEn = '';
  private rightTitleAr = '';
  private rightContentEn = '';
  private rightContentAr = '';

  constructor() {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadPreference(ComparisonComponent.LEFT_TITLE_CODE, (en, ar) => { this.leftTitleEn = en; this.leftTitleAr = ar; });
      this.loadPreference(ComparisonComponent.LEFT_CONTENT_CODE, (en, ar) => { this.leftContentEn = en; this.leftContentAr = ar; });
      this.loadPreference(ComparisonComponent.RIGHT_TITLE_CODE, (en, ar) => { this.rightTitleEn = en; this.rightTitleAr = ar; });
      this.loadPreference(ComparisonComponent.RIGHT_CONTENT_CODE, (en, ar) => { this.rightContentEn = en; this.rightContentAr = ar; });
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasContent(): boolean {
    return !!(this.leftTitle || this.leftContent || this.rightTitle || this.rightContent);
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  get leftTitle(): string {
    return (this.isArabic ? this.leftTitleAr : this.leftTitleEn) || '';
  }

  get leftContent(): string {
    return (this.isArabic ? this.leftContentAr : this.leftContentEn) || '';
  }

  get rightTitle(): string {
    return (this.isArabic ? this.rightTitleAr : this.rightTitleEn) || '';
  }

  get rightContent(): string {
    return (this.isArabic ? this.rightContentAr : this.rightContentEn) || '';
  }

  get safeLeftContent(): SafeHtml | null {
    return this.leftContent ? this.sanitizer.bypassSecurityTrustHtml(this.leftContent) : null;
  }

  get safeRightContent(): SafeHtml | null {
    return this.rightContent ? this.sanitizer.bypassSecurityTrustHtml(this.rightContent) : null;
  }

  private loadPreference(code: string, assign: (en: string, ar: string) => void): void {
    assign('', '');
    if (!this.companyId) return;
    this.companyPreferencesService.getByCompanyAndCode(this.companyId, code).subscribe({
      next: (response) => {
        if (response.success && response.data?.id) {
          assign(response.data.valueEn ?? '', response.data.valueAr ?? '');
          this.cdr.markForCheck();
        }
      },
      error: () => {}
    });
  }
}
