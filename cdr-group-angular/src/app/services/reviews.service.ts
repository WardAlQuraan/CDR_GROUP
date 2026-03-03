import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ReviewDto, CreateReviewDto, UpdateReviewDto, ReviewPagedRequest } from '../models/review.model';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewsService extends BaseService<ReviewDto, CreateReviewDto, UpdateReviewDto> {
  constructor(http: HttpClient) {
    super(http, 'reviews');
  }

  getReviewsPaged(request?: ReviewPagedRequest): Observable<ApiResponse<PagedResult<ReviewDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.isVisible !== undefined && request.isVisible !== null) {
      params = params.set('isVisible', request.isVisible.toString());
    }
    if (request?.companyId) {
      params = params.set('companyId', request.companyId);
    }
    return this.http.get<ApiResponse<PagedResult<ReviewDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
