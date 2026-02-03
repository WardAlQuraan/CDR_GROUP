import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  RoleDto,
  CreateRoleDto,
  UpdateRoleDto,
  AssignPermissionsDto,
  PermissionDto
} from '../models/role.model';

@Injectable({
  providedIn: 'root'
})
export class RolesService extends BaseService<RoleDto, CreateRoleDto, UpdateRoleDto> {

  constructor(http: HttpClient) {
    super(http, 'roles');
  }

  getByName(name: string): Observable<ApiResponse<RoleDto>> {
    return this.get<RoleDto>(`/by-name/${encodeURIComponent(name)}`);
  }

  assignPermissions(id: string, dto: AssignPermissionsDto): Observable<ApiResponse<RoleDto>> {
    return this.post<AssignPermissionsDto, RoleDto>(`/${id}/permissions`, dto);
  }

  removePermissions(id: string, permissionIds: string[]): Observable<ApiResponse<RoleDto>> {
    return this.http.delete<ApiResponse<RoleDto>>(`${this.getApiUrl()}/${id}/permissions`, {
      body: permissionIds
    }).pipe(catchError(error => this.handleError(error)));
  }
}

@Injectable({
  providedIn: 'root'
})
export class PermissionsService extends BaseService<PermissionDto> {

  constructor(http: HttpClient) {
    super(http, 'permissions');
  }

  getByName(name: string): Observable<ApiResponse<PermissionDto>> {
    return this.get<PermissionDto>(`/by-name/${encodeURIComponent(name)}`);
  }

  getByModule(module: string): Observable<ApiResponse<PermissionDto[]>> {
    return this.get<PermissionDto[]>(`/by-module/${encodeURIComponent(module)}`);
  }

  getByRoleId(roleId: string): Observable<ApiResponse<PermissionDto[]>> {
    return this.get<PermissionDto[]>(`/by-role/${roleId}`);
  }

  getByUserId(userId: string): Observable<ApiResponse<PermissionDto[]>> {
    return this.get<PermissionDto[]>(`/by-user/${userId}`);
  }
}
