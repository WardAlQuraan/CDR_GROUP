import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EventDto } from '../../../../models/event.model';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-event-view-dialog',
  standalone: false,
  templateUrl: './event-view-dialog.component.html',
  styleUrl: './event-view-dialog.component.scss'
})
export class EventViewDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<EventViewDialogComponent>,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public event: EventDto
  ) {}

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  getDepartmentName(): string {
    if (!this.event.departmentId) return '-';
    return this.isArabic ? this.event.departmentNameAr || '-' : this.event.departmentNameEn || '-';
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
