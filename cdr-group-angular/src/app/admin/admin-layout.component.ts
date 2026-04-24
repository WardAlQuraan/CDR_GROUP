import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AuthService } from '../services/auth.service';
import { TranslationService } from '../services/translation.service';
import { CompanyStateService } from '../services/company-state.service';
import { Permission, Permissions } from '../models/auth.model';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  exact?: boolean;
  permission?: Permission;
}

@Component({
  selector: 'app-admin-layout',
  standalone: false,
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.scss',
})
export class AdminLayoutComponent implements OnInit {
  isMobile = false;
  isCollapsed = false;
  navItems: NavItem[] = [];

  private readonly allNavItems: NavItem[] = [
    { label: 'admin.dashboard', icon: 'dashboard', route: '/admin', exact: true },
    { label: 'admin.users.title', icon: 'people', route: '/admin/users', permission: Permissions.USERS_READ },
    { label: 'admin.roles.title', icon: 'admin_panel_settings', route: '/admin/roles', permission: Permissions.ROLES_READ },
    { label: 'admin.employees.title', icon: 'badge', route: '/admin/employees', permission: Permissions.EMPLOYEES_READ },
    { label: 'admin.positions.title', icon: 'work_outline', route: '/admin/positions', permission: Permissions.POSITIONS_READ },
{ label: 'admin.companies.title', icon: 'apartment', route: '/admin/companies', permission: Permissions.COMPANIES_READ },
    { label: 'admin.eventsAdmin.title', icon: 'event', route: '/admin/events', permission: Permissions.EVENTS_READ },
    { label: 'admin.contactUs.title', icon: 'mail', route: '/admin/contact-us', permission: Permissions.CONTACTUS_READ },
    { label: 'admin.reviews.title', icon: 'star', route: '/admin/reviews', permission: Permissions.REVIEWS_READ },
    { label: 'admin.complaints.title', icon: 'report_problem', route: '/admin/complaints', permission: Permissions.COMPLAINTS_READ },
    { label: 'admin.partners.title', icon: 'handshake', route: '/admin/partners' },
  ];

  constructor(
    public authService: AuthService,
    public translationService: TranslationService,
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    private companyState: CompanyStateService
  ) {
    this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait])
      .subscribe(result => {
        this.isMobile = result.matches;
        if (this.isMobile) {
          this.isCollapsed = true;
        }
      });
  }

  ngOnInit(): void {
    this.navItems = this.allNavItems.filter(item =>
      !item.permission || this.authService.hasPermission(item.permission)
    );
  }

  toggleSidenav(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  goToSite(): void {
    const companyId = this.companyState.getLastSelectedCompanyId();
    this.router.navigate(companyId ? ['/', companyId] : ['/']);
  }

  logout(): void {
    this.authService.logout();
  }
}
