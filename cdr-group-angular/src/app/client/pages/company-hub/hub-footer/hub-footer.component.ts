import { ChangeDetectorRef, Component, OnDestroy, OnInit, inject } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { TranslationService } from '../../../../services/translation.service';
import { CompanyContactsService } from '../../../../services/company-contacts.service';
import { CompanyStateService } from '../../../../services/company-state.service';
import { CompanyContactDto } from '../../../../models/company-contact.model';
import { CompanyDto } from '../../../../models/company.model';
import { SnackbarService } from '../../../../services/snackbar.service';

@Component({
  selector: 'app-hub-footer',
  standalone: false,
  templateUrl: './hub-footer.component.html',
  styleUrl: './hub-footer.component.scss',
})
export class HubFooterComponent implements OnInit, OnDestroy {
  translationService = inject(TranslationService);
  companyState = inject(CompanyStateService);
  private companyContactsService = inject(CompanyContactsService);
  private cdr = inject(ChangeDetectorRef);
  private sanitizer = inject(DomSanitizer);
  private snackbar = inject(SnackbarService);

  currentYear = new Date().getFullYear();
  contacts: CompanyContactDto[] = [];
  loadingContacts = false;
  activeCompany?: CompanyDto;
  private sub?: Subscription;

  private dayTranslations: Record<string, string> = {
    'Sunday': 'الأحد',
    'Monday': 'الاثنين',
    'Tuesday': 'الثلاثاء',
    'Wednesday': 'الأربعاء',
    'Thursday': 'الخميس',
    'Friday': 'الجمعة',
    'Saturday': 'السبت'
  };

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get parentCompany(): CompanyDto | undefined {
    return this.companyState.selectedCompany;
  }

  get parentName(): string {
    return this.parentCompany ? this.getCompanyName(this.parentCompany) : '';
  }

  get children(): CompanyDto[] {
    return this.parentCompany?.children || [];
  }

  get displayCompany(): CompanyDto | undefined {
    return this.activeCompany || this.parentCompany;
  }

  get openingDays(): string {
    const c = this.displayCompany;
    if (!c?.openingStartDay || !c?.openingEndDay) return '';
    return `${this.translateDay(c.openingStartDay)} - ${this.translateDay(c.openingEndDay)}`;
  }

  get openingTimes(): string {
    const c = this.displayCompany;
    if (!c?.openingStartTime || !c?.openingEndTime) return '';
    return `${this.formatTime(c.openingStartTime)} - ${this.formatTime(c.openingEndTime)}`;
  }

  get openingNote(): string {
    const c = this.displayCompany;
    if (!c) return '';
    return (this.isArabic ? c.openingHoursNoteAr : c.openingHoursNoteEn) || '';
  }

  ngOnInit(): void {
    this.sub = this.companyState.selectedCompany$.subscribe(company => {
      if (company) {
        // Hub parent changed (route navigation) → default to its first child,
        // falling back to the parent itself if it has no children
        const firstChild = company.children?.[0];
        this.setActiveCompany(firstChild || company);
      }
    });
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

  getCompanyName(company: CompanyDto): string {
    return this.isArabic ? company.nameAr : company.nameEn;
  }

  setActiveCompany(company: CompanyDto): void {
    if (this.activeCompany?.id === company.id) return;
    this.activeCompany = company;
    this.loadContacts(company.id);
  }

  isActive(company: CompanyDto): boolean {
    return this.activeCompany?.id === company.id;
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
    return contact.value;
  }

  getDisplayValue(contact: CompanyContactDto): string {
    const value = contact.value || '';
    if (contact.icon === 'bi-telephone' || contact.icon === 'bi-printer' || contact.icon === 'bi-phone' || contact.icon === 'bi-envelope') {
      return value;
    }
    if (/^https?:\/\//i.test(value)) {
      try {
        const url = new URL(value);
        const path = (url.pathname + url.search).replace(/\/$/, '');
        const display = url.hostname.replace(/^www\./, '') + path;
        return display.length > 32 ? display.slice(0, 32) + '…' : display;
      } catch {
        return value.length > 32 ? value.slice(0, 32) + '…' : value;
      }
    }
    return value.length > 32 ? value.slice(0, 32) + '…' : value;
  }

  isExternalLink(contact: CompanyContactDto): boolean {
    return contact.icon !== 'bi-telephone' && contact.icon !== 'bi-envelope';
  }

  onContactClick(event: Event, contact: CompanyContactDto): void {
    navigator.clipboard.writeText(contact.value).then(() => {
      this.snackbar.success(this.translationService.translate('footer.copied'));
    });
  }

  private translateDay(day: string): string {
    return this.isArabic ? (this.dayTranslations[day] || day) : day;
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
    this.cdr.markForCheck();
    this.companyContactsService.getByCompanyId(companyId).subscribe(response => {
      if (response.success && response.data) {
        this.contacts = response.data;
      }
      this.loadingContacts = false;
      this.cdr.markForCheck();
    });
  }
}
