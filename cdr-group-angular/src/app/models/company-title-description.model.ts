import { PagedRequest } from './paged.model';

export interface CompanyTitleDescriptionPagedRequest extends PagedRequest {
  companyId?: string;
  code?: string;
}

export interface CompanyTitleDescriptionDto {
  id: string;
  code: string;
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyTitleDescriptionDto {
  code: string;
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyTitleDescriptionDto {
  code?: string;
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
