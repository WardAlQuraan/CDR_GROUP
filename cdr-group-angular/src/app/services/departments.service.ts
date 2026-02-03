import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  DepartmentDto,
  DepartmentBasicDto,
  DepartmentWithSubDepartmentsDto,
  CreateDepartmentDto,
  UpdateDepartmentDto
} from '../models/department.model';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService extends BaseService<DepartmentDto, CreateDepartmentDto, UpdateDepartmentDto> {
  constructor(http: HttpClient) {
    super(http, 'departments');
  }

  getByCode(code: string): Observable<ApiResponse<DepartmentDto>> {
    return this.get<DepartmentDto>(`/by-code/${code}`);
  }

  getByName(name: string): Observable<ApiResponse<DepartmentDto>> {
    return this.get<DepartmentDto>(`/by-name/${name}`);
  }

  getWithSubDepartments(id: string): Observable<ApiResponse<DepartmentWithSubDepartmentsDto>> {
    return this.get<DepartmentWithSubDepartmentsDto>(`/${id}/with-sub-departments`);
  }

  getSubDepartments(id: string): Observable<ApiResponse<DepartmentBasicDto[]>> {
    return this.get<DepartmentBasicDto[]>(`/${id}/sub-departments`);
  }

  getRootDepartments(): Observable<ApiResponse<DepartmentDto[]>> {
    return this.get<DepartmentDto[]>('/root');
  }

  getActiveDepartments(): Observable<ApiResponse<DepartmentDto[]>> {
    return this.get<DepartmentDto[]>('/active');
  }

  assignManager(id: string, managerId: string | null): Observable<ApiResponse<DepartmentDto>> {
    return this.http.put<ApiResponse<DepartmentDto>>(
      `${this.getApiUrl()}/${id}/manager`,
      JSON.stringify(managerId),
      { headers: { 'Content-Type': 'application/json' } }
    );
  }
}
