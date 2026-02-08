import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { EventDto, CreateEventDto, UpdateEventDto } from '../../../../models/event.model';
import { CompanyDto } from '../../../../models/company.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { EventsService } from '../../../../services/events.service';
import { CompaniesService } from '../../../../services/companies.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';

export type EventDialogMode = 'create' | 'edit';

export interface EventDialogData {
  mode: EventDialogMode;
  event?: EventDto;
}

@Component({
  selector: 'app-event-dialog',
  standalone: false,
  templateUrl: './event-dialog.component.html',
  styleUrl: './event-dialog.component.scss'
})
export class EventDialogComponent implements OnInit {
  form!: FormGroup;
  mode: EventDialogMode;
  loading = false;

  companiesDataSource$!: Observable<ApiResponse<CompanyDto[]>>;

  companyMapper = (company: CompanyDto): SelectOption => ({
    value: company.id,
    label: `${this.isArabic ? company.nameAr : company.nameEn} (${company.code})`
  });

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EventDialogComponent>,
    private eventsService: EventsService,
    private companiesService: CompaniesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: EventDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.initForm();
    this.companiesDataSource$ = this.companiesService.getActiveCompanies();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get isCreateMode(): boolean {
    return this.mode === 'create';
  }

  get isEditMode(): boolean {
    return this.mode === 'edit';
  }

  get dialogTitle(): string {
    return this.isCreateMode ? 'admin.eventsAdmin.createEvent' : 'admin.eventsAdmin.editEvent';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const event = this.data.event!;
      this.form = this.fb.group({
        titleEn: [event.titleEn, [Validators.required, Validators.maxLength(200)]],
        titleAr: [event.titleAr, [Validators.required, Validators.maxLength(200)]],
        descriptionEn: [event.descriptionEn, [Validators.maxLength(2000)]],
        descriptionAr: [event.descriptionAr, [Validators.maxLength(2000)]],
        eventDate: [event.eventDate ? new Date(event.eventDate) : null],
        eventUrl: [event.eventUrl, [Validators.maxLength(500)]],
        companyId: [event.companyId, [Validators.required]]
      });
    } else {
      this.form = this.fb.group({
        titleEn: ['', [Validators.required, Validators.maxLength(200)]],
        titleAr: ['', [Validators.required, Validators.maxLength(200)]],
        descriptionEn: ['', [Validators.maxLength(2000)]],
        descriptionAr: ['', [Validators.maxLength(2000)]],
        eventDate: [null],
        eventUrl: ['', [Validators.maxLength(500)]],
        companyId: [null, [Validators.required]]
      });
    }
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    if (this.isEditMode) {
      this.updateEvent();
    } else {
      this.createEvent();
    }
  }

  private createEvent(): void {
    const createDto: CreateEventDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      eventDate: this.form.value.eventDate || undefined,
      eventUrl: this.form.value.eventUrl || undefined,
      companyId: this.form.value.companyId || undefined
    };

    this.eventsService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.eventsAdmin.eventCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.eventsAdmin.errors.createFailed'));
        this.loading = false;
      }
    });
  }

  private updateEvent(): void {
    const updateDto: UpdateEventDto = {
      titleEn: this.form.value.titleEn,
      titleAr: this.form.value.titleAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      eventDate: this.form.value.eventDate || undefined,
      eventUrl: this.form.value.eventUrl || undefined,
      companyId: this.form.value.companyId || undefined
    };

    this.eventsService.update(this.data.event!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.eventsAdmin.eventUpdated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.eventsAdmin.errors.updateFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
