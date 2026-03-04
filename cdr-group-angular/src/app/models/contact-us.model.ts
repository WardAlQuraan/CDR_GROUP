export interface ContactUsDto {
  id: string;
  fullName: string;
  email: string;
  message: string;
  companyId?: string;
  companyNameEn?: string;
  companyNameAr?: string;
  createdAt: Date;
}

export interface CreateContactUsDto {
  fullName: string;
  email: string;
  message: string;
  companyId?: string;
}

export interface ContactUsPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
}

export interface UpdateContactUsDto {
  fullName?: string;
  email?: string;
  message?: string;
}
