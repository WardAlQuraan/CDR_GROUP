import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  EmployeeDto,
  EmployeeBasicDto,
  EmployeeWithSubordinatesDto,
  EmployeeTreeNodeDto,
  CreateEmployeeDto,
  UpdateEmployeeDto
} from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService extends BaseService<EmployeeDto, CreateEmployeeDto, UpdateEmployeeDto> {

  constructor(http: HttpClient) {
    super(http, 'employees');
  }

  getWithSubordinates(id: string): Observable<ApiResponse<EmployeeWithSubordinatesDto>> {
    return this.get<EmployeeWithSubordinatesDto>(`/${id}/with-subordinates`);
  }

  getByEmployeeCode(employeeCode: string): Observable<ApiResponse<EmployeeDto>> {
    return this.get<EmployeeDto>(`/by-code/${encodeURIComponent(employeeCode)}`);
  }

  getByUserId(userId: string): Observable<ApiResponse<EmployeeDto>> {
    return this.get<EmployeeDto>(`/by-user/${userId}`);
  }

  getSubordinates(id: string): Observable<ApiResponse<EmployeeBasicDto[]>> {
    return this.get<EmployeeBasicDto[]>(`/${id}/subordinates`);
  }

  getByDepartmentId(departmentId: string): Observable<ApiResponse<EmployeeDto[]>> {
    return this.get<EmployeeDto[]>(`/by-department/${departmentId}`);
  }

  assignManager(id: string, managerId: string | null): Observable<ApiResponse<EmployeeDto>> {
    return this.http.put<ApiResponse<EmployeeDto>>(`${this.getApiUrl()}/${id}/manager`, JSON.stringify(managerId), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  linkToUser(id: string, userId: string | null): Observable<ApiResponse<EmployeeDto>> {
    return this.http.put<ApiResponse<EmployeeDto>>(`${this.getApiUrl()}/${id}/user`, JSON.stringify(userId), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  assignDepartment(id: string, departmentId: string | null): Observable<ApiResponse<EmployeeDto>> {
    return this.http.put<ApiResponse<EmployeeDto>>(`${this.getApiUrl()}/${id}/department`, JSON.stringify(departmentId), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  getTree(departmentId?: string): Observable<ApiResponse<EmployeeTreeNodeDto[]>> {
    const params = departmentId ? `?departmentId=${departmentId}` : '';
    return this.get<EmployeeTreeNodeDto[]>(`/tree${params}`);
  }
}
