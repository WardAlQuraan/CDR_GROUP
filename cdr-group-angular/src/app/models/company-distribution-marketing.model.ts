import { PagedRequest } from './paged.model';

export interface CompanyDistributionMarketingPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyDistributionMarketingDto {
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

export interface CreateCompanyDistributionMarketingDto {
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyDistributionMarketingDto {
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
