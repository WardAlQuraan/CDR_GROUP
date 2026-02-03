import { Component, Inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSelectionListChange } from '@angular/material/list';
import { RoleDto, PermissionDto, AssignPermissionsDto } from '../../../../models/role.model';
import { RolesService, PermissionsService } from '../../../../services/roles.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface PermissionsDialogData {
  role: RoleDto;
}

interface PermissionGroup {
  module: string;
  permissions: PermissionDto[];
  selectedIds: Set<string>;
}

@Component({
  selector: 'app-permissions-dialog',
  standalone: false,
  templateUrl: './permissions-dialog.component.html',
  styleUrl: './permissions-dialog.component.scss'
})
export class PermissionsDialogComponent implements OnInit {
  loading = false;
  loadingPermissions = false;

  allPermissions: PermissionDto[] = [];
  permissionGroups: PermissionGroup[] = [];
  selectedPermissionIds: Set<string> = new Set();

  constructor(
    private dialogRef: MatDialogRef<PermissionsDialogComponent>,
    private rolesService: RolesService,
    private permissionsService: PermissionsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: PermissionsDialogData
  ) {}

  ngOnInit(): void {
    this.loadPermissions();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private loadPermissions(): void {
    if (!this.data.role?.id) {
      return;
    }

    this.loadingPermissions = true;

    // First load all permissions
    this.permissionsService.getAll().subscribe({
      next: (allPermissionsResponse) => {
        debugger;
        if (allPermissionsResponse.success && allPermissionsResponse.data) {
          this.allPermissions = allPermissionsResponse.data;
          this.groupPermissionsByModule();

          // Then load role's current permissions for pre-selection
          this.permissionsService.getByRoleId(this.data.role.id).subscribe({
            next: (rolePermissionsResponse) => {
              if (rolePermissionsResponse.success && rolePermissionsResponse.data) {
                const rolePermissionIds = new Set(rolePermissionsResponse.data.map(p => p.id));

                rolePermissionIds.forEach(id => {
                  this.selectedPermissionIds.add(id);
                });

                // Update groups with selected permissions
                this.permissionGroups.forEach(group => {
                  group.permissions.forEach(p => {
                    if (rolePermissionIds.has(p.id)) {
                      group.selectedIds.add(p.id);
                    }
                  });
                });
              }
              this.loadingPermissions = false;
              this.cdr.detectChanges();
            },
            error: (error) => {
              this.snackbar.error(error.message || this.translate('admin.roles.errors.loadPermissionsFailed'));
              this.loadingPermissions = false;
              this.cdr.detectChanges();
            }
          });
        } else {
          this.loadingPermissions = false;
          this.cdr.detectChanges();
        }
      },
      error: (error) => {
        this.snackbar.error(error.message || this.translate('admin.roles.errors.loadPermissionsFailed'));
        this.loadingPermissions = false;
        this.cdr.detectChanges();
      }
    });
  }

  private groupPermissionsByModule(): void {
    const groupMap = new Map<string, PermissionDto[]>();

    this.allPermissions.forEach(permission => {
      const module = permission.module || 'Other';
      if (!groupMap.has(module)) {
        groupMap.set(module, []);
      }
      groupMap.get(module)!.push(permission);
    });

    this.permissionGroups = Array.from(groupMap.entries()).map(([module, permissions]) => ({
      module,
      permissions,
      selectedIds: new Set<string>()
    }));
  }

  togglePermission(permissionId: string, group: PermissionGroup): void {
    if (this.selectedPermissionIds.has(permissionId)) {
      this.selectedPermissionIds.delete(permissionId);
      group.selectedIds.delete(permissionId);
    } else {
      this.selectedPermissionIds.add(permissionId);
      group.selectedIds.add(permissionId);
    }
  }

  toggleAllInGroup(group: PermissionGroup): void {
    const allSelected = group.permissions.every(p => group.selectedIds.has(p.id));
    if (allSelected) {
      group.permissions.forEach(p => {
        group.selectedIds.delete(p.id);
        this.selectedPermissionIds.delete(p.id);
      });
    } else {
      group.permissions.forEach(p => {
        group.selectedIds.add(p.id);
        this.selectedPermissionIds.add(p.id);
      });
    }
  }

  isAllSelectedInGroup(group: PermissionGroup): boolean {
    return group.permissions.length > 0 && group.permissions.every(p => group.selectedIds.has(p.id));
  }

  isSomeSelectedInGroup(group: PermissionGroup): boolean {
    return group.selectedIds.size > 0 && !this.isAllSelectedInGroup(group);
  }

  onSelectionChange(event: MatSelectionListChange, group: PermissionGroup): void {
    event.options.forEach(option => {
      const permissionId = option.value;
      if (option.selected) {
        this.selectedPermissionIds.add(permissionId);
        group.selectedIds.add(permissionId);
      } else {
        this.selectedPermissionIds.delete(permissionId);
        group.selectedIds.delete(permissionId);
      }
    });
  }

  onSave(): void {
    this.loading = true;
    const dto: AssignPermissionsDto = {
      permissionIds: Array.from(this.selectedPermissionIds)
    };

    this.rolesService.assignPermissions(this.data.role.id, dto).subscribe({
      next: () => {
        this.snackbar.success(this.translate('admin.roles.permissionsUpdated'));
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
