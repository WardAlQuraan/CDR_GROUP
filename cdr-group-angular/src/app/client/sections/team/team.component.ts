import { Component, Input, OnChanges, SimpleChanges, effect, ChangeDetectorRef } from '@angular/core';
import { EmployeesService } from '../../../services/employees.service';
import { TranslationService } from '../../../services/translation.service';
import { EmployeeTreeNodeDto } from '../../../models/employee.model';

interface TeamMember {
  id: string;
  name: string;
  position: string;
  department: string;
  initials: string;
  image?: string;
  children: TeamMember[];
}

@Component({
  selector: 'app-team',
  standalone: false,
  templateUrl: './team.component.html',
  styleUrl: './team.component.scss',
})
export class TeamComponent implements OnChanges {
  @Input() companyCode = 'CDR';

  loading = false;
  error = false;
  teamData: TeamMember[] = [];

  private colors = [
    '#D9A93E',
    '#3E423D',
    '#D9A93E',
    '#C4962E',
    '#5C8D89',
    '#7B68EE',
    '#FF6B6B',
    '#4ECDC4'
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
    if (changes['companyCode']) {
      this.loadData();
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadData(): void {
    this.loading = true;
    this.error = false;

    this.employeesService.getTree(undefined, this.companyCode || undefined).subscribe({
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
    return {
      id: node.id,
      name: name || '',
      position: (this.isArabic ? node.positionNameAr : node.positionNameEn) || '',
      department: (this.isArabic ? node.departmentNameAr : node.departmentNameEn) || '',
      initials: this.getInitials(name),
      image: node.filePath || undefined,
      children: node.children?.map(child => this.mapToTeamMember(child)) || []
    };
  }

  private getInitials(name: string): string {
    if (!name) return '?';
    return name
      .split(' ')
      .map(n => n[0])
      .join('')
      .substring(0, 2)
      .toUpperCase();
  }

  getColor(index: number): string {
    return this.colors[index % this.colors.length];
  }

  hasChildren(member: TeamMember): boolean {
    return member.children && member.children.length > 0;
  }
}
