import { ChangeDetectorRef, Component, ElementRef, Input, NgZone, OnChanges, OnDestroy, SimpleChanges, ViewChild, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { forkJoin, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyTitleDescriptionsService } from '../../../services/company-title-descriptions.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { FilesService } from '../../../services/files.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyTitleDescriptionDto } from '../../../models/company-title-description.model';
import { FileAttachmentDto } from '../../../models/file-attachment.model';

interface ImageTitleSlide {
  id: string;
  titleEn: string;
  titleAr: string;
  descriptionEn: string;
  descriptionAr: string;
  imageUrl?: string;
}

@Component({
  selector: 'app-image-title-slider',
  standalone: false,
  templateUrl: './image-title-slider.component.html',
  styleUrl: './image-title-slider.component.scss',
})
export class ImageTitleSliderComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';
  @Input() code = '';
  @Input() titleCode = '';
  @Input() descriptionCode = '';

  // Using a setter so we can hook in the exact moment the viewport DOM mounts —
  // important because `@if (hasItems)` defers the viewport until items load,
  // which is after ngAfterViewInit would have run.
  private viewportEl?: HTMLElement;
  @ViewChild('viewport')
  set viewportRef(ref: ElementRef<HTMLElement> | undefined) {
    const next = ref?.nativeElement;
    if (next === this.viewportEl) return;
    this.resizeObserver?.disconnect();
    this.resizeObserver = undefined;
    this.viewportEl = next;
    if (next) {
      this.observeViewport(next);
    }
  }

  private static readonly ENTITY_TYPE = 'CompanyTitleDescription';
  private static readonly AUTOPLAY_MS = 5000;
  // Static slide dimensions in pixels — must match the CSS values on .image-title-slide
  private static readonly CARD_WIDTH = 320;
  private static readonly CARD_GAP = 16;

  private companyState = inject(CompanyStateService);
  private companyTitleDescriptionsService = inject(CompanyTitleDescriptionsService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private filesService = inject(FilesService);
  private translationService = inject(TranslationService);
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);
  private zone = inject(NgZone);

  items: ImageTitleSlide[] = [];
  currentIndex = 0;
  isPaused = false;
  loading = false;
  // How many full cards currently fit inside the viewport — recomputed on resize
  visibleCount = 1;

  private titleEn = '';
  private titleAr = '';
  private sectionDescriptionEn = '';
  private sectionDescriptionAr = '';

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private resizeObserver?: ResizeObserver;

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
    this.resizeObserver?.disconnect();
  }

  private observeViewport(el: HTMLElement): void {
    if (typeof ResizeObserver === 'undefined') {
      this.recalculateVisibleCount();
      return;
    }
    // Run outside Angular to avoid change detection thrash on resize, then
    // re-enter the zone only when the value actually changes.
    this.zone.runOutsideAngular(() => {
      this.resizeObserver = new ResizeObserver(() => {
        this.zone.run(() => this.recalculateVisibleCount());
      });
      this.resizeObserver.observe(el);
    });
    this.recalculateVisibleCount();
  }

  private recalculateVisibleCount(): void {
    const el = this.viewportEl;
    if (!el) return;
    const step = ImageTitleSliderComponent.CARD_WIDTH + ImageTitleSliderComponent.CARD_GAP;
    // (width + gap) / step accounts for the fact that the last visible card
    // doesn't need a trailing gap.
    const fit = Math.max(1, Math.floor((el.clientWidth + ImageTitleSliderComponent.CARD_GAP) / step));
    if (fit !== this.visibleCount) {
      this.visibleCount = fit;
      // Keep current index in range so we never expose trailing empty space
      this.currentIndex = Math.min(this.currentIndex, this.maxIndex);
      // Restart autoplay so it stops when everything now fits in one page
      this.startAutoplay();
      this.cdr.markForCheck();
    }
  }

  get maxIndex(): number {
    return Math.max(0, this.items.length - this.visibleCount);
  }

  get pageCount(): number {
    if (!this.items.length) return 0;
    return Math.ceil(this.items.length / this.visibleCount);
  }

  get activePage(): number {
    if (this.visibleCount <= 0) return 0;
    return Math.min(this.pageCount - 1, Math.floor(this.currentIndex / this.visibleCount));
  }

  get hasMultiplePages(): boolean {
    return this.pageCount > 1;
  }

  pageRange(): number[] {
    return Array.from({ length: this.pageCount }, (_, i) => i);
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
    return this.companyState.selectedCompany?.primaryColor || '#D9A93E';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#3E423D';
  }

  get sectionId(): string {
    return `section_${this.code.toLowerCase()}`;
  }

  get trackTransform(): string {
    const sign = this.isArabic ? 1 : -1;
    const step = ImageTitleSliderComponent.CARD_WIDTH + ImageTitleSliderComponent.CARD_GAP;
    return `translateX(${sign * this.currentIndex * step}px)`;
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

  getTitle(item: ImageTitleSlide): string {
    return (this.isArabic ? item.titleAr : item.titleEn) || '';
  }

  getDescription(item: ImageTitleSlide): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) || '';
  }

  next(): void {
    if (!this.hasMultiple || this.maxIndex <= 0) return;
    // Wrap to start once we've shown the final full page so we never expose
    // trailing empty space at the end of the strip.
    this.currentIndex = this.currentIndex >= this.maxIndex ? 0 : this.currentIndex + 1;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple || this.maxIndex <= 0) return;
    this.currentIndex = this.currentIndex <= 0 ? this.maxIndex : this.currentIndex - 1;
    this.cdr.markForCheck();
  }

  goToPage(page: number): void {
    if (!this.hasMultiple) return;
    const target = Math.max(0, Math.min(this.maxIndex, page * this.visibleCount));
    this.currentIndex = target;
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
        const records = response.success && response.data ? response.data : [];
        if (records.length === 0) {
          this.items = [];
          this.currentIndex = 0;
          this.stopAutoplay();
          this.loading = false;
          this.cdr.markForCheck();
          return;
        }
        this.attachImages(records);
      },
      error: () => {
        this.items = [];
        this.stopAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private attachImages(records: CompanyTitleDescriptionDto[]): void {
    const requests = records.map(record =>
      this.filesService.getByEntity(record.id, ImageTitleSliderComponent.ENTITY_TYPE).pipe(
        map(response => this.pickImageUrl(response.success && response.data ? response.data : [])),
        catchError(() => of<string | undefined>(undefined))
      )
    );

    forkJoin(requests).subscribe(imageUrls => {
      this.items = records.map((record, i) => ({
        id: record.id,
        titleEn: record.titleEn || '',
        titleAr: record.titleAr || '',
        descriptionEn: record.descriptionEn || '',
        descriptionAr: record.descriptionAr || '',
        imageUrl: imageUrls[i]
      }));
      this.currentIndex = 0;
      // Recalc visibleCount so maxIndex reflects the freshly loaded item count
      this.recalculateVisibleCount();
      this.startAutoplay();
      this.loading = false;
      this.cdr.markForCheck();
    });
  }

  private pickImageUrl(files: FileAttachmentDto[]): string | undefined {
    if (!files.length) return undefined;
    const images = files.filter(f => f.contentType?.startsWith('image/'));
    const pool = images.length ? images : files;
    const primary = pool.find(f => f.isPrimary);
    return (primary || pool[0]).fileUrl;
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
    if (!this.hasMultiplePages) return;
    this.autoplayId = setInterval(() => {
      if (!this.isPaused) {
        this.next();
      }
    }, ImageTitleSliderComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }
}
