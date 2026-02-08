import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';
import { ContactUsDto, CreateContactUsDto, UpdateContactUsDto } from '../models/contact-us.model';

@Injectable({
  providedIn: 'root'
})
export class ContactUsService extends BaseService<ContactUsDto, CreateContactUsDto, UpdateContactUsDto> {
  constructor(http: HttpClient) {
    super(http, 'contactus');
  }
}
