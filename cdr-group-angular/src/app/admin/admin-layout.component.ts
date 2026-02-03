import { Component, inject, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AuthService } from '../services/auth.service';
import { TranslationService } from '../services/translation.service';
import { Permissions } from '../models/auth.model';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  exact?: boolean;
}

@Component({
  selector: 'app-admin-layout',
  standalone: false,
  templateUrl: './admin-layout.component.html',
  styleUrl: './admin-layout.component.scss',
})
export class AdminLayoutComponent implements OnInit {


  hasUserPermission: boolean = true;
  isMobile = false;
  isCollapsed = false;

  navItems: NavItem[] = [
    { label: 'admin.dashboard', icon: 'dashboard', route: '/admin', exact: true },
    { label: 'admin.users.title', icon: 'people', route: '/admin/users' },
    { label: 'admin.roles.title', icon: 'admin_panel_settings', route: '/admin/roles' },
    { label: 'admin.employees.title', icon: 'badge', route: '/admin/employees' },
    { label: 'admin.positions.title', icon: 'work_outline', route: '/admin/positions' },
    { label: 'admin.departments.title', icon: 'business', route: '/admin/departments' },
    { label: 'admin.eventsAdmin.title', icon: 'event', route: '/admin/events' },
  ];

  constructor(public authService: AuthService, public translationService: TranslationService, private breakpointObserver: BreakpointObserver) {
    this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait])
      .subscribe(result => {
        this.isMobile = result.matches;
        if (this.isMobile) {
          this.isCollapsed = true;
        }
      });
  }
  ngOnInit(): void {
    this.hasUserPermission = this.authService.hasPermission(Permissions.USERS_READ);
    if (!this.hasUserPermission) {
      this.navItems = this.navItems.filter(item => item.route !== '/admin/users');
    }

    const hasRolesPermission = this.authService.hasPermission(Permissions.ROLES_READ);
    if (!hasRolesPermission) {
      this.navItems = this.navItems.filter(item => item.route !== '/admin/roles');
    }

    const hasPositionsPermission = this.authService.hasPermission(Permissions.POSITIONS_READ);
    if (!hasPositionsPermission) {
      this.navItems = this.navItems.filter(item => item.route !== '/admin/positions');
    }

    const hasDepartmentsPermission = this.authService.hasPermission(Permissions.DEPARTMENTS_READ);
    if (!hasDepartmentsPermission) {
      this.navItems = this.navItems.filter(item => item.route !== '/admin/departments');
    }
  }

  toggleSidenav(): void {
    this.isCollapsed = !this.isCollapsed;
  }

  logout(): void {
    this.authService.logout();
  }
}
