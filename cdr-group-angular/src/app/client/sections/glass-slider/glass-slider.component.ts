import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyTitleDescriptionsService } from '../../../services/company-title-descriptions.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyTitleDescriptionDto } from '../../../models/company-title-description.model';

@Component({
  selector: 'app-glass-slider',
  standalone: false,
  templateUrl: './glass-slider.component.html',
  styleUrl: './glass-slider.component.scss',
})
export class GlassSliderComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';
  @Input() code = '';
  @Input() titleCode = '';
  @Input() descriptionCode = '';

  private companyState = inject(CompanyStateService);
  private companyTitleDescriptionsService = inject(CompanyTitleDescriptionsService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private translationService = inject(TranslationService);
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);

  items: CompanyTitleDescriptionDto[] = [];
  currentIndex = 0;
  isPaused = false;
  loading = false;

  private titleEn = '';
  private titleAr = '';
  private sectionDescriptionEn = '';
  private sectionDescriptionAr = '';

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private static readonly AUTOPLAY_MS = 5000;

  constructor() {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if ((changes['companyId'] || changes['code']) && this.companyId && this.code) {
      this.loadItems();
    }
    if ((changes['companyId'] || changes['titleCode']) && this.companyId) {
      this.loadTitle();
    }
    if ((changes['companyId'] || changes['descriptionCode']) && this.companyId) {
      this.loadSectionDescription();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoplay();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasItems(): boolean {
    return this.items.length > 0;
  }

  get hasMultiple(): boolean {
    return this.items.length > 1;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  get sectionId(): string {
    return `section_${this.code.toLowerCase()}`;
  }

  get trackTransform(): string {
    const sign = this.isArabic ? 1 : -1;
    return `translateX(${sign * this.currentIndex * 100}%)`;
  }

  get sectionTitle(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeSectionTitle(): SafeHtml | null {
    return this.sectionTitle ? this.sanitizer.bypassSecurityTrustHtml(this.sectionTitle) : null;
  }

  get sectionDescription(): string {
    return (this.isArabic ? this.sectionDescriptionAr : this.sectionDescriptionEn) || '';
  }

  get safeSectionDescription(): SafeHtml | null {
    return this.sectionDescription ? this.sanitizer.bypassSecurityTrustHtml(this.sectionDescription) : null;
  }

  getTitle(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.titleAr : item.titleEn) || '';
  }

  getDescription(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) || '';
  }

  next(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex + 1) % this.items.length;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex - 1 + this.items.length) % this.items.length;
    this.cdr.markForCheck();
  }

  goTo(index: number): void {
    this.currentIndex = index;
    this.cdr.markForCheck();
  }

  onPointerEnter(): void {
    this.isPaused = true;
  }

  onPointerLeave(): void {
    this.isPaused = false;
  }

  private loadItems(): void {
    this.loading = true;
    this.companyTitleDescriptionsService.getByCompanyAndCode(this.companyId, this.code).subscribe({
      next: (response) => {
        this.items = response.success && response.data ? response.data : [];
        this.currentIndex = 0;
        this.startAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.items = [];
        this.stopAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    if (!this.companyId || !this.titleCode) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, this.titleCode)
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

  private loadSectionDescription(): void {
    this.sectionDescriptionEn = '';
    this.sectionDescriptionAr = '';
    if (!this.companyId || !this.descriptionCode) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, this.descriptionCode)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.sectionDescriptionEn = response.data.valueEn ?? '';
            this.sectionDescriptionAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  private startAutoplay(): void {
    this.stopAutoplay();
    if (!this.hasMultiple) return;
    this.autoplayId = setInterval(() => {
      if (!this.isPaused) {
        this.next();
      }
    }, GlassSliderComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }
}
