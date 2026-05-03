import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CompanyClientReachesService } from '../../../services/company-client-reaches.service';
import { CompanyPreferencesService } from '../../../services/company-preferences.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyClientReachDto } from '../../../models/company-client-reach.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-client-reaches',
  standalone: false,
  templateUrl: './client-reaches.component.html',
  styleUrl: './client-reaches.component.scss',
})
export class ClientReachesComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);
  private companyPreferencesService = inject(CompanyPreferencesService);
  private sanitizer = inject(DomSanitizer);

  reaches: CompanyClientReachDto[] = [];
  loading = false;
  isPaused = false;

  private titleEn = '';
  private titleAr = '';
  private subtitleEn = '';
  private subtitleAr = '';

  constructor(
    private companyClientReachesService: CompanyClientReachesService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadReaches();
      this.loadTitle();
      this.loadSubtitle();
    }
  }

  get marqueeReaches(): CompanyClientReachDto[] {
    return [...this.reaches, ...this.reaches];
  }

  onPointerEnter(): void {
    this.isPaused = true;
  }

  onPointerLeave(): void {
    this.isPaused = false;
  }

  get sectionTitle(): string {
    return (this.isArabic ? this.titleAr : this.titleEn) || '';
  }

  get safeSectionTitle(): SafeHtml | null {
    const value = this.sectionTitle;
    return value ? this.sanitizer.bypassSecurityTrustHtml(value) : null;
  }

  get sectionSubtitle(): string {
    return (this.isArabic ? this.subtitleAr : this.subtitleEn) || '';
  }

  get safeSectionSubtitle(): SafeHtml | null {
    const value = this.sectionSubtitle;
    return value ? this.sanitizer.bypassSecurityTrustHtml(value) : null;
  }

  private loadTitle(): void {
    this.titleEn = '';
    this.titleAr = '';
    if (!this.companyId) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, 'COMPANY_CLIENT_REACHES_TITLE')
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.titleEn = response.data.valueEn ?? '';
            this.titleAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  private loadSubtitle(): void {
    this.subtitleEn = '';
    this.subtitleAr = '';
    if (!this.companyId) return;
    this.companyPreferencesService
      .getByCompanyAndCode(this.companyId, 'COMPANY_CLIENT_REACHES_DESCRIPTION')
      .subscribe({
        next: (response) => {
          if (response.success && response.data?.id) {
            this.subtitleEn = response.data.valueEn ?? '';
            this.subtitleAr = response.data.valueAr ?? '';
            this.cdr.markForCheck();
          }
        },
        error: () => {}
      });
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasReaches(): boolean {
    return this.reaches.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getClientName(item: CompanyClientReachDto): string {
    return this.isArabic ? item.clientNameAr : item.clientNameEn;
  }

  getDescription(item: CompanyClientReachDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  getLogoUrl(item: CompanyClientReachDto): string | undefined {
    const url = item.clientLogoUrl;
    if (!url) return undefined;
    if (/^https?:\/\//i.test(url)) return url;
    const base = environment.apiUrl.replace(/\/api\/?$/, '');
    return `${base}${url.startsWith('/') ? '' : '/'}${url}`;
  }

  private loadReaches(): void {
    this.loading = true;
    this.companyClientReachesService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.reaches = response.success && response.data ? response.data : [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.reaches = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
