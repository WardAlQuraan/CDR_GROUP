import { Component, inject, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TranslationService } from '../../../services/translation.service';
import { CompanyContactsService } from '../../../services/company-contacts.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { CompanyContactDto } from '../../../models/company-contact.model';
import { CompanyDto } from '../../../models/company.model';
import { SnackbarService } from '../../../services/snackbar.service';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent implements OnInit, OnDestroy {
  translationService = inject(TranslationService);
  companyState = inject(CompanyStateService);
  private companyContactsService = inject(CompanyContactsService);
  private cdr = inject(ChangeDetectorRef);
  private router = inject(Router);
  private sanitizer = inject(DomSanitizer);
  private snackbar = inject(SnackbarService);

  currentYear = new Date().getFullYear();
  contacts: CompanyContactDto[] = [];
  loadingContacts = false;
  private sub!: Subscription;

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get selectedCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  ngOnInit(): void {
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
    this.router.navigate(['/'], { queryParams: { company: companyId } });
  }

  private formatTime(time: string): string {
    if (!time) return '';
    const [hours, minutes] = time.split(':');
    const h = parseInt(hours, 10);
    const suffix = h >= 12 ? (this.isArabic ? 'م' : 'PM') : (this.isArabic ? 'ص' : 'AM');
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

  getContactHref(contact: CompanyContactDto): string | SafeUrl {
    if (contact.icon === 'bi-telephone' || contact.icon === 'bi-printer') {
      return this.sanitizer.bypassSecurityTrustUrl('tel:' + contact.value);
    }
    if (contact.icon === 'bi-envelope') {
      return this.sanitizer.bypassSecurityTrustUrl('mailto:' + contact.value);
    }
    if (contact.icon === 'bi-whatsapp') {
      const phone = contact.value.replace(/[^0-9+]/g, '');
      return 'https://wa.me/' + phone;
    }
    if (contact.icon === 'bi-telegram') {
      return 'https://t.me/' + contact.value.replace('@', '');
    }
    if (contact.icon === 'bi-twitter' || contact.icon === 'bi-twitter-x') {
      return 'https://x.com/' + contact.value.replace('@', '');
    }
    // if (contact.icon === 'bi-geo-alt' || contact.icon === 'bi-geo-alt-fill') {
    //   return 'https://www.google.com/maps/search/?api=1&query=' + encodeURIComponent(contact.value);
    // }
    return contact.value;
  }

  isExternalLink(contact: CompanyContactDto): boolean {
    return contact.icon !== 'bi-telephone' && contact.icon !== 'bi-envelope';
  }

  onContactClick(event: Event, contact: CompanyContactDto): void {
    navigator.clipboard.writeText(contact.value).then(() => {
      this.snackbar.success(this.translationService.translate('footer.copied'));
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
}
