import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

// Shared Module
import { SharedModule } from '../shared/shared.module';

// Layout
import { ClientLayoutComponent } from './client-layout.component';

// Components
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { LoginComponent } from './components/login/login.component';

// Sections
import { HeroComponent } from './sections/hero/hero.component';
import { AboutComponent } from './sections/about/about.component';
import { EventsComponent } from './sections/events/events.component';
import { ContactComponent } from './sections/contact/contact.component';
import { TeamComponent } from './sections/team/team.component';

// Pages
import { HomeComponent } from './pages/home/home.component';
import { EventListingComponent } from './pages/event-listing/event-listing.component';
import { EventDetailComponent } from './pages/event-detail/event-detail.component';
import { CompanyDetailComponent } from './pages/company-detail/company-detail.component';

// Routes
import { clientRoutes } from './client.routes';

@NgModule({
  declarations: [
    // Layout
    ClientLayoutComponent,
    // Components
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    // Sections
    HeroComponent,
    AboutComponent,
    EventsComponent,
    ContactComponent,
    TeamComponent,
    // Pages
    HomeComponent,
    EventListingComponent,
    EventDetailComponent,
    CompanyDetailComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(clientRoutes),
  ],
  exports: [

  ]
})
export class ClientModule {}
