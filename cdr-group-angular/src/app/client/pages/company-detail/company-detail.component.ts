import { ChangeDetectorRef, Component, OnInit, OnDestroy, effect } from '@angular/core';
import { Subscription } from 'rxjs';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-company-detail',
  standalone: false,
  templateUrl: './company-detail.component.html',
  styleUrl: './company-detail.component.scss',
})
export class CompanyDetailComponent implements OnInit, OnDestroy {
  loading = true;
  error = false;
  company: CompanyDto | null = null;
  companyId: string | null = null;
  private sub!: Subscription;

  constructor(
    private companyStateService: CompanyStateService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnInit(): void {
    this.sub = this.companyStateService.selectedCompany$.subscribe(company => {
      if (company) {
        this.companyId = company.id;
        this.company = company;
        this.loading = false;
        this.error = false;
        this.cdr.markForCheck();
      } else {
        this.loading = false;
        this.error = true;
        this.cdr.markForCheck();
      }
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyName(): string {
    if (!this.company) return '';
    return this.isArabic ? this.company.nameAr : this.company.nameEn;
  }

  get companyDescription(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.descriptionAr : this.company.descriptionEn) || '';
  }

  get companyTitle(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.titleAr : this.company.titleEn) || '';
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
