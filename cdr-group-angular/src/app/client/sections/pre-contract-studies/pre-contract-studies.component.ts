import { ChangeDetectorRef, Component, Input, OnChanges, OnDestroy, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyPreContractStudiesService } from '../../../services/company-pre-contract-studies.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyPreContractStudyDto } from '../../../models/company-pre-contract-study.model';

@Component({
  selector: 'app-pre-contract-studies',
  standalone: false,
  templateUrl: './pre-contract-studies.component.html',
  styleUrl: './pre-contract-studies.component.scss',
})
export class PreContractStudiesComponent implements OnChanges, OnDestroy {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  studies: CompanyPreContractStudyDto[] = [];
  currentIndex = 0;
  isPaused = false;
  loading = false;

  private autoplayId: ReturnType<typeof setInterval> | null = null;
  private static readonly AUTOPLAY_MS = 5000;

  constructor(
    private companyPreContractStudiesService: CompanyPreContractStudiesService,
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
      this.loadStudies();
    }
  }

  ngOnDestroy(): void {
    this.stopAutoplay();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasStudies(): boolean {
    return this.visibleStudies.length > 0;
  }

  get hasMultiple(): boolean {
    return this.visibleStudies.length > 1;
  }

  get visibleStudies(): CompanyPreContractStudyDto[] {
    return this.studies.filter(s => !!this.getDescription(s));
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

  getDescription(item: CompanyPreContractStudyDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  next(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex + 1) % this.visibleStudies.length;
    this.cdr.markForCheck();
  }

  prev(): void {
    if (!this.hasMultiple) return;
    this.currentIndex = (this.currentIndex - 1 + this.visibleStudies.length) % this.visibleStudies.length;
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

  private loadStudies(): void {
    this.loading = true;
    this.companyPreContractStudiesService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.studies = response.success && response.data ? response.data : [];
        this.currentIndex = 0;
        this.startAutoplay();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.studies = [];
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
    }, PreContractStudiesComponent.AUTOPLAY_MS);
  }

  private stopAutoplay(): void {
    if (this.autoplayId) {
      clearInterval(this.autoplayId);
      this.autoplayId = null;
    }
  }
}
