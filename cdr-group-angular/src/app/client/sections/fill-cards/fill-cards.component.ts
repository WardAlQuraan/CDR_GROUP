import { ChangeDetectorRef, Component, ElementRef, Input, OnChanges, SimpleChanges, ViewChild, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyTitleDescriptionsService } from '../../../services/company-title-descriptions.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyTitleDescriptionDto } from '../../../models/company-title-description.model';

@Component({
  selector: 'app-fill-cards',
  standalone: false,
  templateUrl: './fill-cards.component.html',
  styleUrl: './fill-cards.component.scss',
})
export class FillCardsComponent implements OnChanges {
  @Input() companyId = '';
  @Input() code = '';
  @Input() titleCode = '';
  @Input() descriptionCode = '';

  @ViewChild('scroller') scrollerRef?: ElementRef<HTMLElement>;

  // When the number of items exceeds this threshold, render as a horizontal
  // slider with arrow buttons instead of a wrapping grid.
  private static readonly SLIDER_THRESHOLD = 3;

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

  get isSlider(): boolean {
    return this.items.length > FillCardsComponent.SLIDER_THRESHOLD;
  }

  scrollPrev(): void {
    this.scrollByCard(false);
  }

  scrollNext(): void {
    this.scrollByCard(true);
  }

  private scrollByCard(forward: boolean): void {
    const el = this.scrollerRef?.nativeElement;
    if (!el) return;
    const card = el.querySelector<HTMLElement>('.fill-card');
    if (!card) return;
    const styles = getComputedStyle(el);
    const gap = parseFloat(styles.columnGap || styles.gap || '0') || 20;
    const step = card.offsetWidth + gap;
    const direction = forward !== this.isArabic ? 1 : -1;
    el.scrollBy({ left: direction * step, behavior: 'smooth' });
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

  getTitle(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.titleAr : item.titleEn) || '';
  }

  getDescription(item: CompanyTitleDescriptionDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) || '';
  }

  private loadItems(): void {
    this.loading = true;
    this.items = [];
    this.companyTitleDescriptionsService.getByCompanyAndCode(this.companyId, this.code).subscribe({
      next: (response) => {
        this.items = response.success && response.data ? response.data : [];
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
}
