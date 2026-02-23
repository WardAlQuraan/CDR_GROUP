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
      { path: 'contact-us', component: ContactUsAdminComponent },
      { path: 'settings', component: DashboardComponent }, // Placeholder
    ]
  }
];
