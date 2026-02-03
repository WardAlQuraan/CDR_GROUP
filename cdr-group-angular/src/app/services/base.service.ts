import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { PagedRequest, PagedResult } from '../models/paged.model';

export abstract class BaseService<T, TCreate = T, TUpdate = Partial<T>> {
  protected readonly baseUrl = environment.apiUrl;

  constructor(
    protected http: HttpClient,
    protected endpoint: string
  ) {}

  protected getApiUrl(): string {
    return `${this.baseUrl}/${this.endpoint}`;
  }

  getAll(): Observable<ApiResponse<T[]>> {
    return this.http.get<ApiResponse<T[]>>(`${this.getApiUrl()}/all`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getPaged(request?: PagedRequest): Observable<ApiResponse<PagedResult<T>>> {
    const params = this.buildPagedParams(request);
    return this.http.get<ApiResponse<PagedResult<T>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getById(id: string): Observable<ApiResponse<T>> {
    return this.http.get<ApiResponse<T>>(`${this.getApiUrl()}/${id}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  create(dto: TCreate): Observable<ApiResponse<T>> {
    return this.http.post<ApiResponse<T>>(this.getApiUrl(), dto).pipe(
      catchError(error => this.handleError(error))
    );
  }

  update(id: string, dto: TUpdate): Observable<ApiResponse<T>> {
    return this.http.put<ApiResponse<T>>(`${this.getApiUrl()}/${id}`, dto).pipe(
      catchError(error => this.handleError(error))
    );
  }

  delete(id: string): Observable<ApiResponse<void>> {
    return this.http.delete<ApiResponse<void>>(`${this.getApiUrl()}/${id}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  protected post<TReq, TRes>(path: string, dto?: TReq): Observable<ApiResponse<TRes>> {
    return this.http.post<ApiResponse<TRes>>(`${this.getApiUrl()}${path}`, dto).pipe(
      catchError(error => this.handleError(error))
    );
  }

  protected get<TRes>(path: string): Observable<ApiResponse<TRes>> {
    return this.http.get<ApiResponse<TRes>>(`${this.getApiUrl()}${path}`).pipe(
      catchError(error => this.handleError(error))
    );
  }

  protected buildPagedParams(request?: PagedRequest): HttpParams {
    let params = new HttpParams();
    if (request) {
      if (request.pageNumber !== undefined) {
        params = params.set('pageNumber', request.pageNumber.toString());
      }
      if (request.pageSize !== undefined) {
        params = params.set('pageSize', request.pageSize.toString());
      }
      if (request.sortBy) {
        params = params.set('sortBy', request.sortBy);
      }
      if (request.sortDescending !== undefined) {
        params = params.set('sortDescending', request.sortDescending.toString());
      }
      if (request.search) {
        params = params.set('search', request.search);
      }
    }
    return params;
  }

  protected handleError(error: any): Observable<never> {
    let errorMessage = 'An error occurred';
    if (error.error?.message) {
      errorMessage = error.error.message;
    } else if (error.error?.errors?.length) {
      errorMessage = error.error.errors.join(', ');
    } else if (error.message) {
      errorMessage = error.message;
    }
    return throwError(() => new Error(errorMessage));
  }
}
