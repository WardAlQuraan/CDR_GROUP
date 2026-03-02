import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CompanyContactDto, CreateCompanyContactDto, UpdateCompanyContactDto } from '../../../../models/company-contact.model';
import { CompanyContactsService } from '../../../../services/company-contacts.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type CompanyContactDialogMode = 'create' | 'edit';

export interface CompanyContactDialogData {
  mode: CompanyContactDialogMode;
  companyId: string;
  contact?: CompanyContactDto;
}

@Component({
  selector: 'app-company-contact-dialog',
  standalone: false,
  templateUrl: './company-contact-dialog.component.html',
  styleUrl: './company-contact-dialog.component.scss'
})
export class CompanyContactDialogComponent implements OnInit {
  form!: FormGroup;
  mode: CompanyContactDialogMode;
  loading = false;

  contactIcons = [
    { value: 'bi-telephone', label: 'Phone' },
    { value: 'bi-phone', label: 'Mobile' },
    { value: 'bi-envelope', label: 'Email' },
    { value: 'bi-geo-alt', label: 'Location' },
    { value: 'bi-globe', label: 'Website' },
    { value: 'bi-printer', label: 'Fax' },
    { value: 'bi-whatsapp', label: 'WhatsApp' },
    { value: 'bi-telegram', label: 'Telegram' },
    { value: 'bi-facebook', label: 'Facebook' },
    { value: 'bi-instagram', label: 'Instagram' },
    { value: 'bi-linkedin', label: 'LinkedIn' },
    { value: 'bi-twitter-x', label: 'X (Twitter)' },
    { value: 'bi-link-45deg', label: 'Link' }
  ];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyContactDialogComponent>,
    private companyContactsService: CompanyContactsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyContactDialogData
  ) {
    this.mode = data.mode;
  }

  ngOnInit(): void {
    this.initForm();
  }

  get isCreateMode(): boolean {
    return this.mode === 'create';
  }

  get isEditMode(): boolean {
    return this.mode === 'edit';
  }

  get dialogTitle(): string {
    return this.isCreateMode ? 'admin.companyContacts.createContact' : 'admin.companyContacts.editContact';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const contact = this.data.contact!;
      this.form = this.fb.group({
        icon: [contact.icon, [Validators.required, Validators.maxLength(100)]],
        name: [contact.name, [Validators.required, Validators.maxLength(200)]],
        value: [contact.value, [Validators.required, Validators.maxLength(500)]]
      });
    } else {
      this.form = this.fb.group({
        icon: ['', [Validators.required, Validators.maxLength(100)]],
        name: ['', [Validators.required, Validators.maxLength(200)]],
        value: ['', [Validators.required, Validators.maxLength(500)]]
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
      this.updateContact();
    } else {
      this.createContact();
    }
  }

  private createContact(): void {
    const createDto: CreateCompanyContactDto = {
      icon: this.form.value.icon,
      name: this.form.value.name,
      value: this.form.value.value,
      companyId: this.data.companyId
    };

    this.companyContactsService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyContacts.contactCreated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updateContact(): void {
    const updateDto: UpdateCompanyContactDto = {
      icon: this.form.value.icon,
      name: this.form.value.name,
      value: this.form.value.value
    };

    this.companyContactsService.update(this.data.contact!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.companyContacts.contactUpdated'));
        this.dialogRef.close(true);
      },
      error: () => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
