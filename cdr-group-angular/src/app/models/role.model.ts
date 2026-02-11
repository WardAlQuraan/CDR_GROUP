export interface RoleDto {
  id: string;
  name: string;
  description?: string;
  isSystemRole: boolean;
  createdAt: Date;
  createdBy?: string;
  permissions: string[];
}

export interface CreateRoleDto {
  name: string;
  description?: string;
  permissionIds?: string[];
}

export interface UpdateRoleDto {
  name?: string;
  description?: string;
}

export interface AssignPermissionsDto {
  permissionIds: string[];
}

export interface PermissionDto {
  id: string;
  name: string;
  description?: string;
  module: string;
  createdAt: Date;
}
