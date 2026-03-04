import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import { ContactUsDto, CreateContactUsDto, UpdateContactUsDto, ContactUsPagedRequest } from '../models/contact-us.model';

@Injectable({
  providedIn: 'root'
})
export class ContactUsService extends BaseService<ContactUsDto, CreateContactUsDto, UpdateContactUsDto> {
  constructor(http: HttpClient) {
    super(http, 'contactus');
  }

  getContactUsPaged(request?: ContactUsPagedRequest): Observable<ApiResponse<PagedResult<ContactUsDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<ContactUsDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
