export interface CompanyFormDto {
  id: string;
  formUrl: string;
  formNameEn: string;
  formNameAr: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyFormDto {
  formUrl: string;
  formNameEn: string;
  formNameAr: string;
  companyId: string;
}

export interface UpdateCompanyFormDto {
  formUrl?: string;
  formNameEn?: string;
  formNameAr?: string;
  companyId?: string;
}

export interface CompanyFormPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
}
