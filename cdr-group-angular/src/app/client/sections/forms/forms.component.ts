import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyFormsService } from '../../../services/company-forms.service';
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

  forms: CompanyFormDto[] = [];
  loading = false;

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
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasForms(): boolean {
    return this.forms.length > 0;
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
