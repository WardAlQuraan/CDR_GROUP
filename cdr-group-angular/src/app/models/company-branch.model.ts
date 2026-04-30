import { PagedRequest } from './paged.model';

export interface CompanyBranchPagedRequest extends PagedRequest {
  companyId?: string;
  cityId?: string;
}

export interface CompanyBranchDto {
  id: string;
  nameEn: string;
  nameAr: string;
  nickNameEn?: string;
  nickNameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  openingDate: Date;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  cityId: string;
  cityNameEn: string;
  cityNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyBranchDto {
  nameEn: string;
  nameAr: string;
  nickNameEn?: string;
  nickNameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  openingDate: Date;
  companyId: string;
  cityId: string;
}

export interface UpdateCompanyBranchDto {
  nameEn?: string;
  nameAr?: string;
  nickNameEn?: string;
  nickNameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  openingDate?: Date;
  companyId?: string;
  cityId?: string;
}
