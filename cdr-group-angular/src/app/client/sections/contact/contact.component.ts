import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { ContactUsService } from '../../../services/contact-us.service';
import { CreateContactUsDto } from '../../../models/contact-us.model';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent {
  constructor(private contactUsService: ContactUsService, private cdr:ChangeDetectorRef) {}

  formData: CreateContactUsDto = {
    fullName: '',
    email: '',
    message: ''
  };

  submitting = false;
  submitSuccess = false;
  submitError = '';

  onSubmit() {
    if (this.submitting) return;

    this.submitting = true;
    this.submitSuccess = false;
    this.submitError = '';

    this.contactUsService.create(this.formData).subscribe({
      next: (response) => {
        if (response.success) {
          this.submitSuccess = true;
          this.formData = { fullName: '', email: '', message: '' };
        } else {
          this.submitError = response.message || 'Something went wrong';
        }
        this.cdr.markForCheck();
        this.submitting = false;
      },
      error: (err) => {
        this.submitting = false;
        this.submitError = 'Failed to submit contact form. Please try again later.';
        this.cdr.markForCheck();
      }
    });
  }
}
