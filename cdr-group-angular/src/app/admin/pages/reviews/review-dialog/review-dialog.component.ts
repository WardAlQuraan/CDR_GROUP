import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReviewDto, UpdateReviewDto } from '../../../../models/review.model';
import { ReviewsService } from '../../../../services/reviews.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface ReviewDialogData {
  mode: 'edit';
  review: ReviewDto;
}

@Component({
  selector: 'app-review-dialog',
  standalone: false,
  templateUrl: './review-dialog.component.html',
  styleUrl: './review-dialog.component.scss'
})
export class ReviewDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ReviewDialogComponent>,
    private reviewsService: ReviewsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: ReviewDialogData
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    const review = this.data.review;
    this.form = this.fb.group({
      numberOfStars: [review.numberOfStars, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: [review.comment, [Validators.required, Validators.maxLength(2000)]],
      isVisible: [review.isVisible],
      companyId: [review.companyId]
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const updateDto: UpdateReviewDto = {
      numberOfStars: this.form.value.numberOfStars,
      comment: this.form.value.comment,
      isVisible: this.form.value.isVisible,
      companyId: this.data.review.companyId
    };

    this.reviewsService.update(this.data.review.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.reviews.reviewUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
