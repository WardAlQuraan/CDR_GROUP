import { Component, Input, inject } from '@angular/core';
import { MembershipService, TeamMember } from '../../../services/membership';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-about',
  standalone: false,
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss',
})
export class AboutComponent {
  private membershipService = inject(MembershipService);
  private translationService = inject(TranslationService);

  @Input() company?: CompanyDto;

  teamMembers: TeamMember[] = this.membershipService.getTeamMembers();

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getMemberName(member: TeamMember): string {
    return this.isArabic ? member.nameAr : member.nameEn;
  }

  getMemberRole(member: TeamMember): string {
    return this.isArabic ? member.roleAr : member.roleEn;
  }

  get companyStory(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.storyAr : this.company.storyEn) || '';
  }

  get companyMission(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.missionAr : this.company.missionEn) || '';
  }

  get companyVision(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.visionAr : this.company.visionEn) || '';
  }
}
