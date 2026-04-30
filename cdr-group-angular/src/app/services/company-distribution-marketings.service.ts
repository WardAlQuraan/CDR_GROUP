import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyDistributionMarketingDto,
  CreateCompanyDistributionMarketingDto,
  UpdateCompanyDistributionMarketingDto,
  CompanyDistributionMarketingPagedRequest
} from '../models/company-distribution-marketing.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyDistributionMarketingsService extends BaseService<CompanyDistributionMarketingDto, CreateCompanyDistributionMarketingDto, UpdateCompanyDistributionMarketingDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyDistributionMarketings');
  }

  getCompanyDistributionMarketingsPaged(request?: CompanyDistributionMarketingPagedRequest): Observable<ApiResponse<PagedResult<CompanyDistributionMarketingDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyDistributionMarketingDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyDistributionMarketingDto[]>> {
    return this.http.get<ApiResponse<CompanyDistributionMarketingDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
