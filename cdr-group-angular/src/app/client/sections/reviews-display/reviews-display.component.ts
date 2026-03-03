import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ReviewsService } from '../../../services/reviews.service';
import { TranslationService } from '../../../services/translation.service';
import { ReviewDto } from '../../../models/review.model';

@Component({
  selector: 'app-reviews-display',
  standalone: false,
  templateUrl: './reviews-display.component.html',
  styleUrl: './reviews-display.component.scss',
})
export class ReviewsDisplayComponent implements OnChanges {
  @Input() companyId?: string;

  reviews: ReviewDto[] = [];
  loading = false;
  currentPage = 1;
  pageSize = 6;
  totalPages = 0;
  hasMore = false;

  constructor(
    private reviewsService: ReviewsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.reviews = [];
      this.currentPage = 1;
      this.loadReviews();
    }
  }

  loadReviews(): void {
    this.loading = true;
    this.reviewsService.getReviewsPaged({
      pageNumber: this.currentPage,
      pageSize: this.pageSize,
      isVisible: true,
      companyId: this.companyId,
      sortDescending: true,
      sortBy: 'CreatedAt'
    }).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.reviews = [...this.reviews, ...response.data.data];
          this.totalPages = response.data.totalPages;
          this.hasMore = response.data.hasNextPage;
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  loadMore(): void {
    this.currentPage++;
    this.loadReviews();
  }

  getStarsArray(count: number): number[] {
    return Array.from({ length: 5 }, (_, i) => i + 1);
  }

  getTimeAgo(date: Date): string {
    const now = new Date();
    const past = new Date(date);
    const diffMs = now.getTime() - past.getTime();
    const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));

    if (diffDays === 0) return this.translationService.translate('reviewsDisplay.today');
    if (diffDays === 1) return this.translationService.translate('reviewsDisplay.yesterday');
    if (diffDays < 7) return diffDays + ' ' + this.translationService.translate('reviewsDisplay.daysAgo');
    if (diffDays < 30) {
      const weeks = Math.floor(diffDays / 7);
      return weeks + ' ' + this.translationService.translate(weeks === 1 ? 'reviewsDisplay.weekAgo' : 'reviewsDisplay.weeksAgo');
    }
    if (diffDays < 365) {
      const months = Math.floor(diffDays / 30);
      return months + ' ' + this.translationService.translate(months === 1 ? 'reviewsDisplay.monthAgo' : 'reviewsDisplay.monthsAgo');
    }
    const years = Math.floor(diffDays / 365);
    return years + ' ' + this.translationService.translate(years === 1 ? 'reviewsDisplay.yearAgo' : 'reviewsDisplay.yearsAgo');
  }

  getInitial(index: number): string {
    const letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    return letters[index % letters.length];
  }

  getAvatarColor(index: number): string {
    const colors = ['#6366f1', '#8b5cf6', '#ec4899', '#f43f5e', '#f97316', '#eab308', '#22c55e', '#14b8a6', '#06b6d4', '#3b82f6'];
    return colors[index % colors.length];
  }
}
