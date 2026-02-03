import { Component, inject } from '@angular/core';
import { EventService, Event } from '../../../services/event';

@Component({
  selector: 'app-event-listing',
  standalone: false,
  templateUrl: './event-listing.component.html',
  styleUrl: './event-listing.component.scss',
})
export class EventListingComponent {
  private eventService = inject(EventService);
  events: Event[] = this.eventService.getEvents();
  featuredEvents: Event[] = this.eventService.getFeaturedEvents();
}
