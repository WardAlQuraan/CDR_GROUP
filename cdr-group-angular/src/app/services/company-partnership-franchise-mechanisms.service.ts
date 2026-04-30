import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyPartnershipFranchiseMechanismDto,
  CreateCompanyPartnershipFranchiseMechanismDto,
  UpdateCompanyPartnershipFranchiseMechanismDto,
  CompanyPartnershipFranchiseMechanismPagedRequest
} from '../models/company-partnership-franchise-mechanism.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyPartnershipFranchiseMechanismsService extends BaseService<CompanyPartnershipFranchiseMechanismDto, CreateCompanyPartnershipFranchiseMechanismDto, UpdateCompanyPartnershipFranchiseMechanismDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyPartnershipFranchiseMechanisms');
  }

  getCompanyPartnershipFranchiseMechanismsPaged(request?: CompanyPartnershipFranchiseMechanismPagedRequest): Observable<ApiResponse<PagedResult<CompanyPartnershipFranchiseMechanismDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyPartnershipFranchiseMechanismDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyPartnershipFranchiseMechanismDto[]>> {
    return this.http.get<ApiResponse<CompanyPartnershipFranchiseMechanismDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
