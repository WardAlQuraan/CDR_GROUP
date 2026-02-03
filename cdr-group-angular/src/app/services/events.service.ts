import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { ApiResponse } from '../models/api-response.model';
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

  getByDepartmentId(departmentId: string): Observable<ApiResponse<EventDto[]>> {
    return this.get<EventDto[]>(`/by-department/${departmentId}`);
  }
}
