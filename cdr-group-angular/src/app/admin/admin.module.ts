import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

// Shared Module
import { SharedModule } from '../shared/shared.module';

// Layout
import { AdminLayoutComponent } from './admin-layout.component';

// Pages
import { DashboardComponent } from './pages/dashboard/dashboard.component';

// Routes
import { adminRoutes } from './admin.routes';
import { HasPermissionDirective } from "../directives/has-permission";
import { UsersComponent } from './pages/users/users-component/users-component';
import { UserDialogComponent } from './pages/users/user-dialog/user-dialog.component';
import { RolesComponent } from './pages/roles/roles-component/roles-component';
import { RoleDialogComponent } from './pages/roles/role-dialog/role-dialog.component';
import { PermissionsDialogComponent } from './pages/roles/permissions-dialog/permissions-dialog.component';
import { EmployeesComponent } from './pages/employees/employees-component/employees-component';
import { EmployeeDialogComponent } from './pages/employees/employee-dialog/employee-dialog.component';
import { EmployeeViewDialogComponent } from './pages/employees/employee-view-dialog/employee-view-dialog.component';
import { EmployeeLinkUserDialogComponent } from './pages/employees/employee-link-user-dialog/employee-link-user-dialog.component';
import { SalaryHistoryDialogComponent } from './pages/employees/salary-history-dialog/salary-history-dialog.component';
import { PositionsComponent } from './pages/positions/positions-component/positions-component';
import { PositionDialogComponent } from './pages/positions/position-dialog/position-dialog.component';
import { PositionViewDialogComponent } from './pages/positions/position-view-dialog/position-view-dialog.component';
import { EventsComponent } from './pages/events/events-component/events-component';
import { EventDialogComponent } from './pages/events/event-dialog/event-dialog.component';
import { EventViewDialogComponent } from './pages/events/event-view-dialog/event-view-dialog.component';
import { CompaniesComponent } from './pages/companies/companies-component/companies-component';
import { CompanyDialogComponent } from './pages/companies/company-dialog/company-dialog.component';
import { CompanyOrgChartPageComponent } from './pages/companies/company-org-chart-page/company-org-chart-page.component';
import { ContactUsAdminComponent } from './pages/contact-us/contact-us-component/contact-us-component';
import { ContactUsViewDialogComponent } from './pages/contact-us/contact-us-view-dialog/contact-us-view-dialog.component';
import { ChangePasswordDialogComponent } from './pages/users/change-password-dialog/change-password-dialog.component';
import { ManageRolesDialogComponent } from './pages/users/manage-roles-dialog/manage-roles-dialog.component';
import { AuditLogsComponent } from './pages/audit-logs/audit-logs-component/audit-logs-component';
import { AuditLogViewDialogComponent } from './pages/audit-logs/audit-log-view-dialog/audit-log-view-dialog.component';
import { CompanyContactsComponent } from './pages/company-contacts/company-contacts-component/company-contacts-component';
import { CompanyContactDialogComponent } from './pages/company-contacts/company-contact-dialog/company-contact-dialog.component';
import { ReviewsComponent } from './pages/reviews/reviews-component/reviews-component';
import { ReviewDialogComponent } from './pages/reviews/review-dialog/review-dialog.component';
import { ComplaintsComponent } from './pages/complaints/complaints-component/complaints-component';
import { ComplaintViewDialogComponent } from './pages/complaints/complaint-view-dialog/complaint-view-dialog.component';
import { PartnersComponent } from './pages/partners/partners-component/partners-component';
import { PartnerDialogComponent } from './pages/partners/partner-dialog/partner-dialog.component';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    DashboardComponent,
    UsersComponent,
    UserDialogComponent,
    ChangePasswordDialogComponent,
    ManageRolesDialogComponent,
    RolesComponent,
    RoleDialogComponent,
    PermissionsDialogComponent,
    EmployeesComponent,
    EmployeeDialogComponent,
    EmployeeViewDialogComponent,
    EmployeeLinkUserDialogComponent,
    SalaryHistoryDialogComponent,
    PositionsComponent,
    PositionDialogComponent,
    PositionViewDialogComponent,
    EventsComponent,
    EventDialogComponent,
    EventViewDialogComponent,
    CompaniesComponent,
    CompanyDialogComponent,
    CompanyOrgChartPageComponent,
    ContactUsAdminComponent,
    ContactUsViewDialogComponent,
    AuditLogsComponent,
    AuditLogViewDialogComponent,
    CompanyContactsComponent,
    CompanyContactDialogComponent,
    ReviewsComponent,
    ReviewDialogComponent,
    ComplaintsComponent,
    ComplaintViewDialogComponent,
    PartnersComponent,
    PartnerDialogComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(adminRoutes),
    HasPermissionDirective
  ],
})
export class AdminModule {}
