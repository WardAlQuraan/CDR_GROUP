import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { CacheService } from './cache.service';
import { ApiResponse } from '../models/api-response.model';
import { PagedResult } from '../models/paged.model';
import { CityDto, CreateCityDto, UpdateCityDto, CityPagedRequest } from '../models/city.model';

@Injectable({
  providedIn: 'root'
})
export class CitiesService extends BaseService<CityDto, CreateCityDto, UpdateCityDto> {

  constructor(http: HttpClient, private cacheService: CacheService) {
    super(http, 'cities');
  }

  getAllCached(): Observable<ApiResponse<CityDto[]>> {
    return this.cacheService.get('all-cities', () => this.getAll());
  }

  getCitiesPaged(request?: CityPagedRequest): Observable<ApiResponse<PagedResult<CityDto>>> {
    let params = this.buildPagedParams(request);
    if (request?.countryId) {
      params = params.set('countryId', request.countryId);
    }
    return this.http.get<ApiResponse<PagedResult<CityDto>>>(this.getApiUrl(), { params }).pipe(
      catchError(error => this.handleError(error))
    );
  }
}
