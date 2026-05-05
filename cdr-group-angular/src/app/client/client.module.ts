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
import { ReviewComponent } from './sections/review/review.component';
import { ContactReviewComponent } from './sections/contact-review/contact-review.component';
import { ComplaintComponent } from './sections/complaint/complaint.component';
import { ReviewsDisplayComponent } from './sections/reviews-display/reviews-display.component';
import { FormsComponent } from './sections/forms/forms.component';
import { BranchesComponent } from './sections/branches/branches.component';
import { ClientReachesComponent } from './sections/client-reaches/client-reaches.component';
import { SubCompaniesComponent } from './sections/sub-companies/sub-companies.component';
import { ComparisonComponent } from './sections/comparison/comparison.component';
import { GlassCardComponent } from './sections/glass-card/glass-card.component';
import { QuoteSliderComponent } from './sections/quote-slider/quote-slider.component';
import { FillCardsComponent } from './sections/fill-cards/fill-cards.component';
import { CardScrollerComponent } from './sections/card-scroller/card-scroller.component';
import { GlassSliderComponent } from './sections/glass-slider/glass-slider.component';
import { StatementSliderComponent } from './sections/statement-slider/statement-slider.component';
import { ImageTitleSliderComponent } from './sections/image-title-slider/image-title-slider.component';
import { PromiseBannerComponent } from './sections/promise-banner/promise-banner.component';

// Pages
import { HomeComponent } from './pages/home/home.component';
import { EventListingComponent } from './pages/event-listing/event-listing.component';
import { EventDetailComponent } from './pages/event-detail/event-detail.component';
import { CompanyDetailComponent } from './pages/company-detail/company-detail.component';
import { WorldMapComponent } from './pages/world-map/world-map.component';
import { CompanyHubComponent } from './pages/company-hub/company-hub.component';
import { HubHeaderComponent } from './pages/company-hub/hub-header/hub-header.component';
import { HubFooterComponent } from './pages/company-hub/hub-footer/hub-footer.component';

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
    ReviewComponent,
    ContactReviewComponent,
    ComplaintComponent,
    ReviewsDisplayComponent,
    FormsComponent,
    BranchesComponent,
    ClientReachesComponent,
    SubCompaniesComponent,
    ComparisonComponent,
    GlassCardComponent,
    QuoteSliderComponent,
    FillCardsComponent,
    CardScrollerComponent,
    GlassSliderComponent,
    StatementSliderComponent,
    ImageTitleSliderComponent,
    PromiseBannerComponent,
    // Pages
    HomeComponent,
    EventListingComponent,
    EventDetailComponent,
    CompanyDetailComponent,
    WorldMapComponent,
    CompanyHubComponent,
    HubHeaderComponent,
    HubFooterComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(clientRoutes),
  ],
  exports: [

  ]
})
export class ClientModule {}
