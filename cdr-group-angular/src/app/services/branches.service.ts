import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import {
  BranchDto,
  CreateBranchDto,
  UpdateBranchDto
} from '../models/branch.model';

@Injectable({
  providedIn: 'root'
})
export class BranchesService extends BaseService<BranchDto, CreateBranchDto, UpdateBranchDto> {

  constructor(http: HttpClient) {
    super(http, 'branches');
  }

  getByCode(code: string): Observable<ApiResponse<BranchDto>> {
    return this.get<BranchDto>(`/by-code/${code}`);
  }

  getByCompanyId(companyId: string): Observable<ApiResponse<BranchDto[]>> {
    return this.get<BranchDto[]>(`/by-company/${companyId}`);
  }

  getActive(): Observable<ApiResponse<BranchDto[]>> {
    return this.get<BranchDto[]>('/active');
  }
}
