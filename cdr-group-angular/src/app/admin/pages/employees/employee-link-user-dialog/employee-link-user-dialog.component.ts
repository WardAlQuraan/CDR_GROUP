import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { EmployeeDto } from '../../../../models/employee.model';
import { UserDto } from '../../../../models/user.model';
import { ApiResponse } from '../../../../models/api-response.model';
import { EmployeesService } from '../../../../services/employees.service';
import { UsersService } from '../../../../services/users.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';
import { SelectOption } from '../../../../shared/components/async-select/async-select.component';

@Component({
  selector: 'app-employee-link-user-dialog',
  standalone: false,
  templateUrl: './employee-link-user-dialog.component.html',
  styleUrl: './employee-link-user-dialog.component.scss'
})
export class EmployeeLinkUserDialogComponent implements OnInit {
  form!: FormGroup;
  loading = false;
  usersSource$!: Observable<ApiResponse<UserDto[]>>;
  userMapper = (user: UserDto): SelectOption => ({
    value: user.id,
    label: `${user.username} (${user.email})`
  });
  activeUserFilter = (user: UserDto): boolean => user.isActive;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EmployeeLinkUserDialogComponent>,
    private employeesService: EmployeesService,
    private usersService: UsersService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public employee: EmployeeDto
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.usersSource$ = this.usersService.getAll();
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  private initForm(): void {
    this.form = this.fb.group({
      userId: [this.employee.userId || null]
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
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
