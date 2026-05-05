import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { CacheService } from './cache.service';
import {
  CompanyHomeComponentSetupDto,
  CompanyHomeComponentSetupPagedRequest,
  CompanyHomeComponentSetupRankUpdate,
  CreateCompanyHomeComponentSetupDto,
  UpdateCompanyHomeComponentSetupDto,
} from '../models/company-home-component-setup.model';
import { HttpClient } from '@angular/common/http';
import { PagedResult } from '../models/paged.model';
import { ApiResponse } from '../models/api-response.model';
import { catchError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CompanyHomeComponentSetupsService extends BaseService<
  CompanyHomeComponentSetupDto,
  CreateCompanyHomeComponentSetupDto,
  UpdateCompanyHomeComponentSetupDto
> {
  constructor(http: HttpClient, private cacheService: CacheService) {
    super(http, 'CompanyHomeComponentSetups');
  }

  getCompanyHomeComponentSetups(
    request?: CompanyHomeComponentSetupPagedRequest
  ): Observable<ApiResponse<PagedResult<CompanyHomeComponentSetupDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    if (request?.componentCode) {
      params = params.set('componentCode', request.componentCode);
    }
    return this.http
      .get<ApiResponse<PagedResult<CompanyHomeComponentSetupDto>>>(this.getApiUrl(), { params })
      .pipe(catchError(error => this.handleError(error)));
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyHomeComponentSetupDto[]>> {
    return this.cacheService.get(
      `home-component-setups-${companyId}`,
      () => this.http
        .get<ApiResponse<CompanyHomeComponentSetupDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`)
        .pipe(catchError(error => this.handleError(error)))
    );
  }

  reorder(items: CompanyHomeComponentSetupRankUpdate[]): Observable<ApiResponse<void>> {
    return this.http
      .put<ApiResponse<void>>(`${this.getApiUrl()}/reorder`, items)
      .pipe(catchError(error => this.handleError(error)));
  }

  getByCompanyAndComponent(
    companyId: string,
    componentCode: string
  ): Observable<ApiResponse<CompanyHomeComponentSetupDto>> {
    return this.http
      .get<ApiResponse<CompanyHomeComponentSetupDto>>(
        `${this.getApiUrl()}/by-company/${companyId}/component/${encodeURIComponent(componentCode)}`
      )
      .pipe(catchError(error => this.handleError(error)));
  }
}
