import { PagedRequest } from './paged.model';

export interface CompanyContactPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyContactDto {
  id: string;
  icon: string;
  name: string;
  value: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyContactDto {
  icon: string;
  name: string;
  value: string;
  companyId: string;
}

export interface UpdateCompanyContactDto {
  icon?: string;
  name?: string;
  value?: string;
  companyId?: string;
}
