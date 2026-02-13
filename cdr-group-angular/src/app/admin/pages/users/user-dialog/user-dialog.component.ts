import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserDto, CreateUserDto, UpdateUserDto } from '../../../../models/user.model';
import { UsersService } from '../../../../services/users.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface UserDialogData {
  mode: 'create' | 'edit';
  user?: UserDto;
}

@Component({
  selector: 'app-user-dialog',
  standalone: false,
  templateUrl: './user-dialog.component.html',
  styleUrl: './user-dialog.component.scss'
})
export class UserDialogComponent implements OnInit {
  form!: FormGroup;
  isEditMode: boolean;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<UserDialogComponent>,
    private usersService: UsersService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public data: UserDialogData,
    private cdr: ChangeDetectorRef
  ) {
    this.isEditMode = data.mode === 'edit';
  }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    if (this.isEditMode) {
      this.form = this.fb.group({
        email: [this.data.user?.email, [Validators.required, Validators.email]],
        firstName: [this.data.user?.firstName],
        lastName: [this.data.user?.lastName],
        phoneNumber: [this.data.user?.phoneNumber, [Validators.pattern(/^00962\d{9}$/)]]
      });
    } else {
      this.form = this.fb.group({
        username: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        firstName: [''],
        lastName: [''],
        phoneNumber: ['', [Validators.pattern(/^00962\d{9}$/)]]
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
      const updateDto: UpdateUserDto = {
        email: this.form.value.email,
        firstName: this.form.value.firstName,
        lastName: this.form.value.lastName,
        phoneNumber: this.form.value.phoneNumber
      };

      this.usersService.update(this.data.user!.id, updateDto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.users.userUpdated'));
          this.dialogRef.close(true);
        },
        error: (error) => {
          this.cdr.markForCheck();
          this.loading = false;
        }
      });
    } else {
      const createDto: CreateUserDto = {
        username: this.form.value.username,
        email: this.form.value.email,
        password: this.form.value.password,
        firstName: this.form.value.firstName,
        lastName: this.form.value.lastName,
        phoneNumber: this.form.value.phoneNumber
      };

      this.usersService.create(createDto).subscribe({
        next: () => {
          this.snackbar.success(this.translate('admin.users.userCreated'));
          this.dialogRef.close(true);
        },
        error: (error) => {
          this.cdr.markForCheck();
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
