import { Component, Input, OnChanges, SimpleChanges, effect, ChangeDetectorRef, ViewChild, ElementRef, AfterViewInit, OnDestroy, NgZone } from '@angular/core';
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
export class TeamComponent implements OnChanges, AfterViewInit, OnDestroy {
  @Input() companyId = '';
  @ViewChild('scrollWrapper') scrollWrapper!: ElementRef<HTMLElement>;

  loading = false;
  error = false;
  teamData: TeamMember[] = [];
  flatMembers: TeamMember[] = [];

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

  // Drag & auto-scroll state
  private isDragging = false;
  private startX = 0;
  private scrollLeft = 0;
  private autoScrollId: number | null = null;
  private scrollSpeed = 0.5;
  private isPaused = false;

  constructor(
    private cdr: ChangeDetectorRef,
    private ngZone: NgZone,
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

  ngAfterViewInit(): void {
    this.startAutoScroll();
  }

  ngOnDestroy(): void {
    this.stopAutoScroll();
  }

  // --- Auto scroll ---
  private startAutoScroll(): void {
    this.ngZone.runOutsideAngular(() => {
      const tick = () => {
        if (!this.isPaused && !this.isDragging && this.scrollWrapper) {
          const el = this.scrollWrapper.nativeElement;
          el.scrollLeft += this.scrollSpeed;
          // Reset to start for seamless loop
          if (el.scrollLeft >= el.scrollWidth / 2) {
            el.scrollLeft = 0;
          }
        }
        this.autoScrollId = requestAnimationFrame(tick);
      };
      this.autoScrollId = requestAnimationFrame(tick);
    });
  }

  private stopAutoScroll(): void {
    if (this.autoScrollId !== null) {
      cancelAnimationFrame(this.autoScrollId);
      this.autoScrollId = null;
    }
  }

  // --- Mouse events ---
  onMouseEnter(): void {
    this.isPaused = true;
  }

  onMouseLeave(): void {
    this.isPaused = false;
    this.isDragging = false;
  }

  onMouseDown(event: MouseEvent): void {
    this.isDragging = true;
    this.startX = event.pageX - this.scrollWrapper.nativeElement.offsetLeft;
    this.scrollLeft = this.scrollWrapper.nativeElement.scrollLeft;
  }

  onMouseMove(event: MouseEvent): void {
    if (!this.isDragging) return;
    event.preventDefault();
    const x = event.pageX - this.scrollWrapper.nativeElement.offsetLeft;
    const walk = (x - this.startX) * 2;
    this.scrollWrapper.nativeElement.scrollLeft = this.scrollLeft - walk;
  }

  onMouseUp(): void {
    this.isDragging = false;
  }

  // --- Touch events ---
  onTouchStart(event: TouchEvent): void {
    this.isPaused = true;
    this.startX = event.touches[0].pageX - this.scrollWrapper.nativeElement.offsetLeft;
    this.scrollLeft = this.scrollWrapper.nativeElement.scrollLeft;
  }

  onTouchMove(event: TouchEvent): void {
    const x = event.touches[0].pageX - this.scrollWrapper.nativeElement.offsetLeft;
    const walk = (x - this.startX) * 2;
    this.scrollWrapper.nativeElement.scrollLeft = this.scrollLeft - walk;
  }

  onTouchEnd(): void {
    this.isPaused = false;
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
          this.flatMembers = this.flattenMembers(this.teamData);
          console.log('Loaded team members:', this.flatMembers);
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

  hasChildren(member: TeamMember): boolean {
    return member.children && member.children.length > 0;
  }

  private flattenMembers(members: TeamMember[]): TeamMember[] {
    const result: TeamMember[] = [];
    for (const member of members) {
      result.push(member);
      if (member.children?.length) {
        result.push(...this.flattenMembers(member.children));
      }
    }
    return result;
  }
}
