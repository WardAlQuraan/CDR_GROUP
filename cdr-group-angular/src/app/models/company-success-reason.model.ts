import { PagedRequest } from './paged.model';

export interface CompanySuccessReasonPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanySuccessReasonDto {
  id: string;
  reasonEn: string;
  reasonAr: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanySuccessReasonDto {
  reasonEn: string;
  reasonAr: string;
  companyId: string;
}

export interface UpdateCompanySuccessReasonDto {
  reasonEn?: string;
  reasonAr?: string;
  companyId?: string;
}
