import { PagedRequest } from './paged.model';

export interface CompanyGeographicExpansionPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyGeographicExpansionDto {
  id: string;
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyGeographicExpansionDto {
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyGeographicExpansionDto {
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
