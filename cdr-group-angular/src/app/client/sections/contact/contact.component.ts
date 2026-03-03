import { ChangeDetectorRef, Component, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContactUsService } from '../../../services/contact-us.service';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent {
  @Input() companyId?: string;

  contactForm: FormGroup;
  submitting = false;
  submitSuccess = false;
  submitError = '';

  constructor(
    private fb: FormBuilder,
    private contactUsService: ContactUsService,
    private cdr: ChangeDetectorRef
  ) {
    this.contactForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      message: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.contactForm.invalid) {
      this.contactForm.markAllAsTouched();
      return;
    }
    if (this.submitting) return;

    this.submitting = true;
    this.submitSuccess = false;
    this.submitError = '';

    const payload = { ...this.contactForm.value, companyId: this.companyId };
    this.contactUsService.create(payload).subscribe({
      next: (response) => {
        if (response.success) {
          this.submitSuccess = true;
          this.contactForm.reset();
        } else {
          this.submitError = response.message || 'Something went wrong';
        }
        this.cdr.markForCheck();
        this.submitting = false;
      },
      error: () => {
        this.submitting = false;
        this.submitError = 'Failed to submit contact form. Please try again later.';
        this.cdr.markForCheck();
      }
    });
  }
}
