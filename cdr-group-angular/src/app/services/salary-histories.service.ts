import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { SalaryHistoryDto, CreateSalaryHistoryDto, UpdateSalaryHistoryDto } from '../models/salary-history.model';

@Injectable({
  providedIn: 'root'
})
export class SalaryHistoriesService extends BaseService<SalaryHistoryDto, CreateSalaryHistoryDto, UpdateSalaryHistoryDto> {

  constructor(http: HttpClient) {
    super(http, 'SalaryHistories');
  }

  getByEmployee(employeeId: string): Observable<ApiResponse<SalaryHistoryDto[]>> {
    return this.get<SalaryHistoryDto[]>(`/by-employee/${employeeId}`);
  }
}
