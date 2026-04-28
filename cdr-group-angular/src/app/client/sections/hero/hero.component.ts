import { Component, Input, OnInit, OnChanges, OnDestroy, SimpleChanges, ChangeDetectorRef } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';
import { CompanyBackgroundsService } from '../../../services/company-backgrounds.service';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-hero',
  standalone: false,
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss',
})
export class HeroComponent implements OnInit, OnChanges, OnDestroy {
  @Input() isSmall = false;
  @Input() title = '';
  @Input() company?: CompanyDto;

  showMore = false;
  backgroundUrls: string[] = [];
  currentIndex = 0;

  private rotationTimer?: ReturnType<typeof setInterval>;
  private readonly slideIntervalMs = 5000;

  constructor(
    private translationService: TranslationService,
    private companyBackgroundsService: CompanyBackgroundsService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    if (this.company?.id) {
      this.loadBackgrounds(this.company.id);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    const change = changes['company'];
    if (change && change.currentValue?.id !== change.previousValue?.id) {
      const id = (change.currentValue as CompanyDto | undefined)?.id;
      if (id) {
        this.loadBackgrounds(id);
      } else {
        this.backgroundUrls = [];
        this.stopRotation();
      }
    }
  }

  ngOnDestroy(): void {
    this.stopRotation();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyName(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.nameAr : this.company.nameEn) || '';
  }

  get companyTitle(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.titleAr : this.company.titleEn) || '';
  }

  get companyDescription(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.descriptionAr : this.company.descriptionEn) || '';
  }

  toggleMore(): void {
    this.showMore = !this.showMore;
  }

  goToSlide(index: number): void {
    this.currentIndex = index;
    this.restartRotation();
  }

  private loadBackgrounds(companyId: string): void {
    this.companyBackgroundsService.getByCompany(companyId).subscribe({
      next: (response) => {
        const items = response.success && response.data ? response.data : [];
        this.backgroundUrls = items
          .map(b => this.resolveImageUrl(b.imageUrl))
          .filter((u): u is string => !!u);
        this.currentIndex = 0;
        this.restartRotation();
        this.cdr.markForCheck();
      }
    });
  }

  private resolveImageUrl(url?: string): string | undefined {
    if (!url) return undefined;
    if (/^https?:\/\//i.test(url)) return url;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${url.startsWith('/') ? '' : '/'}${url}`;
  }

  private restartRotation(): void {
    this.stopRotation();
    if (this.backgroundUrls.length > 1) {
      this.rotationTimer = setInterval(() => {
        this.currentIndex = (this.currentIndex + 1) % this.backgroundUrls.length;
        this.cdr.markForCheck();
      }, this.slideIntervalMs);
    }
  }

  private stopRotation(): void {
    if (this.rotationTimer) {
      clearInterval(this.rotationTimer);
      this.rotationTimer = undefined;
    }
  }
}
