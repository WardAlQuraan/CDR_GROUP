import { Component, OnInit, effect } from '@angular/core';
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
export class TeamComponent implements OnInit {
  loading = false;
  error = false;
  teamData: TeamMember[] = [];

  private colors = [
    '#81B29A',
    '#3D405B',
    '#F2CC8F',
    '#E07A5F',
    '#5C8D89',
    '#7B68EE',
    '#FF6B6B',
    '#4ECDC4'
  ];

  constructor(
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

  ngOnInit(): void {
    this.loadData();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadData(): void {
    this.loading = true;
    this.error = false;

    this.employeesService.getTree().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.teamData = response.data.map(node => this.mapToTeamMember(node));
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
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
