import { Component, Input, OnChanges, SimpleChanges, effect, ChangeDetectorRef } from '@angular/core';
import { EmployeesService } from '../../../services/employees.service';
import { TranslationService } from '../../../services/translation.service';
import { EmployeeTreeNodeDto } from '../../../models/employee.model';

interface TeamMember {
  id: string;
  name: string;
  position: string;
  company: string;
  initials: string;
  image?: string;
  imageError?: boolean;
  children: TeamMember[];
}

@Component({
  selector: 'app-team',
  standalone: false,
  templateUrl: './team.component.html',
  styleUrl: './team.component.scss',
})
export class TeamComponent implements OnChanges {
  @Input() companyId = '';

  loading = false;
  error = false;
  teamData: TeamMember[] = [];

  private colors = [
    '#D9A93E', '#3E423D', '#D9A93E', '#C4962E',
    '#5C8D89', '#7B68EE', '#FF6B6B', '#4ECDC4'
  ];

  constructor(
    private cdr: ChangeDetectorRef,
    private employeesService: EmployeesService,
    private translationService: TranslationService
  ) {
    effect(() => {
      this.translationService.language();
      if (this.teamData.length > 0) {
        this.loadData();
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyId']) {
      this.loadData();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadData(): void {
    this.loading = true;
    this.error = false;

    this.employeesService.getTree(this.companyId || undefined).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.teamData = response.data.map(node => this.mapToTeamMember(node));
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
        this.error = true;
      }
    });
  }

  private mapToTeamMember(node: EmployeeTreeNodeDto): TeamMember {
    const name = this.isArabic ? node.fullNameAr : node.fullNameEn;
    const firstName = this.isArabic ? node.firstNameAr : node.firstNameEn;
    const lastName = this.isArabic ? node.lastNameAr : node.lastNameEn;
    return {
      id: node.id,
      name: name || '',
      position: (this.isArabic ? node.positionNameAr : node.positionNameEn) || '',
      company: (this.isArabic ? node.companyNameAr : node.companyNameEn) || '',
      initials: this.getInitials(firstName, lastName),
      image: node.filePath || undefined,
      children: node.children?.map(child => this.mapToTeamMember(child)) || []
    };
  }

  private getInitials(firstName: string, lastName: string): string {
    const f = firstName?.[0] || '';
    const l = lastName?.[0] || '';
    return (f + l).toUpperCase() || '?';
  }

  getColor(index: number): string {
    return this.colors[index % this.colors.length];
  }

  /**
   * Returns an SVG path that curves from the top-center (the parent's exit point)
   * down to a specific child's top-center. The viewBox is 0..100 in both axes
   * and the SVG stretches with preserveAspectRatio="none" so the curves align
   * with each child's horizontal position regardless of card size.
   */
  getBranchPath(index: number, total: number): string {
    if (total <= 0) return '';
    const x = total === 1 ? 50 : ((index + 0.5) * (100 / total));
    // Smooth S-curve: vertical out of parent, bending toward the child.
    return `M 50 0 C 50 35, ${x} 65, ${x} 100`;
  }

  branchIndices(total: number): number[] {
    return Array.from({ length: total }, (_, i) => i);
  }
}
