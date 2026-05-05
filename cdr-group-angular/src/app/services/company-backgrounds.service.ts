import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { CacheService } from './cache.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyBackgroundDto,
  CreateCompanyBackgroundDto,
  UpdateCompanyBackgroundDto,
  CompanyBackgroundPagedRequest
} from '../models/company-background.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyBackgroundsService extends BaseService<CompanyBackgroundDto, CreateCompanyBackgroundDto, UpdateCompanyBackgroundDto> {

  constructor(http: HttpClient, private cacheService: CacheService) {
    super(http, 'CompanyBackgrounds');
  }

  getCompanyBackgroundsPaged(request?: CompanyBackgroundPagedRequest): Observable<ApiResponse<PagedResult<CompanyBackgroundDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyBackgroundDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyBackgroundDto[]>> {
    return this.cacheService.get(
      `company-backgrounds-${companyId}`,
      () => this.http.get<ApiResponse<CompanyBackgroundDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
        catchError(error => this.handleError(error))
      )
    );
  }
}
