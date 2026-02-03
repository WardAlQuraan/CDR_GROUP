import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeDto } from '../../../../models/employee.model';
import { UserDto } from '../../../../models/user.model';
import { EmployeesService } from '../../../../services/employees.service';
import { UsersService } from '../../../../services/users.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

@Component({
  selector: 'app-employee-link-user-dialog',
  standalone: false,
  templateUrl: './employee-link-user-dialog.component.html',
  styleUrl: './employee-link-user-dialog.component.scss'
})
export class EmployeeLinkUserDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  loadingUsers = false;
  availableUsers: UserDto[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeLinkUserDialogComponent>,
    private employeesService: EmployeesService,
    private usersService: UsersService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    @Inject(MAT_DIALOG_DATA) public employee: EmployeeDto
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadAvailableUsers();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initForm(): void {
    this.form = this.fb.group({
      userId: [this.employee.userId || null]
    });
  }

  private loadAvailableUsers(): void {
    this.loadingUsers = true;
    this.usersService.getAll().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.availableUsers = response.data.filter(u => u.isActive);
        }
        this.loadingUsers = false;
      },
      error: () => {
        this.loadingUsers = false;
      }
    });
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  onSubmit(): void {
    this.loading = true;
    const userId = this.form.value.userId || null;

    this.employeesService.linkToUser(this.employee.id, userId).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.employees.linkedToUser'));
        this.dialogRef.close(true);
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.employees.errors.linkToUserFailed'));
        this.loading = false;
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
