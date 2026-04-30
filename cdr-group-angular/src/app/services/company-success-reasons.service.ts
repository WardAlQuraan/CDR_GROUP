import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanySuccessReasonDto,
  CreateCompanySuccessReasonDto,
  UpdateCompanySuccessReasonDto,
  CompanySuccessReasonPagedRequest
} from '../models/company-success-reason.model';

@Injectable({
  providedIn: 'root'
})
export class CompanySuccessReasonsService extends BaseService<CompanySuccessReasonDto, CreateCompanySuccessReasonDto, UpdateCompanySuccessReasonDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanySuccessReasons');
  }

  getCompanySuccessReasonsPaged(request?: CompanySuccessReasonPagedRequest): Observable<ApiResponse<PagedResult<CompanySuccessReasonDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanySuccessReasonDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanySuccessReasonDto[]>> {
    return this.http.get<ApiResponse<CompanySuccessReasonDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
