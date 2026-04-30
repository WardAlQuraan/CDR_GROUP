import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyGeographicExpansionDto,
  CreateCompanyGeographicExpansionDto,
  UpdateCompanyGeographicExpansionDto,
  CompanyGeographicExpansionPagedRequest
} from '../models/company-geographic-expansion.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyGeographicExpansionsService extends BaseService<CompanyGeographicExpansionDto, CreateCompanyGeographicExpansionDto, UpdateCompanyGeographicExpansionDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyGeographicExpansions');
  }

  getCompanyGeographicExpansionsPaged(request?: CompanyGeographicExpansionPagedRequest): Observable<ApiResponse<PagedResult<CompanyGeographicExpansionDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyGeographicExpansionDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyGeographicExpansionDto[]>> {
    return this.http.get<ApiResponse<CompanyGeographicExpansionDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
