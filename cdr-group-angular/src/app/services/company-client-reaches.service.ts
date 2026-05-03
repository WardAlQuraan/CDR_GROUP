import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyClientReachDto,
  CreateCompanyClientReachDto,
  UpdateCompanyClientReachDto,
  CompanyClientReachPagedRequest
} from '../models/company-client-reach.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyClientReachesService extends BaseService<CompanyClientReachDto, CreateCompanyClientReachDto, UpdateCompanyClientReachDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyClientReaches');
  }

  getCompanyClientReachesPaged(request?: CompanyClientReachPagedRequest): Observable<ApiResponse<PagedResult<CompanyClientReachDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyClientReachDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyClientReachDto[]>> {
    return this.http.get<ApiResponse<CompanyClientReachDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  uploadLogo(id: string, file: File): Observable<ApiResponse<string>> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<ApiResponse<string>>(`${this.getApiUrl()}/${id}/logo`, formData).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
