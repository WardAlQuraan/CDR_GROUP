import { PagedRequest } from './paged.model';

export interface CompanyFinancialClausesRightsPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyFinancialClausesRightsDto {
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

export interface CreateCompanyFinancialClausesRightsDto {
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyFinancialClausesRightsDto {
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
