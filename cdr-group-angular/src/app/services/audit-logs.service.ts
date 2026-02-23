import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { PagedRequest, PagedResult } from '../models/paged.model';
import { AuditLogDto } from '../models/audit-log.model';

@Injectable({
  providedIn: 'root'
})
export class AuditLogsService {
  private readonly baseUrl = `${environment.apiUrl}/auditlogs`;

  constructor(private http: HttpClient) {}

  getPaged(entityName: string, entityId: string, request?: PagedRequest): Observable<ApiResponse<PagedResult<AuditLogDto>>> {
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
      if (request.searchTerm) {
        params = params.set('searchTerm', request.searchTerm);
      }
    }
    return this.http.get<ApiResponse<PagedResult<AuditLogDto>>>(`${this.baseUrl}/${entityName}/${entityId}`, { params });
  }
}
