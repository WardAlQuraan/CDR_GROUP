import { PagedRequest } from './paged.model';

export interface CompanyPartnershipFranchiseMechanismPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyPartnershipFranchiseMechanismDto {
  id: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyPartnershipFranchiseMechanismDto {
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyPartnershipFranchiseMechanismDto {
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
