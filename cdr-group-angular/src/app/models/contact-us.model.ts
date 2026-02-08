export interface ContactUsDto {
  id: string;
  fullName: string;
  email: string;
  message: string;
  createdAt: Date;
}

export interface CreateContactUsDto {
  fullName: string;
  email: string;
  message: string;
}

export interface UpdateContactUsDto {
  fullName?: string;
  email?: string;
  message?: string;
}
