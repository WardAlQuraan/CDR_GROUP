import { Component, inject, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { TranslationService } from '../../../services/translation.service';
import { CompaniesService } from '../../../services/companies.service';
import { CompanyContactsService } from '../../../services/company-contacts.service';
import { CompanyContactDto } from '../../../models/company-contact.model';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent implements OnDestroy {
  translationService = inject(TranslationService);
  private route = inject(ActivatedRoute);
  private companiesService = inject(CompaniesService);
  private companyContactsService = inject(CompanyContactsService);
  private cdr = inject(ChangeDetectorRef);

  currentYear = new Date().getFullYear();
  companies: CompanyDto[] = [];
  contacts: CompanyContactDto[] = [];
  selectedCompanyId?: string;
  selectedCompany?: CompanyDto;
  loadingContacts = false;
  private sub: Subscription;

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  constructor() {
    this.companiesService.getActiveCompanies().subscribe(response => {
      if (response.success && response.data) {
        this.companies = response.data;
      }
    });

    this.sub = this.route.queryParams.pipe(
      switchMap(params => {
        const code = params['company'] || 'CDR';
        return this.companiesService.getByCode(code);
      })
    ).subscribe(response => {
      if (response.success && response.data) {
        this.selectedCompanyId = response.data.id;
        this.selectedCompany = response.data;
        this.loadContacts(response.data.id);
      }
    });
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  private dayTranslations: Record<string, string> = {
    'Sunday': 'الأحد',
    'Monday': 'الاثنين',
    'Tuesday': 'الثلاثاء',
    'Wednesday': 'الأربعاء',
    'Thursday': 'الخميس',
    'Friday': 'الجمعة',
    'Saturday': 'السبت'
  };

  private translateDay(day: string): string {
    return this.isArabic ? (this.dayTranslations[day] || day) : day;
  }

  get openingDays(): string {
    if (!this.selectedCompany?.openingStartDay || !this.selectedCompany?.openingEndDay) return '';
    return `${this.translateDay(this.selectedCompany.openingStartDay)} - ${this.translateDay(this.selectedCompany.openingEndDay)}`;
  }

  get openingTimes(): string {
    if (!this.selectedCompany?.openingStartTime || !this.selectedCompany?.openingEndTime) return '';
    return `${this.formatTime(this.selectedCompany.openingStartTime)} - ${this.formatTime(this.selectedCompany.openingEndTime)}`;
  }

  get openingNote(): string {
    if (!this.selectedCompany) return '';
    return (this.isArabic ? this.selectedCompany.openingHoursNoteAr : this.selectedCompany.openingHoursNoteEn) || '';
  }

  selectCompany(companyId: string): void {
    this.selectedCompanyId = companyId;
    this.selectedCompany = this.companies.find(c => c.id === companyId);
    this.loadContacts(companyId);
  }

  private formatTime(time: string): string {
    if (!time) return '';
    const [hours, minutes] = time.split(':');
    const h = parseInt(hours, 10);
    const suffix = h >= 12 ? 'PM' : 'AM';
    const h12 = h % 12 || 12;
    return `${h12}:${minutes} ${suffix}`;
  }

  private loadContacts(companyId: string): void {
    this.loadingContacts = true;
    this.companyContactsService.getByCompanyId(companyId).subscribe(response => {
      if (response.success && response.data) {
        this.contacts = response.data;
      }
      this.loadingContacts = false;
      this.cdr.markForCheck();
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
