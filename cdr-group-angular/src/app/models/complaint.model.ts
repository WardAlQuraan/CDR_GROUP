export interface ComplaintDto {
  id: string;
  fullName: string;
  email: string;
  subject: string;
  message: string;
  companyId: string;
  companyNameEn?: string;
  companyNameAr?: string;
  createdAt: Date;
}

export interface CreateComplaintDto {
  fullName: string;
  email: string;
  subject: string;
  message: string;
  companyId: string;
}

export interface UpdateComplaintDto {
  fullName?: string;
  email?: string;
  subject?: string;
  message?: string;
  companyId?: string;
}

export interface ComplaintPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
}
