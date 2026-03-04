import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import { ComplaintDto, CreateComplaintDto, UpdateComplaintDto, ComplaintPagedRequest } from '../models/complaint.model';

@Injectable({
  providedIn: 'root'
})
export class ComplaintsService extends BaseService<ComplaintDto, CreateComplaintDto, UpdateComplaintDto> {
  constructor(http: HttpClient) {
    super(http, 'complaints');
  }

  getComplaintsPaged(request?: ComplaintPagedRequest): Observable<ApiResponse<PagedResult<ComplaintDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<ComplaintDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
