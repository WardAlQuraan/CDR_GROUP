import { ChangeDetectorRef, Component, ElementRef, Input, OnChanges, OnDestroy, SimpleChanges, ViewChild, effect, inject } from '@angular/core';
import { CompanyDistinguishesService } from '../../../services/company-distinguishes.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDistinguishDto } from '../../../models/company-distinguish.model';

@Component({
  selector: 'app-distinguishes',
  standalone: false,
  templateUrl: './distinguishes.component.html',
  styleUrl: './distinguishes.component.scss',
})
export class DistinguishesComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';

  @ViewChild('scroller') scrollerRef?: ElementRef<HTMLDivElement>;

  private companyState = inject(CompanyStateService);

  distinguishes: CompanyDistinguishDto[] = [];
  loading = false;
  isPaused = false;

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private static readonly AUTOPLAY_MS = 4500;

  constructor(
    private companyDistinguishesService: CompanyDistinguishesService,
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
      this.loadDistinguishes();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoplay();
  }

  onPointerEnter(): void {
    this.isPaused = true;
  }

  onPointerLeave(): void {
    this.isPaused = false;
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasDistinguishes(): boolean {
    return this.distinguishes.length > 0;
  }

  get hasMultiple(): boolean {
    return this.distinguishes.length > 1;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getTitle(item: CompanyDistinguishDto): string {
    return (this.isArabic ? item.titleAr : item.titleEn) ?? '';
  }

  getDescription(item: CompanyDistinguishDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  scrollPrev(): void {
    this.scrollByCard(false);
    this.restartAutoplay();
  }

  scrollNext(): void {
    this.scrollByCard(true);
    this.restartAutoplay();
  }

  private scrollByCard(forward: boolean): void {
    const el = this.scrollerRef?.nativeElement;
    if (!el) return;
    const card = el.querySelector<HTMLElement>('.distinguish-card');
    if (!card) return;
    const styles = getComputedStyle(el);
    const gap = parseFloat(styles.columnGap || styles.gap || '0') || 22;
    const step = card.offsetWidth + gap;
    const direction = forward !== this.isArabic ? 1 : -1;
    el.scrollBy({ left: direction * step, behavior: 'smooth' });
  }

  private advanceAuto(): void {
    const el = this.scrollerRef?.nativeElement;
    if (!el) return;
    const card = el.querySelector<HTMLElement>('.distinguish-card');
    if (!card) return;

    const styles = getComputedStyle(el);
    const gap = parseFloat(styles.columnGap || styles.gap || '0') || 22;
    const step = card.offsetWidth + gap;

    const maxScroll = Math.max(0, el.scrollWidth - el.clientWidth);
    const currentScroll = Math.abs(el.scrollLeft);

    if (maxScroll <= 0 || currentScroll >= maxScroll - 4) {
      el.scrollTo({ left: 0, behavior: 'smooth' });
    } else {
      const direction = this.isArabic ? -1 : 1;
      el.scrollBy({ left: direction * step, behavior: 'smooth' });
    }
  }

  private startAutoplay(): void {
    this.stopAutoplay();
    if (!this.hasMultiple) return;
    this.autoplayId = setInterval(() => {
      if (!this.isPaused) {
        this.advanceAuto();
      }
    }, DistinguishesComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }

  private restartAutoplay(): void {
    if (this.autoplayId) {
      this.startAutoplay();
    }
  }

  private loadDistinguishes(): void {
    this.loading = true;
    this.companyDistinguishesService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.distinguishes = response.success && response.data ? response.data : [];
        this.startAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.distinguishes = [];
        this.stopAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
