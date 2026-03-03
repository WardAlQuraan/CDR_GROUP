import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReviewsService } from '../../../services/reviews.service';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-review',
  standalone: false,
  templateUrl: './review.component.html',
  styleUrl: './review.component.scss',
})
export class ReviewComponent {
  @Input() companyId?: string;

  reviewForm: FormGroup;
  submitting = false;
  submitSuccess = false;
  submitError = '';
  hoveredStar = 0;

  constructor(
    private fb: FormBuilder,
    private reviewsService: ReviewsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    this.reviewForm = this.fb.group({
      numberOfStars: [0, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', [Validators.required, Validators.maxLength(2000)]]
    });
  }

  get selectedStars(): number {
    return this.reviewForm.get('numberOfStars')!.value;
  }

  setRating(star: number): void {
    this.reviewForm.get('numberOfStars')!.setValue(star);
    this.reviewForm.get('numberOfStars')!.markAsTouched();
  }

  translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.reviewForm.invalid) {
      this.reviewForm.markAllAsTouched();
      return;
    }
    if (this.submitting) return;

    this.submitting = true;
    this.submitSuccess = false;
    this.submitError = '';

    const payload = { ...this.reviewForm.value, companyId: this.companyId };
    this.reviewsService.create(payload).subscribe({
      next: (response) => {
        if (response.success) {
          this.submitSuccess = true;
          this.reviewForm.reset({ numberOfStars: 0, comment: '' });
          this.hoveredStar = 0;
        } else {
          this.submitError = response.message || 'Something went wrong';
        }
        this.cdr.markForCheck();
        this.submitting = false;
      },
      error: () => {
        this.submitting = false;
        this.submitError = this.translate('review.errorMessage');
        this.cdr.markForCheck();
      }
    });
  }
}
