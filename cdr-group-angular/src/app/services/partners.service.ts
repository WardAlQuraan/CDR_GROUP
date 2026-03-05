import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import { PartnerDto, CreatePartnerDto, UpdatePartnerDto, PartnerPagedRequest } from '../models/partner.model';

@Injectable({
  providedIn: 'root'
})
export class PartnersService extends BaseService<PartnerDto, CreatePartnerDto, UpdatePartnerDto> {

  constructor(http: HttpClient) {
    super(http, 'partners');
  }

  getPartnersPaged(request?: PartnerPagedRequest): Observable<ApiResponse<PagedResult<PartnerDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    if (request?.cityId) {
      params = params.set('cityId', request.cityId);
    }
    if (request?.countryId) {
      params = params.set('countryId', request.countryId);
    }
    if (request?.status) {
      params = params.set('status', request.status);
    }
    return this.http.get<ApiResponse<PagedResult<PartnerDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
