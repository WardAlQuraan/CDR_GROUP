import { PagedRequest } from './paged.model';

export interface CompanyPreContractStudyPagedRequest extends PagedRequest {
  companyId?: string;
}

export interface CompanyPreContractStudyDto {
  id: string;
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyPreContractStudyDto {
  descriptionEn?: string;
  descriptionAr?: string;
  companyId: string;
}

export interface UpdateCompanyPreContractStudyDto {
  descriptionEn?: string;
  descriptionAr?: string;
  companyId?: string;
}
