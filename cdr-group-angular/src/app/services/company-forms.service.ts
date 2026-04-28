import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyFormDto,
  CreateCompanyFormDto,
  UpdateCompanyFormDto,
  CompanyFormPagedRequest
} from '../models/company-form.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyFormsService extends BaseService<CompanyFormDto, CreateCompanyFormDto, UpdateCompanyFormDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyForms');
  }

  getCompanyFormsPaged(request?: CompanyFormPagedRequest): Observable<ApiResponse<PagedResult<CompanyFormDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyFormDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyFormDto[]>> {
    return this.http.get<ApiResponse<CompanyFormDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
