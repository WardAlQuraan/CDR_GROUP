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
  imageUrl?: string;
  locationUrl?: string;
  latitude?: number;
  longitude?: number;
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
  imageUrl?: string;
  locationUrl?: string;
  latitude?: number;
  longitude?: number;
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
  imageUrl?: string;
  locationUrl?: string;
  latitude?: number;
  longitude?: number;
}
