import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyDistinguishDto,
  CreateCompanyDistinguishDto,
  UpdateCompanyDistinguishDto,
  CompanyDistinguishPagedRequest
} from '../models/company-distinguish.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyDistinguishesService extends BaseService<CompanyDistinguishDto, CreateCompanyDistinguishDto, UpdateCompanyDistinguishDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyDistinguishes');
  }

  getCompanyDistinguishesPaged(request?: CompanyDistinguishPagedRequest): Observable<ApiResponse<PagedResult<CompanyDistinguishDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyDistinguishDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyDistinguishDto[]>> {
    return this.http.get<ApiResponse<CompanyDistinguishDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
