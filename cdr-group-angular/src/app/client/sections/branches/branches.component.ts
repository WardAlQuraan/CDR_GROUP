import { ChangeDetectorRef, Component, Input, OnChanges, SimpleChanges, effect, inject } from '@angular/core';
import { CompanyBranchesService } from '../../../services/company-branches.service';
import { CompanyStateService } from '../../../services/company-state.service';
import { TranslationService } from '../../../services/translation.service';
import { CompanyBranchDto } from '../../../models/company-branch.model';

interface BranchTimelineRow {
  branch: CompanyBranchDto;
  startDate: Date;
  gapLabel?: string;
}

@Component({
  selector: 'app-branches',
  standalone: false,
  templateUrl: './branches.component.html',
  styleUrl: './branches.component.scss',
})
export class BranchesComponent implements OnChanges {
  @Input() companyId = '';

  private companyState = inject(CompanyStateService);

  branches: CompanyBranchDto[] = [];
  rows: BranchTimelineRow[] = [];
  loading = false;

  constructor(
    private companyBranchesService: CompanyBranchesService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      this.buildTimeline();
      this.cdr.markForCheck();
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId'] && this.companyId) {
      this.loadBranches();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get hasBranches(): boolean {
    return this.branches.length > 0;
  }

  get primaryColorValue(): string {
    return this.companyState.selectedCompany?.primaryColor || '#833AB4';
  }

  get secondaryColorValue(): string {
    return this.companyState.selectedCompany?.secondaryColor || '#FD1D1D';
  }

  getBranchName(branch: CompanyBranchDto): string {
    return this.isArabic ? branch.nameAr : branch.nameEn;
  }

  getBranchNickname(branch: CompanyBranchDto): string {
    const nickname = this.isArabic ? branch.nickNameAr : branch.nickNameEn;
    return nickname?.trim() || this.getBranchName(branch);
  }

  hasNickname(branch: CompanyBranchDto): boolean {
    const nickname = this.isArabic ? branch.nickNameAr : branch.nickNameEn;
    return !!nickname?.trim();
  }

  getCityName(branch: CompanyBranchDto): string {
    return this.isArabic ? branch.cityNameAr : branch.cityNameEn;
  }

  getDescription(branch: CompanyBranchDto): string {
    return (this.isArabic ? branch.descriptionAr : branch.descriptionEn) ?? '';
  }

  private loadBranches(): void {
    this.loading = true;
    this.companyBranchesService.getByCompanyId(this.companyId).subscribe({
      next: (response) => {
        this.branches = response.success && response.data ? response.data : [];
        this.buildTimeline();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.branches = [];
        this.rows = [];
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private buildTimeline(): void {
    const sorted = [...this.branches]
      .sort((a, b) => new Date(a.openingDate).getTime() - new Date(b.openingDate).getTime())
      .map<BranchTimelineRow>(branch => ({ branch, startDate: new Date(branch.openingDate) }));

    for (let i = 1; i < sorted.length; i++) {
      sorted[i].gapLabel = this.formatGap(sorted[i - 1].startDate, sorted[i].startDate);
    }

    this.rows = sorted;
  }

  private formatGap(start: Date, end: Date): string {
    const diffMs = Math.max(0, end.getTime() - start.getTime());
    const days = diffMs / 86_400_000;
    const t = (key: string) => this.translationService.translate(key);

    if (days >= 365) {
      const years = Math.round(days / 365);
      return `${years} ${t(years === 1 ? 'branches.gap.year' : 'branches.gap.years')}`;
    }
    if (days >= 30) {
      const months = Math.round(days / 30);
      return `${months} ${t(months === 1 ? 'branches.gap.month' : 'branches.gap.months')}`;
    }
    const d = Math.max(1, Math.round(days));
    return `${d} ${t(d === 1 ? 'branches.gap.day' : 'branches.gap.days')}`;
  }
}
