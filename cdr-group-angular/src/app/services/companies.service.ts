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

  getByCode(code: string): Observable<ApiResponse<CompanyDto>> {
    return this.get<CompanyDto>(`/by-code/${code}`);
  }

  getActiveCompanies(): Observable<ApiResponse<CompanyDto[]>> {
    return this.cacheService.get('active-companies', () => this.get<CompanyDto[]>('/active'));
  }
}
