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
import { ReviewsComponent } from './pages/reviews/reviews-component/reviews-component';
import { ComplaintsComponent } from './pages/complaints/complaints-component/complaints-component';
import { PartnersComponent } from './pages/partners/partners-component/partners-component';

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
