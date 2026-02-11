import { ChangeDetectorRef, Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UsersService } from '../../../../services/users.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface ChangePasswordDialogData {
  userId: string;
  username: string;
}

@Component({
  selector: 'app-change-password-dialog',
  standalone: false,
  templateUrl: './change-password-dialog.component.html',
  styleUrl: './change-password-dialog.component.scss'
})
export class ChangePasswordDialogComponent {
  form: FormGroup;
  loading = false;
  hideNewPassword = true;
  hideConfirmPassword = true;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ChangePasswordDialogComponent>,
    private usersService: UsersService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: ChangePasswordDialogData
  ) {
    this.form = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  get passwordsMismatch(): boolean {
    return this.form.get('newPassword')?.value !== this.form.get('confirmPassword')?.value
      && !!this.form.get('confirmPassword')?.touched;
  }

  onSubmit(): void {
    if (this.form.invalid || this.passwordsMismatch) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    this.usersService.changePassword(this.data.userId, {
      currentPassword: '',
      newPassword: this.form.value.newPassword
    }).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.users.passwordChanged'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.cdr.markForCheck();
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
