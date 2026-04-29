import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyPreferenceDto,
  CreateCompanyPreferenceDto,
  UpdateCompanyPreferenceDto,
  CompanyPreferencePagedRequest
} from '../models/company-preference.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyPreferencesService extends BaseService<CompanyPreferenceDto, CreateCompanyPreferenceDto, UpdateCompanyPreferenceDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyPreferences');
  }

  getCompanyPreferencesPaged(request?: CompanyPreferencePagedRequest): Observable<ApiResponse<PagedResult<CompanyPreferenceDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    if (request?.code) {
      params = params.set('code', request.code);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyPreferenceDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyPreferenceDto[]>> {
    return this.http.get<ApiResponse<CompanyPreferenceDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompanyAndCode(companyId: string, code: string): Observable<ApiResponse<CompanyPreferenceDto>> {
    return this.http.get<ApiResponse<CompanyPreferenceDto>>(`${this.getApiUrl()}/by-company/${companyId}/code/${encodeURIComponent(code)}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
