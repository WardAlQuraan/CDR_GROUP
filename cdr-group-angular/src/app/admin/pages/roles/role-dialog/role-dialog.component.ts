import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { RoleDto, CreateRoleDto, UpdateRoleDto } from '../../../../models/role.model';
import { RolesService } from '../../../../services/roles.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface RoleDialogData {
  mode: 'create' | 'edit' | 'view';
  role?: RoleDto;
}

@Component({
  selector: 'app-role-dialog',
  standalone: false,
  templateUrl: './role-dialog.component.html',
  styleUrl: './role-dialog.component.scss'
})
export class RoleDialogComponent implements OnInit {
  form!: FormGroup;
  mode: string;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RoleDialogComponent>,
    private rolesService: RolesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: RoleDialogData
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

  get isViewMode(): boolean {
    return this.mode === 'view';
  }

  get dialogTitle(): string {
    switch (this.mode) {
      case 'create': return 'admin.roles.createRole';
      case 'edit': return 'admin.roles.editRole';
      case 'view': return 'admin.roles.viewRole';
      default: return '';
    }
  }

  get saveLabel(): string {
    switch (this.mode) {
      case 'create': return 'common.create';
      case 'edit': return 'common.update';
      default: return 'common.close';
    }
  }

  private initForm(): void {
    if (this.isViewMode) {
      this.form = this.fb.group({});
    } else if (this.isEditMode) {
      this.form = this.fb.group({
        name: [this.data.role?.name, [Validators.required, Validators.minLength(2)]],
        description: [this.data.role?.description]
      });
    } else {
      this.form = this.fb.group({
        name: ['', [Validators.required, Validators.minLength(2)]],
        description: ['']
      });
    }
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    if (this.isViewMode) {
      this.dialogRef.close();
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    if (this.isEditMode) {
      const updateDto: UpdateRoleDto = {
        name: this.form.value.name,
        description: this.form.value.description
      };

      this.rolesService.update(this.data.role!.id, updateDto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.roles.roleUpdated'));
          this.dialogRef.close(true);
        },
        error: (error) => {
          this.snackbar.error(error.message || this.translate('admin.roles.errors.updateFailed'));
          this.loading = false;
        }
      });
    } else {
      const createDto: CreateRoleDto = {
        name: this.form.value.name,
        description: this.form.value.description
      };

      this.rolesService.create(createDto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.roles.roleCreated'));
          this.dialogRef.close(true);
        },
        error: (error) => {
          this.snackbar.error(error.message || this.translate('admin.roles.errors.createFailed'));
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
