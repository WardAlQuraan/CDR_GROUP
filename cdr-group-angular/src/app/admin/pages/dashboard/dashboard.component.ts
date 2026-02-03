import { Component, OnInit } from '@angular/core';
import { Permissions } from '../../../models/auth.model';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  hasUserPermission = true;
  permissions = Permissions;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.hasUserPermission = this.authService.hasPermission(Permissions.USERS_READ);
  }
}
