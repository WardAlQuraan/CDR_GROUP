import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { CacheService } from './cache.service';
import { ApiResponse } from '../models/api-response.model';
import {
  CompanyDto,
  CreateCompanyDto,
  UpdateCompanyDto
} from '../models/company.model';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService extends BaseService<CompanyDto, CreateCompanyDto, UpdateCompanyDto> {
  constructor(http: HttpClient, private cacheService: CacheService) {
    super(http, 'companies');
  }

  getActiveCompanies(): Observable<ApiResponse<CompanyDto[]>> {
    return this.cacheService.get('active-companies', () => this.get<CompanyDto[]>('/active'));
  }

  getTree(): Observable<ApiResponse<CompanyDto[]>> {
    return this.get<CompanyDto[]>('/tree');
  }
  getTreeByCompanyId(companyId: string): Observable<ApiResponse<CompanyDto[]>> {
    return this.get<CompanyDto[]>(`/tree?parentId=${companyId}`);
  }

  uploadLogo(id: string, file: File): Observable<ApiResponse<CompanyDto>> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<ApiResponse<CompanyDto>>(`${this.getApiUrl()}/${id}/logo`, formData);
  }
}
