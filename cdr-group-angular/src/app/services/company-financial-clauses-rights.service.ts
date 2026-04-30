import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { CompanyFinancialClausesRightsDto, CompanyFinancialClausesRightsPagedRequest, CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto } from '../models/company-financial-clauses-rights.model';
import { HttpClient } from '@angular/common/http';
import { PagedResult } from '../models/paged.model';
import { ApiResponse } from '../models/api-response.model';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CompanyFinancialClausesRightsService extends BaseService<CompanyFinancialClausesRightsDto,CreateCompanyFinancialClausesRightsDto, UpdateCompanyFinancialClausesRightsDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyFinancialClausesRights');
  }
  
  getCompanyFinancialClausesRights(request?: CompanyFinancialClausesRightsPagedRequest): Observable<ApiResponse<PagedResult<CompanyFinancialClausesRightsDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyFinancialClausesRightsDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyFinancialClausesRightsDto[]>> {
    return this.http.get<ApiResponse<CompanyFinancialClausesRightsDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}