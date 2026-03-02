import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyContactDto,
  CompanyContactPagedRequest,
  CreateCompanyContactDto,
  UpdateCompanyContactDto
} from '../models/company-contact.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyContactsService extends BaseService<CompanyContactDto, CreateCompanyContactDto, UpdateCompanyContactDto> {
  constructor(http: HttpClient) {
    super(http, 'CompanyContacts');
  }

  getPagedWithCompany(request?: CompanyContactPagedRequest): Observable<ApiResponse<PagedResult<CompanyContactDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyContactDto>>>(this.getApiUrl(), { params });
  }

  getByCompanyId(companyId: string): Observable<ApiResponse<CompanyContactDto[]>> {
    return this.get<CompanyContactDto[]>(`/by-company/${companyId}`);
  }
}
