import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-contact-review',
  standalone: false,
  templateUrl: './contact-review.component.html',
  styleUrl: './contact-review.component.scss',
})
export class ContactReviewComponent {
  @Input() companyId?: string;
  activeTab: 'contact' | 'review' | 'complaint' = 'contact';
}
