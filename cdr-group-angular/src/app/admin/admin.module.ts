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
import { EmployeeAssignManagerDialogComponent } from './pages/employees/employee-assign-manager-dialog/employee-assign-manager-dialog.component';
import { EmployeeLinkUserDialogComponent } from './pages/employees/employee-link-user-dialog/employee-link-user-dialog.component';
import { PositionsComponent } from './pages/positions/positions-component/positions-component';
import { PositionDialogComponent } from './pages/positions/position-dialog/position-dialog.component';
import { PositionViewDialogComponent } from './pages/positions/position-view-dialog/position-view-dialog.component';
import { PositionAssignDepartmentDialogComponent } from './pages/positions/position-assign-department-dialog/position-assign-department-dialog.component';
import { DepartmentsComponent } from './pages/departments/departments-component/departments-component';
import { DepartmentDialogComponent } from './pages/departments/department-dialog/department-dialog.component';
import { DepartmentViewDialogComponent } from './pages/departments/department-view-dialog/department-view-dialog.component';
import { DepartmentAssignManagerDialogComponent } from './pages/departments/department-assign-manager-dialog/department-assign-manager-dialog.component';
import { DepartmentOrgChartPageComponent } from './pages/departments/department-org-chart-page/department-org-chart-page.component';
import { EventsComponent } from './pages/events/events-component/events-component';
import { EventDialogComponent } from './pages/events/event-dialog/event-dialog.component';
import { EventViewDialogComponent } from './pages/events/event-view-dialog/event-view-dialog.component';
import { CompaniesComponent } from './pages/companies/companies-component/companies-component';
import { CompanyDialogComponent } from './pages/companies/company-dialog/company-dialog.component';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    DashboardComponent,
    UsersComponent,
    UserDialogComponent,
    RolesComponent,
    RoleDialogComponent,
    PermissionsDialogComponent,
    EmployeesComponent,
    EmployeeDialogComponent,
    EmployeeViewDialogComponent,
    EmployeeAssignManagerDialogComponent,
    EmployeeLinkUserDialogComponent,
    PositionsComponent,
    PositionDialogComponent,
    PositionViewDialogComponent,
    PositionAssignDepartmentDialogComponent,
    DepartmentsComponent,
    DepartmentDialogComponent,
    DepartmentViewDialogComponent,
    DepartmentAssignManagerDialogComponent,
    DepartmentOrgChartPageComponent,
    EventsComponent,
    EventDialogComponent,
    EventViewDialogComponent,
    CompaniesComponent,
    CompanyDialogComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(adminRoutes),
    HasPermissionDirective
  ],
})
export class AdminModule {}
