import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyPartnershipFranchiseMechanismsService } from '../../../services/company-partnership-franchise-mechanisms.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyPartnershipFranchiseMechanismDto } from '../../../models/company-partnership-franchise-mechanism.model';

@Component({
  selector: 'app-partnership-franchise-mechanisms',
  standalone: false,
  templateUrl: './partnership-franchise-mechanisms.component.html',
  styleUrl: './partnership-franchise-mechanisms.component.scss',
})
export class PartnershipFranchiseMechanismsComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  mechanisms: CompanyPartnershipFranchiseMechanismDto[] = [];
  currentIndex = 0;
  isPaused = false;
  loading = false;

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private static readonly AUTOPLAY_MS = 5000;

  constructor(
    private companyPartnershipFranchiseMechanismsService: CompanyPartnershipFranchiseMechanismsService,
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
      this.loadMechanisms();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoplay();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasMechanisms(): boolean {
    return this.visibleMechanisms.length > 0;
  }

  get hasMultiple(): boolean {
    return this.visibleMechanisms.length > 1;
  }

  get visibleMechanisms(): CompanyPartnershipFranchiseMechanismDto[] {
    return this.mechanisms.filter(m => !!this.getDescription(m));
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

  getDescription(item: CompanyPartnershipFranchiseMechanismDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  next(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex + 1) % this.visibleMechanisms.length;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex - 1 + this.visibleMechanisms.length) % this.visibleMechanisms.length;
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

  private loadMechanisms(): void {
    this.loading = true;
    this.companyPartnershipFranchiseMechanismsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.mechanisms = response.success && response.data ? response.data : [];
        this.currentIndex = 0;
        this.startAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.mechanisms = [];
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
    }, PartnershipFranchiseMechanismsComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }
}
