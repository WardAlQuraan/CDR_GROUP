import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyPreContractStudiesService } from '../../../services/company-pre-contract-studies.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyPreContractStudyDto } from '../../../models/company-pre-contract-study.model';

@Component({
  selector: 'app-pre-contract-studies',
  standalone: false,
  templateUrl: './pre-contract-studies.component.html',
  styleUrl: './pre-contract-studies.component.scss',
})
export class PreContractStudiesComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  studies: CompanyPreContractStudyDto[] = [];
  loading = false;

  constructor(
    private companyPreContractStudiesService: CompanyPreContractStudiesService,
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
      this.loadStudies();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasStudies(): boolean {
    return this.studies.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getDescription(item: CompanyPreContractStudyDto): string {
    return (this.isArabic ? item.descriptionAr : item.descriptionEn) ?? '';
  }

  private loadStudies(): void {
    this.loading = true;
    this.companyPreContractStudiesService.getByCompany(this.companyId).subscribe({
      next: (response) => {
        this.studies = response.success && response.data ? response.data : [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.studies = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }
}
