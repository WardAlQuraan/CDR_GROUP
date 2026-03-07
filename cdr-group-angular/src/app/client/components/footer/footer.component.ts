import { Component, inject, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { TranslationService } from '../../../services/translation.service';
import { CompanyContactsService } from '../../../services/company-contacts.service';
import { CompanyStateService } from '../../../services/company-state.service';
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
  companyState = inject(CompanyStateService);
  private companyContactsService = inject(CompanyContactsService);
  private cdr = inject(ChangeDetectorRef);

  currentYear = new Date().getFullYear();
  contacts: CompanyContactDto[] = [];
  loadingContacts = false;
  private sub: Subscription;

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get selectedCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  constructor() {
    this.sub = this.companyState.selectedCompany$.subscribe(company => {
      if (company) {
        this.loadContacts(company.id);
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
    const companies = this.companyState.companies;
    const found = this.findInList(companies, companyId);
    if (found) {
      this.companyState.setSelectedCompany(found);
    }
  }

  private findInList(companies: CompanyDto[], id: string): CompanyDto | undefined {
    for (const company of companies) {
      if (company.id === id) return company;
      if (company.children?.length) {
        const found = this.findInList(company.children, id);
        if (found) return found;
      }
    }
    return undefined;
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
