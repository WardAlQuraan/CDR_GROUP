import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSelectionListChange } from '@angular/material/list';
import { UserDto, AssignRolesDto } from '../../../../models/user.model';
import { RoleDto } from '../../../../models/role.model';
import { UsersService } from '../../../../services/users.service';
import { RolesService } from '../../../../services/roles.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface ManageRolesDialogData {
  user: UserDto;
}

@Component({
  selector: 'app-manage-roles-dialog',
  standalone: false,
  templateUrl: './manage-roles-dialog.component.html',
  styleUrl: './manage-roles-dialog.component.scss'
})
export class ManageRolesDialogComponent implements OnInit {
  loading = false;
  loadingRoles = false;

  allRoles: RoleDto[] = [];
  selectedRoleIds: Set<string> = new Set();

  constructor(
    private dialogRef: MatDialogRef<ManageRolesDialogComponent>,
    private usersService: UsersService,
    private rolesService: RolesService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: ManageRolesDialogData
  ) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private loadRoles(): void {
    this.loadingRoles = true;

    this.rolesService.getAll().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.allRoles = response.data;

          // Pre-select roles the user already has (match by name)
          const userRoleNames = new Set(this.data.user.roles || []);
          this.allRoles.forEach(role => {
            if (userRoleNames.has(role.name)) {
              this.selectedRoleIds.add(role.id);
            }
          });
        }
        this.loadingRoles = false;
        this.cdr.markForCheck();
      },
      error: (error) => {
        this.loadingRoles = false;
        this.cdr.markForCheck();
      }
    });
  }

  onSelectionChange(event: MatSelectionListChange): void {
    event.options.forEach(option => {
      if (option.selected) {
        this.selectedRoleIds.add(option.value);
      } else {
        this.selectedRoleIds.delete(option.value);
      }
    });
  }

  onSave(): void {
    this.loading = true;
    const dto: AssignRolesDto = {
      roleIds: Array.from(this.selectedRoleIds)
    };

    this.usersService.assignRoles(this.data.user.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.users.rolesUpdated'));
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
