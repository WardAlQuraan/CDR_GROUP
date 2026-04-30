import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, SimpleChanges, effect, inject } from '@angular/core';
import { CompanySuccessReasonsService } from '../../../services/company-success-reasons.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanySuccessReasonDto } from '../../../models/company-success-reason.model';

@Component({
  selector: 'app-success-reasons',
  standalone: false,
  templateUrl: './success-reasons.component.html',
  styleUrl: './success-reasons.component.scss',
})
export class SuccessReasonsComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  reasons: CompanySuccessReasonDto[] = [];
  currentIndex = 0;
  isPaused = false;
  loading = false;

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private static readonly AUTOPLAY_MS = 5000;

  constructor(
    private companySuccessReasonsService: CompanySuccessReasonsService,
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
      this.loadReasons();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoplay();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasReasons(): boolean {
    return this.reasons.length > 0;
  }

  get hasMultiple(): boolean {
    return this.reasons.length > 1;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  get trackTransform(): string {
    const sign = this.isArabic ? 1 : -1;
    return `translateX(${sign * this.currentIndex * 100}%)`;
  }

  getReason(reason: CompanySuccessReasonDto): string {
    return this.isArabic ? reason.reasonAr : reason.reasonEn;
  }

  next(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex + 1) % this.reasons.length;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex - 1 + this.reasons.length) % this.reasons.length;
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

  private loadReasons(): void {
    this.loading = true;
    this.companySuccessReasonsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.reasons = response.success && response.data ? response.data : [];
        this.currentIndex = 0;
        this.startAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.reasons = [];
        this.stopAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private startAutoplay(): void {
    this.stopAutoplay();
    if (!this.hasMultiple) return;
    this.autoplayId = setInterval(() => {
      if (!this.isPaused) {
        this.next();
      }
    }, SuccessReasonsComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }
}
