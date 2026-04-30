import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyBranchDto,
  CompanyBranchPagedRequest,
  CreateCompanyBranchDto,
  UpdateCompanyBranchDto
} from '../models/company-branch.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyBranchesService extends BaseService<CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto> {
  constructor(http: HttpClient) {
    super(http, 'CompanyBranches');
  }

  getPagedWithFilters(request?: CompanyBranchPagedRequest): Observable<ApiResponse<PagedResult<CompanyBranchDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    if (request?.cityId) {
      params = params.set('cityId', request.cityId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyBranchDto>>>(this.getApiUrl(), { params });
  }

  getByCompanyId(companyId: string): Observable<ApiResponse<CompanyBranchDto[]>> {
    return this.get<CompanyBranchDto[]>(`/by-company/${companyId}`);
  }
}
