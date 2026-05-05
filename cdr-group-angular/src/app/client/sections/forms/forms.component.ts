import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyFormsService } from '../../../services/company-forms.service';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyFormDto } from '../../../models/company-form.model';

@Component({
  selector: 'app-forms',
  standalone: false,
  templateUrl: './forms.component.html',
  styleUrl: './forms.component.scss',
})
export class FormsComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private sanitizer = inject(DomSanitizer);

  forms: CompanyFormDto[] = [];
  loading = false;
  loadingTitle = false;
  loadingSubtitle = false;

  private titleEn = '';
  private titleAr = '';
  private subtitleEn = '';
  private subtitleAr = '';

  constructor(
    private companyFormsService: CompanyFormsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadForms();
      this.loadTitle();
      this.loadSubtitle();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasForms(): boolean {
    return this.forms.length > 0;
  }

  get sectionTitle(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeSectionTitle(): SafeHtml | null {
    return this.sectionTitle ? this.sanitizer.bypassSecurityTrustHtml(this.sectionTitle) : null;
  }

  get sectionSubtitle(): string {
    return (this.isArabic ? this.subtitleAr : this.subtitleEn) || '';
  }

  get safeSectionSubtitle(): SafeHtml | null {
    return this.sectionSubtitle ? this.sanitizer.bypassSecurityTrustHtml(this.sectionSubtitle) : null;
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    if (!this.companyId) return;
    this.loadingTitle = true;
    this.cdr.markForCheck();
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, 'COMPANY_FORMS_TITLE')
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.titleEn = response.data.valueEn ?? '';
            this.titleAr = response.data.valueAr ?? '';
          }
          this.loadingTitle = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.loadingTitle = false;
          this.cdr.markForCheck();
        }
      });
  }

  private loadSubtitle(): void {
    this.subtitleEn = '';
    this.subtitleAr = '';
    if (!this.companyId) return;
    this.loadingSubtitle = true;
    this.cdr.markForCheck();
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, 'COMPANY_FORMS_DESCRIPTION')
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.subtitleEn = response.data.valueEn ?? '';
            this.subtitleAr = response.data.valueAr ?? '';
          }
          this.loadingSubtitle = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.loadingSubtitle = false;
          this.cdr.markForCheck();
        }
      });
  }

  getFormName(form: CompanyFormDto): string {
    return this.isArabic ? form.formNameAr : form.formNameEn;
  }

  getInitials(form: CompanyFormDto): string {
    const name = this.getFormName(form) || '';
    const words = name.trim().split(/\s+/).slice(0, 2);
    return words.map(w => w.charAt(0)).join('').toUpperCase();
  }

  openForm(form: CompanyFormDto): void {
    if (form.formUrl) {
      window.open(form.formUrl, '_blank');
    }
  }

  private loadForms(): void {
    this.loading = true;
    this.companyFormsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.forms = response.data;
        } else {
          this.forms = [];
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.forms = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
