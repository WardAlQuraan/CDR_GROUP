import { Component, inject } from '@angular/core';
import { MembershipService, TeamMember } from '../../../services/membership';

@Component({
  selector: 'app-about',
  standalone: false,
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss',
})
export class AboutComponent {
  private membershipService = inject(MembershipService);
  teamMembers: TeamMember[] = this.membershipService.getTeamMembers();
}
