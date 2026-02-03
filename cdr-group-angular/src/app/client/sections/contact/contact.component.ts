import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  standalone: false,
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent {
  formData = {
    fullName: '',
    email: '',
    message: ''
  };

  onSubmit() {
    console.log('Contact form submitted:', this.formData);
    this.formData = { fullName: '', email: '', message: '' };
  }
}
