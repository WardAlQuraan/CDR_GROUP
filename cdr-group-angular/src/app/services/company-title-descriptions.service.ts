import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyTitleDescriptionDto,
  CreateCompanyTitleDescriptionDto,
  UpdateCompanyTitleDescriptionDto,
  CompanyTitleDescriptionPagedRequest
} from '../models/company-title-description.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyTitleDescriptionsService extends BaseService<CompanyTitleDescriptionDto, CreateCompanyTitleDescriptionDto, UpdateCompanyTitleDescriptionDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyTitleDescriptions');
  }

  getCompanyTitleDescriptionsPaged(request?: CompanyTitleDescriptionPagedRequest): Observable<ApiResponse<PagedResult<CompanyTitleDescriptionDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    if (request?.code) {
      params = params.set('code', request.code);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyTitleDescriptionDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyTitleDescriptionDto[]>> {
    return this.http.get<ApiResponse<CompanyTitleDescriptionDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompanyAndCode(companyId: string, code: string): Observable<ApiResponse<CompanyTitleDescriptionDto[]>> {
    return this.http.get<ApiResponse<CompanyTitleDescriptionDto[]>>(`${this.getApiUrl()}/by-company/${companyId}/code/${encodeURIComponent(code)}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
