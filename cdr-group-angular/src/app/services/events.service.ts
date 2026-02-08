import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedRequest, PagedResult } from '../models/paged.model';
import {
  EventDto,
  CreateEventDto,
  UpdateEventDto
} from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService extends BaseService<EventDto, CreateEventDto, UpdateEventDto> {
  constructor(http: HttpClient) {
    super(http, 'events');
  }

  getPagedByCompany(companyCode: string, request?: PagedRequest): Observable<ApiResponse<PagedResult<EventDto>>> {
    let params = this.buildPagedParams(request);
    params = params.set('companyCode', companyCode);
    return this.http.get<ApiResponse<PagedResult<EventDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
