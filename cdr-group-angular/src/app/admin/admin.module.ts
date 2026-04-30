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
import { CompanyLogoDialogComponent } from './pages/companies/company-logo-dialog/company-logo-dialog.component';
import { CompanyOrgChartPageComponent } from './pages/companies/company-org-chart-page/company-org-chart-page.component';
import { ContactUsAdminComponent } from './pages/contact-us/contact-us-component/contact-us-component';
import { ContactUsViewDialogComponent } from './pages/contact-us/contact-us-view-dialog/contact-us-view-dialog.component';
import { ChangePasswordDialogComponent } from './pages/users/change-password-dialog/change-password-dialog.component';
import { ManageRolesDialogComponent } from './pages/users/manage-roles-dialog/manage-roles-dialog.component';
import { AuditLogsComponent } from './pages/audit-logs/audit-logs-component/audit-logs-component';
import { AuditLogViewDialogComponent } from './pages/audit-logs/audit-log-view-dialog/audit-log-view-dialog.component';
import { CompanyContactsComponent } from './pages/company-contacts/company-contacts-component/company-contacts-component';
import { CompanyContactDialogComponent } from './pages/company-contacts/company-contact-dialog/company-contact-dialog.component';
import { CompanyBackgroundsComponent } from './pages/company-backgrounds/company-backgrounds-component/company-backgrounds-component';
import { CompanyBackgroundDialogComponent } from './pages/company-backgrounds/company-background-dialog/company-background-dialog.component';
import { CompanyFormsComponent } from './pages/company-forms/company-forms-component/company-forms-component';
import { CompanyFormDialogComponent } from './pages/company-forms/company-form-dialog/company-form-dialog.component';
import { CompanyPreferencesComponent } from './pages/company-preferences/company-preferences-component/company-preferences-component';
import { CompanyPreferenceDialogComponent } from './pages/company-preferences/company-preference-dialog/company-preference-dialog.component';
import { CompanyBranchesComponent } from './pages/company-branches/company-branches-component/company-branches-component';
import { CompanyBranchDialogComponent } from './pages/company-branches/company-branch-dialog/company-branch-dialog.component';
import { CompanySuccessReasonsComponent } from './pages/company-success-reasons/company-success-reasons-component/company-success-reasons-component';
import { CompanySuccessReasonDialogComponent } from './pages/company-success-reasons/company-success-reason-dialog/company-success-reason-dialog.component';
import { CompanyDistinguishesComponent } from './pages/company-distinguishes/company-distinguishes-component/company-distinguishes-component';
import { CompanyDistinguishDialogComponent } from './pages/company-distinguishes/company-distinguish-dialog/company-distinguish-dialog.component';
import { CompanyDistributionMarketingsComponent } from './pages/company-distribution-marketings/company-distribution-marketings-component/company-distribution-marketings-component';
import { CompanyDistributionMarketingDialogComponent } from './pages/company-distribution-marketings/company-distribution-marketing-dialog/company-distribution-marketing-dialog.component';
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
    CompanyLogoDialogComponent,
    CompanyOrgChartPageComponent,
    ContactUsAdminComponent,
    ContactUsViewDialogComponent,
    AuditLogsComponent,
    AuditLogViewDialogComponent,
    CompanyContactsComponent,
    CompanyContactDialogComponent,
    CompanyBackgroundsComponent,
    CompanyBackgroundDialogComponent,
    CompanyFormsComponent,
    CompanyFormDialogComponent,
    CompanyPreferencesComponent,
    CompanyPreferenceDialogComponent,
    CompanyBranchesComponent,
    CompanyBranchDialogComponent,
    CompanySuccessReasonsComponent,
    CompanySuccessReasonDialogComponent,
    CompanyDistinguishesComponent,
    CompanyDistinguishDialogComponent,
    CompanyDistributionMarketingsComponent,
    CompanyDistributionMarketingDialogComponent,
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
