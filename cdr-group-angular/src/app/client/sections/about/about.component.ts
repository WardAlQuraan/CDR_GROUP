import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, inject } from '@angular/core';
import { MembershipService, TeamMember } from '../../../services/membership';
import { TranslationService } from '../../../services/translation.service';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-about',
  standalone: false,
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss',
})
export class AboutComponent implements OnChanges {
  private membershipService = inject(MembershipService);
  private translationService = inject(TranslationService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private cdr = inject(ChangeDetectorRef);

  private static readonly DESCRIPTION_TITLE_CODE = 'ABOUT_DESCRIPTION_TITLE';

  @Input() company?: CompanyDto;

  teamMembers: TeamMember[] = this.membershipService.getTeamMembers();

  private descriptionTitleEn = '';
  private descriptionTitleAr = '';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['company']) {
      this.loadDescriptionTitle();
    }
  }

  get primaryColorValue(): string {
    return this.company?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.company?.secondaryColor || '#FD1D1D';
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getMemberName(member: TeamMember): string {
    return this.isArabic ? member.nameAr : member.nameEn;
  }

  getMemberRole(member: TeamMember): string {
    return this.isArabic ? member.roleAr : member.roleEn;
  }

  get companyDescription(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.descriptionAr : this.company.descriptionEn) || '';
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

  get descriptionTitle(): string {
    return (this.isArabic ? this.descriptionTitleAr : this.descriptionTitleEn) || '';
  }

  private loadDescriptionTitle(): void {
    this.descriptionTitleEn = '';
    this.descriptionTitleAr = '';
    if (!this.company?.id || !this.companyDescription) {
      return;
    }
    this.companyPreferencesService
      .getByCompanyAndCode(this.company.id, AboutComponent.DESCRIPTION_TITLE_CODE)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.descriptionTitleEn = response.data.valueEn ?? '';
            this.descriptionTitleAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }
}
