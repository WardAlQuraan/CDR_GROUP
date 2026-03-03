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

export interface UpdateContactUsDto {
  fullName?: string;
  email?: string;
  message?: string;
}
