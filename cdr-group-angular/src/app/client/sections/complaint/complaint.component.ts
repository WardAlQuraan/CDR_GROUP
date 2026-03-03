import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ComplaintsService } from '../../../services/complaints.service';
import { TranslationService } from '../../../services/translation.service';

@Component({
  selector: 'app-complaint',
  standalone: false,
  templateUrl: './complaint.component.html',
  styleUrl: './complaint.component.scss',
})
export class ComplaintComponent {
  @Input() companyId?: string;

  complaintForm: FormGroup;
  submitting = false;
  submitSuccess = false;
  submitError = '';

  constructor(
    private fb: FormBuilder,
    private complaintsService: ComplaintsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    this.complaintForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      subject: ['', Validators.required],
      message: ['', Validators.required]
    });
  }

  translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.complaintForm.invalid) {
      this.complaintForm.markAllAsTouched();
      return;
    }
    if (this.submitting) return;

    this.submitting = true;
    this.submitSuccess = false;
    this.submitError = '';

    const payload = { ...this.complaintForm.value, companyId: this.companyId };
    this.complaintsService.create(payload).subscribe({
      next: (response) => {
        if (response.success) {
          this.submitSuccess = true;
          this.complaintForm.reset();
        } else {
          this.submitError = response.message || 'Something went wrong';
        }
        this.cdr.markForCheck();
        this.submitting = false;
      },
      error: () => {
        this.submitting = false;
        this.submitError = this.translate('complaint.errorMessage');
        this.cdr.markForCheck();
      }
    });
  }
}
