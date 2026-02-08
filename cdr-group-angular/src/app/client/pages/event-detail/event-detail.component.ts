import { ChangeDetectorRef, Component, OnInit, effect } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EventsService } from '../../../services/events.service';
import { FilesService } from '../../../services/files.service';
import { TranslationService } from '../../../services/translation.service';
import { EventDto } from '../../../models/event.model';
import { FileAttachmentDto } from '../../../models/file-attachment.model';
import { ImagePreviewDialogComponent } from '../../../shared/components/image-preview-dialog/image-preview-dialog.component';

interface DisplayEvent {
  id: string;
  title: string;
  description: string;
  company: string;
  eventUrl?: string;
  eventDate?: Date;
  primaryFileUrl?: string;
}

@Component({
  selector: 'app-event-detail',
  standalone: false,
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss',
})
export class EventDetailComponent implements OnInit {
  loading = false;
  error = false;
  event: DisplayEvent | null = null;
  images: FileAttachmentDto[] = [];
  loadingImages = false;
  private rawEvent: EventDto | null = null;

  constructor(
    private route: ActivatedRoute,
    private eventsService: EventsService,
    private filesService: FilesService,
    private translationService: TranslationService,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) {
    effect(() => {
      this.translationService.language();
      if (this.rawEvent) {
        this.event = this.mapToDisplayEvent(this.rawEvent);
      }
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadEvent(id);
    }
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loadEvent(id: string): void {
    this.loading = true;
    this.error = false;

    this.eventsService.getById(id).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.rawEvent = response.data;
          this.event = this.mapToDisplayEvent(response.data);
          this.loadImages(id);
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.error = true;
        this.cdr.markForCheck();
      }
    });
  }

  private mapToDisplayEvent(event: EventDto): DisplayEvent {
    return {
      id: event.id,
      title: this.isArabic ? event.titleAr : event.titleEn,
      description: (this.isArabic ? event.descriptionAr : event.descriptionEn) || '',
      company: (this.isArabic ? event.companyNameAr : event.companyName) || '',
      eventUrl: event.eventUrl,
      eventDate: event.eventDate,
      primaryFileUrl: event.primaryFileUrl
    };
  }

  getFormattedDate(date: Date): string {
    const d = new Date(date);
    const day = d.getDate().toString().padStart(2, '0');
    const months = this.isArabic
      ? ['يناير', 'فبراير', 'مارس', 'أبريل', 'مايو', 'يونيو', 'يوليو', 'أغسطس', 'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر']
      : ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const month = months[d.getMonth()];
    const year = d.getFullYear();
    return `${day} ${month} ${year}`;
  }

  openEventUrl(): void {
    if (this.event?.eventUrl) {
      window.open(this.event.eventUrl, '_blank');
    }
  }

  openImagePreview(imageUrl: string): void {
    this.dialog.open(ImagePreviewDialogComponent, {
      data: { imageUrl },
      panelClass: 'image-preview-dialog',
      maxWidth: '95vw',
      maxHeight: '95vh'
    });
  }

  private loadImages(eventId: string): void {
    this.loadingImages = true;
    this.filesService.getByEntityId(eventId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.images = response.data;
        }
        this.loadingImages = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingImages = false;
        this.cdr.markForCheck();
      }
    });
  }
}
