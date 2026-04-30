import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyPartnershipFranchiseMechanismsService } from '../../../services/company-partnership-franchise-mechanisms.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyPartnershipFranchiseMechanismDto } from '../../../models/company-partnership-franchise-mechanism.model';

@Component({
  selector: 'app-partnership-franchise-mechanisms',
  standalone: false,
  templateUrl: './partnership-franchise-mechanisms.component.html',
  styleUrl: './partnership-franchise-mechanisms.component.scss',
})
export class PartnershipFranchiseMechanismsComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  mechanisms: CompanyPartnershipFranchiseMechanismDto[] = [];
  loading = false;

  constructor(
    private companyPartnershipFranchiseMechanismsService: CompanyPartnershipFranchiseMechanismsService,
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
      this.loadMechanisms();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasMechanisms(): boolean {
    return this.mechanisms.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getDescription(item: CompanyPartnershipFranchiseMechanismDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  private loadMechanisms(): void {
    this.loading = true;
    this.companyPartnershipFranchiseMechanismsService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.mechanisms = response.success && response.data ? response.data : [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.mechanisms = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
