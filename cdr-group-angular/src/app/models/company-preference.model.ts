export interface CompanyPreferenceDto {
  id: string;
  code: string;
  valueEn: string;
  valueAr: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyPreferenceDto {
  code: string;
  valueEn: string;
  valueAr: string;
  companyId: string;
}

export interface UpdateCompanyPreferenceDto {
  code?: string;
  valueEn?: string;
  valueAr?: string;
  companyId?: string;
}

export interface CompanyPreferencePagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
  code?: string;
}
