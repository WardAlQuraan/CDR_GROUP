import { Routes } from '@angular/router';
import { ClientLayoutComponent } from './client-layout.component';
import { HomeComponent } from './pages/home/home.component';
import { EventListingComponent } from './pages/event-listing/event-listing.component';
import { EventDetailComponent } from './pages/event-detail/event-detail.component';

export const clientRoutes: Routes = [
  {
    path: '',
    component: ClientLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'events', component: EventListingComponent },
      { path: 'events/:id', component: EventDetailComponent },
    ]
  }
];
