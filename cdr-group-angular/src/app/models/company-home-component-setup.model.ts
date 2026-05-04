import { PagedRequest } from './paged.model';

export interface CompanyHomeComponentSetupPagedRequest extends PagedRequest {
  companyId?: string;
  componentCode?: string;
}

export interface CompanyHomeComponentSetupDto {
  id: string;
  componentCode: string;
  companyTitleDescriptionCode?: string;
  preferenceTitleCode?: string;
  preferenceDescriptionCode?: string;
  rank: number;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyHomeComponentSetupDto {
  componentCode: string;
  companyTitleDescriptionCode?: string;
  preferenceTitleCode?: string;
  preferenceDescriptionCode?: string;
  rank: number;
  companyId: string;
}

export interface UpdateCompanyHomeComponentSetupDto {
  componentCode?: string;
  companyTitleDescriptionCode?: string;
  preferenceTitleCode?: string;
  preferenceDescriptionCode?: string;
  rank?: number;
  companyId?: string;
}
