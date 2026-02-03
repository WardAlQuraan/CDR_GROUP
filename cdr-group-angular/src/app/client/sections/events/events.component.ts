import { ChangeDetectorRef, Component, OnInit, effect } from '@angular/core';
import { EventsService } from '../../../services/events.service';
import { TranslationService } from '../../../services/translation.service';
import { EventDto } from '../../../models/event.model';
import { PagedResult } from '../../../models/paged.model';

interface DisplayEvent {
  id: string;
  title: string;
  description: string;
  department: string;
  eventUrl?: string;
  eventDate?: Date;
  primaryFileUrl?: string;
}

@Component({
  selector: 'app-events',
  standalone: false,
  templateUrl: './events.component.html',
  styleUrl: './events.component.scss',
})
export class EventsComponent implements OnInit {
  loading = false;
  paginationLoading = false;
  error = false;
  events: DisplayEvent[] = [];

  // Pagination
  pageNumber = 1;
  pageSize = 10;
  totalPages = 0;
  totalCount = 0;
  hasPreviousPage = false;
  hasNextPage = false;

  private colors = [
    'linear-gradient(135deg, #81B29A 0%, #5a9178 100%)',
    'linear-gradient(135deg, #3D405B 0%, #2a2d40 100%)',
    'linear-gradient(135deg, #F2CC8F 0%, #e0b67a 100%)',
    'linear-gradient(135deg, #E07A5F 0%, #c4644d 100%)'
  ];

  constructor(
    private eventsService: EventsService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      if (this.events.length > 0) {
        this.loadEvents();
      }
    });
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadEvents(isPagination = false): void {
    if (isPagination) {
      this.paginationLoading = true;
    } else {
      this.loading = true;
    }
    this.error = false;

    this.eventsService.getPaged({
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      sortBy: 'eventDate',
      sortDescending: true
    }).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          const pagedResult = response.data;
          this.events = pagedResult.data.map(event => this.mapToDisplayEvent(event));
          this.totalPages = pagedResult.totalPages;
          this.totalCount = pagedResult.totalCount;
          this.hasPreviousPage = pagedResult.hasPreviousPage;
          this.hasNextPage = pagedResult.hasNextPage;
        }
        this.loading = false;
        this.paginationLoading = false;
        this.cdr.markForCheck();

      },
      error: () => {
        this.loading = false;
        this.paginationLoading = false;
        this.error = true;
        this.cdr.markForCheck();

      }
    });
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.pageNumber) {
      this.pageNumber = page;
      this.loadEvents(true);
    }
  }

  nextPage(): void {
    if (this.hasNextPage) {
      this.pageNumber++;
      this.loadEvents(true);
    }
  }

  previousPage(): void {
    if (this.hasPreviousPage) {
      this.pageNumber--;
      this.loadEvents(true);
    }
  }

  private mapToDisplayEvent(event: EventDto): DisplayEvent {
    return {
      id: event.id,
      title: this.isArabic ? event.titleAr : event.titleEn,
      description: (this.isArabic ? event.descriptionAr : event.descriptionEn) || '',
      department: (this.isArabic ? event.departmentNameAr : event.departmentNameEn) || '',
      eventUrl: event.eventUrl,
      eventDate: event.eventDate,
      primaryFileUrl: event.primaryFileUrl
    };
  }

  getColor(index: number): string {
    return this.colors[index % this.colors.length];
  }

  getDateDay(date: Date): string {
    return new Date(date).getDate().toString().padStart(2, '0');
  }

  getDateMonth(date: Date): string {
    const months = this.isArabic
      ? ['يناير', 'فبراير', 'مارس', 'أبريل', 'مايو', 'يونيو', 'يوليو', 'أغسطس', 'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر']
      : ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return months[new Date(date).getMonth()];
  }

  getDateYear(date: Date): string {
    return new Date(date).getFullYear().toString();
  }

  openEventDetail(event: DisplayEvent): void {
    window.open(`/events/${event.id}`, '_blank');
  }

  openExternalLink(event: DisplayEvent, e: Event): void {
    e.stopPropagation();
    if (event.eventUrl) {
      window.open(event.eventUrl, '_blank');
    }
  }
}
