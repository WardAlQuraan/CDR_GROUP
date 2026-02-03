import { Component } from '@angular/core';

@Component({
  selector: 'app-newsletter',
  standalone: false,
  templateUrl: './newsletter.component.html',
  styleUrl: './newsletter.component.scss',
})
export class NewsletterComponent {
  email = '';

  onSubscribe() {
    if (this.email) {
      console.log('Subscribed with email:', this.email);
      this.email = '';
    }
  }
}
