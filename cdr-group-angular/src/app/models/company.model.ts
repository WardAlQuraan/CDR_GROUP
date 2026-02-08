export interface CompanyDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive: boolean;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CompanyBasicDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
}

export interface CreateCompanyDto {
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive?: boolean;
}

export interface UpdateCompanyDto {
  code?: string;
  nameEn?: string;
  nameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive?: boolean;
}
