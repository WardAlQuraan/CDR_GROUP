import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  UserDto,
  CreateUserDto,
  UpdateUserDto,
  ChangePasswordDto,
  AssignRolesDto
} from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends BaseService<UserDto, CreateUserDto, UpdateUserDto> {

  constructor(http: HttpClient) {
    super(http, 'users');
  }

  getByUsername(username: string): Observable<ApiResponse<UserDto>> {
    return this.get<UserDto>(`/by-username/${username}`);
  }

  getByEmail(email: string): Observable<ApiResponse<UserDto>> {
    return this.get<UserDto>(`/by-email/${email}`);
  }

  changePassword(id: string, dto: ChangePasswordDto): Observable<ApiResponse<void>> {
    return this.post<ChangePasswordDto, void>(`/${id}/change-password`, dto);
  }

  assignRoles(id: string, dto: AssignRolesDto): Observable<ApiResponse<UserDto>> {
    return this.post<AssignRolesDto, UserDto>(`/${id}/roles`, dto);
  }

  removeRoles(id: string, roleIds: string[]): Observable<ApiResponse<UserDto>> {
    return this.http.delete<ApiResponse<UserDto>>(`${this.getApiUrl()}/${id}/roles`, {
      body: roleIds
    }).pipe(catchError(error => this.handleError(error)));
  }

  activate(id: string): Observable<ApiResponse<void>> {
    return this.post<void, void>(`/${id}/activate`);
  }

  deactivate(id: string): Observable<ApiResponse<void>> {
    return this.post<void, void>(`/${id}/deactivate`);
  }
}
