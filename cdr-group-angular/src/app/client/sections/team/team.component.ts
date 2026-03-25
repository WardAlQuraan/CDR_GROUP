import { Component, Input, OnChanges, SimpleChanges, effect, ChangeDetectorRef, ViewChild, ElementRef, AfterViewInit, OnDestroy, NgZone, HostListener } from '@angular/core';
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
  @ViewChild('swiperViewport') swiperViewport!: ElementRef<HTMLElement>;

  loading = false;
  error = false;
  teamData: TeamMember[] = [];
  flatMembers: TeamMember[] = [];

  // Swiper state
  currentIndex = 0;
  trackOffset = 0;
  isDragging = false;
  dotsArray: number[] = [];
  totalDots = 0;
  maxIndex = 0;

  private cardWidth = 256; // card width (240) + gap (16)
  private visibleCards = 4;
  private touchStartX = 0;
  private touchDeltaX = 0;
  private startOffset = 0;

  private colors = [
    '#D9A93E', '#3E423D', '#D9A93E', '#C4962E',
    '#5C8D89', '#7B68EE', '#FF6B6B', '#4ECDC4'
  ];

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
    this.calculateLayout();
  }

  ngOnDestroy(): void {}

  @HostListener('window:resize')
  onResize(): void {
    this.calculateLayout();
  }

  private calculateLayout(): void {
    if (!this.swiperViewport) return;
    const viewportWidth = this.swiperViewport.nativeElement.offsetWidth;

    if (viewportWidth <= 480) {
      this.cardWidth = 196; // 180 + 16
      this.visibleCards = 1;
    } else if (viewportWidth <= 768) {
      this.cardWidth = 216; // 200 + 16
      this.visibleCards = 2;
    } else if (viewportWidth <= 992) {
      this.cardWidth = 256;
      this.visibleCards = 3;
    } else {
      this.cardWidth = 256;
      this.visibleCards = 4;
    }

    this.maxIndex = Math.max(0, this.flatMembers.length - this.visibleCards);
    this.totalDots = this.maxIndex + 1;
    this.dotsArray = Array.from({ length: this.totalDots }, (_, i) => i);

    if (this.currentIndex > this.maxIndex) {
      this.currentIndex = this.maxIndex;
    }
    this.updateTrackOffset();
  }

  private updateTrackOffset(): void {
    this.trackOffset = -(this.currentIndex * this.cardWidth);
  }

  // Navigation
  prevSlide(): void {
    if (this.currentIndex > 0) {
      this.currentIndex--;
      this.updateTrackOffset();
    }
  }

  nextSlide(): void {
    if (this.currentIndex < this.maxIndex) {
      this.currentIndex++;
      this.updateTrackOffset();
    }
  }

  goToSlide(index: number): void {
    this.currentIndex = Math.min(index, this.maxIndex);
    this.updateTrackOffset();
  }

  // Mouse events
  onMouseDown(event: MouseEvent): void {
    this.isDragging = true;
    this.touchStartX = event.clientX;
    this.startOffset = this.trackOffset;
    event.preventDefault();
  }

  onMouseMove(event: MouseEvent): void {
    if (!this.isDragging) return;
    this.touchDeltaX = event.clientX - this.touchStartX;
    this.trackOffset = this.startOffset + this.touchDeltaX;
  }

  onMouseUp(): void {
    if (!this.isDragging) return;
    this.isDragging = false;

    const cardsMoved = Math.round(Math.abs(this.touchDeltaX) / this.cardWidth) || 1;

    if (this.touchDeltaX < -40) {
      this.currentIndex = Math.min(this.currentIndex + cardsMoved, this.maxIndex);
    } else if (this.touchDeltaX > 40) {
      this.currentIndex = Math.max(this.currentIndex - cardsMoved, 0);
    }

    this.touchDeltaX = 0;
    this.updateTrackOffset();
  }

  // Touch events
  onTouchStart(event: TouchEvent): void {
    this.isDragging = true;
    this.touchStartX = event.touches[0].clientX;
    this.startOffset = this.trackOffset;
  }

  onTouchMove(event: TouchEvent): void {
    if (!this.isDragging) return;
    this.touchDeltaX = event.touches[0].clientX - this.touchStartX;
    this.trackOffset = this.startOffset + this.touchDeltaX;
  }

  onTouchEnd(): void {
    this.isDragging = false;

    const cardsMoved = Math.round(Math.abs(this.touchDeltaX) / this.cardWidth) || 1;

    if (this.touchDeltaX < -40) {
      this.currentIndex = Math.min(this.currentIndex + cardsMoved, this.maxIndex);
    } else if (this.touchDeltaX > 40) {
      this.currentIndex = Math.max(this.currentIndex - cardsMoved, 0);
    }

    this.touchDeltaX = 0;
    this.updateTrackOffset();
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
          this.currentIndex = 0;
        }
        this.loading = false;
        this.cdr.markForCheck();
        setTimeout(() => this.calculateLayout());
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
