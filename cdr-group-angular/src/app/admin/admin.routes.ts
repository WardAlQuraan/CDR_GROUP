import { Routes } from '@angular/router';
import { AdminLayoutComponent } from './admin-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UsersComponent } from './pages/users/users-component/users-component';
import { RolesComponent } from './pages/roles/roles-component/roles-component';
import { EmployeesComponent } from './pages/employees/employees-component/employees-component';
import { PositionsComponent } from './pages/positions/positions-component/positions-component';
import { EventsComponent } from './pages/events/events-component/events-component';
import { CompaniesComponent } from './pages/companies/companies-component/companies-component';
import { CompanyOrgChartPageComponent } from './pages/companies/company-org-chart-page/company-org-chart-page.component';
import { ContactUsAdminComponent } from './pages/contact-us/contact-us-component/contact-us-component';
import { AuditLogsComponent } from './pages/audit-logs/audit-logs-component/audit-logs-component';
import { CompanyContactsComponent } from './pages/company-contacts/company-contacts-component/company-contacts-component';
import { CompanyBackgroundsComponent } from './pages/company-backgrounds/company-backgrounds-component/company-backgrounds-component';
import { CompanyFormsComponent } from './pages/company-forms/company-forms-component/company-forms-component';
import { CompanyPreferencesComponent } from './pages/company-preferences/company-preferences-component/company-preferences-component';
import { CompanyBranchesComponent } from './pages/company-branches/company-branches-component/company-branches-component';
import { ReviewsComponent } from './pages/reviews/reviews-component/reviews-component';
import { ComplaintsComponent } from './pages/complaints/complaints-component/complaints-component';
import { PartnersComponent } from './pages/partners/partners-component/partners-component';
import { CompanyFinancialClausesRightsComponent } from './pages/company-financial-clauses-rights/company-financial-clauses-rights-component/company-financial-clauses-rights-component';
import { CompanyClientReachesComponent } from './pages/company-client-reaches/company-client-reaches-component/company-client-reaches-component';
import { CompanyTitleDescriptionsComponent } from './pages/company-title-descriptions/company-title-descriptions-component/company-title-descriptions-component';
import { CompanyHomeComponentSetupsComponent } from './pages/company-home-component-setups/company-home-component-setups-component/company-home-component-setups-component';

export const adminRoutes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      { path: '', component: DashboardComponent },
      { path: 'users', component: UsersComponent },
      { path: 'roles', component: RolesComponent },
      { path: 'employees', component: EmployeesComponent },
      { path: 'positions', component: PositionsComponent },
      { path: 'events', component: EventsComponent },
      { path: 'companies', component: CompaniesComponent },
      { path: 'companies/:id/org-chart', component: CompanyOrgChartPageComponent },
      { path: 'companies/:companyId/contacts', component: CompanyContactsComponent },
      { path: 'companies/:companyId/backgrounds', component: CompanyBackgroundsComponent },
      { path: 'companies/:companyId/forms', component: CompanyFormsComponent },
      { path: 'companies/:companyId/preferences', component: CompanyPreferencesComponent },
      { path: 'companies/:companyId/branches', component: CompanyBranchesComponent },
      { path: 'companies/:companyId/financial-clauses-rights', component: CompanyFinancialClausesRightsComponent },
      { path: 'companies/:companyId/client-reaches', component: CompanyClientReachesComponent },
      { path: 'companies/:companyId/title-descriptions', component: CompanyTitleDescriptionsComponent },
      { path: 'companies/:companyId/home-component-setups', component: CompanyHomeComponentSetupsComponent },
      { path: 'partners', component: PartnersComponent },
      { path: 'contact-us', component: ContactUsAdminComponent },
      { path: 'reviews', component: ReviewsComponent },
      { path: 'complaints', component: ComplaintsComponent },
      { path: 'audit-logs', component: AuditLogsComponent },
      { path: 'audit-logs/:entityName/:entityId', component: AuditLogsComponent },
      { path: 'settings', component: DashboardComponent }, // Placeholder
    ]
  }
];
