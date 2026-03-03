import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { ComplaintDto, CreateComplaintDto, UpdateComplaintDto } from '../models/complaint.model';

@Injectable({
  providedIn: 'root'
})
export class ComplaintsService extends BaseService<ComplaintDto, CreateComplaintDto, UpdateComplaintDto> {
  constructor(http: HttpClient) {
    super(http, 'complaints');
  }
}
