import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import {
  CompanyPreContractStudyDto,
  CreateCompanyPreContractStudyDto,
  UpdateCompanyPreContractStudyDto,
  CompanyPreContractStudyPagedRequest
} from '../models/company-pre-contract-study.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyPreContractStudiesService extends BaseService<CompanyPreContractStudyDto, CreateCompanyPreContractStudyDto, UpdateCompanyPreContractStudyDto> {

  constructor(http: HttpClient) {
    super(http, 'CompanyPreContractStudies');
  }

  getCompanyPreContractStudiesPaged(request?: CompanyPreContractStudyPagedRequest): Observable<ApiResponse<PagedResult<CompanyPreContractStudyDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<CompanyPreContractStudyDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getByCompany(companyId: string): Observable<ApiResponse<CompanyPreContractStudyDto[]>> {
    return this.http.get<ApiResponse<CompanyPreContractStudyDto[]>>(`${this.getApiUrl()}/by-company/${companyId}`).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
