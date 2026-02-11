import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PositionDto, CreatePositionDto, UpdatePositionDto } from '../../../../models/position.model';
import { PositionsService } from '../../../../services/positions.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export type PositionDialogMode = 'create' | 'edit';

export interface PositionDialogData {
  mode: PositionDialogMode;
  position?: PositionDto;
}

@Component({
  selector: 'app-position-dialog',
  standalone: false,
  templateUrl: './position-dialog.component.html',
  styleUrl: './position-dialog.component.scss'
})
export class PositionDialogComponent implements OnInit {
  form!: FormGroup;
  mode: PositionDialogMode;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PositionDialogComponent>,
    private positionsService: PositionsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: PositionDialogData
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
    return this.isCreateMode ? 'admin.positions.createPosition' : 'admin.positions.editPosition';
  }

  get saveLabel(): string {
    return this.isCreateMode ? 'common.create' : 'common.update';
  }

  private initForm(): void {
    if (this.isEditMode) {
      const pos = this.data.position!;
      this.form = this.fb.group({
        code: [pos.code, [Validators.required, Validators.maxLength(50)]],
        nameEn: [pos.nameEn, [Validators.required, Validators.maxLength(200)]],
        nameAr: [pos.nameAr, [Validators.required, Validators.maxLength(200)]],
        descriptionEn: [pos.descriptionEn, [Validators.maxLength(500)]],
        descriptionAr: [pos.descriptionAr, [Validators.maxLength(500)]],
        minSalary: [pos.minSalary, [Validators.min(0)]],
        maxSalary: [pos.maxSalary, [Validators.min(0)]],
        isActive: [pos.isActive]
      });
    } else {
      this.form = this.fb.group({
        code: ['', [Validators.required, Validators.maxLength(50)]],
        nameEn: ['', [Validators.required, Validators.maxLength(200)]],
        nameAr: ['', [Validators.required, Validators.maxLength(200)]],
        descriptionEn: ['', [Validators.maxLength(500)]],
        descriptionAr: ['', [Validators.maxLength(500)]],
        minSalary: [null, [Validators.min(0)]],
        maxSalary: [null, [Validators.min(0)]],
        isActive: [true]
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
      this.updatePosition();
    } else {
      this.createPosition();
    }
  }

  private createPosition(): void {
    const createDto: CreatePositionDto = {
      code: this.form.value.code,
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      minSalary: this.form.value.minSalary || undefined,
      maxSalary: this.form.value.maxSalary || undefined,
      isActive: this.form.value.isActive
    };

    this.positionsService.create(createDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.positions.positionCreated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  private updatePosition(): void {
    const updateDto: UpdatePositionDto = {
      code: this.form.value.code,
      nameEn: this.form.value.nameEn,
      nameAr: this.form.value.nameAr,
      descriptionEn: this.form.value.descriptionEn || undefined,
      descriptionAr: this.form.value.descriptionAr || undefined,
      minSalary: this.form.value.minSalary || undefined,
      maxSalary: this.form.value.maxSalary || undefined,
      isActive: this.form.value.isActive
    };

    this.positionsService.update(this.data.position!.id, updateDto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.positions.positionUpdated'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
