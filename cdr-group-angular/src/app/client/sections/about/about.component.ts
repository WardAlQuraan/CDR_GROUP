import { Component, Input, inject } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';
import { MembershipService, TeamMember } from '../../../services/membership';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-about',
  standalone: false,
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss',
})
export class AboutComponent {
  private translationService = inject(TranslationService);
  private membershipService = inject(MembershipService);

  @Input() company?: CompanyDto;

  get teamMembers(): TeamMember[] {
  
    return this.membershipService.getTeamMembers();
  }

  get story(): string {
    if (this.company) {
      const text = this.translationService.language() === 'ar'
        ? this.company.storyAr : this.company.storyEn;
      if (text) return text;
    }
    return this.translationService.translate('about.description');
  }

  get missionText(): string {
    if (this.company) {
      const text = this.translationService.language() === 'ar'
        ? this.company.missionAr : this.company.missionEn;
      if (text) return text;
    }
    return this.translationService.translate('about.missionText');
  }

  get visionText(): string {
    if (this.company) {
      const text = this.translationService.language() === 'ar'
        ? this.company.visionAr : this.company.visionEn;
      if (text) return text;
    }
    return this.translationService.translate('about.visionText');
  }
}
