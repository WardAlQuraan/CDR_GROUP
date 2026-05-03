import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
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
  private sanitizer = inject(DomSanitizer);
  private cdr = inject(ChangeDetectorRef);

  private static readonly DESCRIPTION_TITLE_CODE = 'ABOUT_DESCRIPTION_TITLE';
  private static readonly FIRST_SECTION_CODE = 'FIRST_SECTION_ABOUT_COMPANY';
  private static readonly SECOND_SECTION_CODE = 'SECOND_SECTION_ABOUT_COMPANY';
  private static readonly SECONDARY_DESCRIPTION_TITLE_CODE = 'SECONDARY_ABOUT_DESCRIPTION_TITLE';
  private static readonly SECONDARY_DESCRIPTION_TEXT_CODE = 'SECONDARY_ABOUT_DESCRIPTION_SUB_TITLE';

  @Input() company?: CompanyDto;

  teamMembers: TeamMember[] = this.membershipService.getTeamMembers();

  private descriptionTitleEn = '';
  private descriptionTitleAr = '';
  private firstSectionEn = '';
  private firstSectionAr = '';
  private secondSectionEn = '';
  private secondSectionAr = '';
  private secondaryDescriptionTitleEn = '';
  private secondaryDescriptionTitleAr = '';
  private secondaryDescriptionTextEn = '';
  private secondaryDescriptionTextAr = '';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['company']) {
      this.loadDescriptionTitle();
      this.loadFirstSection();
      this.loadSecondSection();
      this.loadSecondaryDescriptionTitle();
      this.loadSecondaryDescriptionText();
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

  get firstSection(): string {
    return (this.isArabic ? this.firstSectionAr : this.firstSectionEn) || '';
  }

  get safeFirstSection(): SafeHtml | null {
    return this.firstSection ? this.sanitizer.bypassSecurityTrustHtml(this.firstSection) : null;
  }

  get secondSection(): string {
    return (this.isArabic ? this.secondSectionAr : this.secondSectionEn) || '';
  }

  get safeSecondSection(): SafeHtml | null {
    return this.secondSection ? this.sanitizer.bypassSecurityTrustHtml(this.secondSection) : null;
  }

  get secondaryDescriptionTitle(): string {
    return (this.isArabic ? this.secondaryDescriptionTitleAr : this.secondaryDescriptionTitleEn) || '';
  }

  get safeSecondaryDescriptionTitle(): SafeHtml | null {
    return this.secondaryDescriptionTitle
      ? this.sanitizer.bypassSecurityTrustHtml(this.secondaryDescriptionTitle)
      : null;
  }

  get secondaryDescriptionText(): string {
    return (this.isArabic ? this.secondaryDescriptionTextAr : this.secondaryDescriptionTextEn) || '';
  }

  get safeSecondaryDescriptionText(): SafeHtml | null {
    return this.secondaryDescriptionText
      ? this.sanitizer.bypassSecurityTrustHtml(this.secondaryDescriptionText)
      : null;
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

  private loadFirstSection(): void {
    this.firstSectionEn = '';
    this.firstSectionAr = '';
    if (!this.company?.id) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.company.id, AboutComponent.FIRST_SECTION_CODE)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.firstSectionEn = response.data.valueEn ?? '';
            this.firstSectionAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  private loadSecondSection(): void {
    this.secondSectionEn = '';
    this.secondSectionAr = '';
    if (!this.company?.id) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.company.id, AboutComponent.SECOND_SECTION_CODE)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.secondSectionEn = response.data.valueEn ?? '';
            this.secondSectionAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  private loadSecondaryDescriptionTitle(): void {
    this.secondaryDescriptionTitleEn = '';
    this.secondaryDescriptionTitleAr = '';
    if (!this.company?.id) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.company.id, AboutComponent.SECONDARY_DESCRIPTION_TITLE_CODE)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.secondaryDescriptionTitleEn = response.data.valueEn ?? '';
            this.secondaryDescriptionTitleAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  private loadSecondaryDescriptionText(): void {
    this.secondaryDescriptionTextEn = '';
    this.secondaryDescriptionTextAr = '';
    if (!this.company?.id) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.company.id, AboutComponent.SECONDARY_DESCRIPTION_TEXT_CODE)
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.secondaryDescriptionTextEn = response.data.valueEn ?? '';
            this.secondaryDescriptionTextAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }
}
