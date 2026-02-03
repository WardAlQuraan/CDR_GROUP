import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  PositionDto,
  PositionBasicDto,
  PositionWithEmployeesDto,
  CreatePositionDto,
  UpdatePositionDto
} from '../models/position.model';

@Injectable({
  providedIn: 'root'
})
export class PositionsService extends BaseService<PositionDto, CreatePositionDto, UpdatePositionDto> {
  constructor(http: HttpClient) {
    super(http, 'positions');
  }

  getByCode(code: string): Observable<ApiResponse<PositionDto>> {
    return this.get<PositionDto>(`/by-code/${code}`);
  }

  getByName(name: string): Observable<ApiResponse<PositionDto>> {
    return this.get<PositionDto>(`/by-name/${name}`);
  }

  getByDepartmentId(departmentId: string): Observable<ApiResponse<PositionDto[]>> {
    return this.get<PositionDto[]>(`/by-department/${departmentId}`);
  }

  getActivePositions(): Observable<ApiResponse<PositionDto[]>> {
    return this.get<PositionDto[]>('/active');
  }

  getWithEmployeeCount(id: string): Observable<ApiResponse<PositionWithEmployeesDto>> {
    return this.get<PositionWithEmployeesDto>(`/${id}/with-employee-count`);
  }

  assignDepartment(id: string, departmentId: string | null): Observable<ApiResponse<PositionDto>> {
    return this.http.put<ApiResponse<PositionDto>>(
      `${this.getApiUrl()}/${id}/department`,
      JSON.stringify(departmentId),
      { headers: { 'Content-Type': 'application/json' } }
    );
  }
}
