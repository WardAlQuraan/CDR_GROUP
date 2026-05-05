import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyTitleDescriptionsService } from '../../../services/company-title-descriptions.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyTitleDescriptionDto } from '../../../models/company-title-description.model';

@Component({
  selector: 'app-glass-card',
  standalone: false,
  templateUrl: './glass-card.component.html',
  styleUrl: './glass-card.component.scss',
})
export class GlassCardComponent implements OnChanges {
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
  loading = false;
  loadingTitle = false;
  loadingDescription = false;
  currentIndex = 0;

  private static readonly VISIBLE_COUNT = 3;

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
    if ((changes['companyId'] || changes['code']) && this.companyId && this.code) {
      this.loadItems();
    }
    if ((changes['companyId'] || changes['titleCode']) && this.companyId) {
      this.loadTitle();
    }
    if ((changes['companyId'] || changes['descriptionCode']) && this.companyId) {
      this.loadDescription();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasItems(): boolean {
    return this.items.length > 0;
  }

  get visibleCount(): number {
    return GlassCardComponent.VISIBLE_COUNT;
  }

  get maxIndex(): number {
    return Math.max(0, this.items.length - this.visibleCount);
  }

  get totalPositions(): number {
    return this.maxIndex + 1;
  }

  get positionDots(): number[] {
    return Array.from({ length: this.totalPositions }, (_, i) => i);
  }

  get hasMultiple(): boolean {
    return this.items.length > this.visibleCount;
  }

  get trackTransform(): string {
    const sign = this.isArabic ? 1 : -1;
    const shiftPercent = this.currentIndex * (100 / this.visibleCount);
    return `translateX(${sign * shiftPercent}%)`;
  }

  next(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex + 1) % this.totalPositions;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex - 1 + this.totalPositions) % this.totalPositions;
    this.cdr.markForCheck();
  }

  goTo(index: number): void {
    this.currentIndex = index;
    this.cdr.markForCheck();
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

  get sectionTitle(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeSectionTitle(): SafeHtml | null {
    return this.sectionTitle ? this.sanitizer.bypassSecurityTrustHtml(this.sectionTitle) : null;
  }

  get sectionDescription(): string {
    return (this.isArabic ? this.descriptionAr : this.descriptionEn) || '';
  }

  get safeSectionDescription(): SafeHtml | null {
    return this.sectionDescription ? this.sanitizer.bypassSecurityTrustHtml(this.sectionDescription) : null;
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    if (!this.companyId || !this.titleCode) return;
    this.loadingTitle = true;
    this.cdr.markForCheck();
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, this.titleCode)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.titleEn = response.data.valueEn ?? '';
            this.titleAr = response.data.valueAr ?? '';
          }
          this.loadingTitle = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.loadingTitle = false;
          this.cdr.markForCheck();
        }
      });
  }

  private loadDescription(): void {
    this.descriptionEn = '';
    this.descriptionAr = '';
    if (!this.companyId || !this.descriptionCode) return;
    this.loadingDescription = true;
    this.cdr.markForCheck();
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, this.descriptionCode)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.descriptionEn = response.data.valueEn ?? '';
            this.descriptionAr = response.data.valueAr ?? '';
          }
          this.loadingDescription = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.loadingDescription = false;
          this.cdr.markForCheck();
        }
      });
  }

  getTitle(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.titleAr : item.titleEn) || '';
  }

  getDescription(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) || '';
  }

  getSafeDescription(item: CompanyTitleDescriptionDto): SafeHtml | null {
    const desc = this.getDescription(item);
    return desc ? this.sanitizer.bypassSecurityTrustHtml(desc) : null;
  }

  private loadItems(): void {
    this.loading = true;
    this.items = [];
    this.companyTitleDescriptionsService.getByCompanyAndCode(this.companyId, this.code).subscribe({
      next: (response) => {
        this.items = response.success && response.data ? response.data : [];
        this.currentIndex = 0;
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.items = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
