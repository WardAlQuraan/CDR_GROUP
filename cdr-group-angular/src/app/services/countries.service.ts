import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { CacheService } from './cache.service';
import { ApiResponse } from '../models/api-response.model';
import { CountryDto, CreateCountryDto, UpdateCountryDto } from '../models/country.model';

@Injectable({
  providedIn: 'root'
})
export class CountriesService extends BaseService<CountryDto, CreateCountryDto, UpdateCountryDto> {

  constructor(http: HttpClient, private cacheService: CacheService) {
    super(http, 'countries');
  }

  getAllCached(): Observable<ApiResponse<CountryDto[]>> {
    return this.cacheService.get('all-countries', () => this.getAll());
  }
}
