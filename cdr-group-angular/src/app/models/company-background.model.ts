export interface CompanyBackgroundDto {
  id: string;
  imageUrl: string;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateCompanyBackgroundDto {
  imageUrl: string;
  companyId: string;
}

export interface UpdateCompanyBackgroundDto {
  imageUrl?: string;
  companyId?: string;
}

export interface CompanyBackgroundPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
}
