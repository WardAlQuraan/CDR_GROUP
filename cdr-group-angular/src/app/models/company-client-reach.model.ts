import { PagedRequest } from './paged.model';

export interface CompanyClientReachPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyClientReachDto {
  id: string;
  clientNameEn: string;
  clientNameAr: string;
  clientLogoUrl?: string;
  reach: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyClientReachDto {
  clientNameEn: string;
  clientNameAr: string;
  reach: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyClientReachDto {
  clientNameEn?: string;
  clientNameAr?: string;
  reach?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
