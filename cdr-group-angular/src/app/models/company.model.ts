export interface CompanyDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  storyEn?: string;
  storyAr?: string;
  missionEn?: string;
  missionAr?: string;
  visionEn?: string;
  visionAr?: string;
  titleEn?: string;
  titleAr?: string;
  primaryColor?: string;
  secondaryColor?: string;
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
  storyEn?: string;
  storyAr?: string;
  missionEn?: string;
  missionAr?: string;
  visionEn?: string;
  visionAr?: string;
  titleEn?: string;
  titleAr?: string;
  primaryColor?: string;
  secondaryColor?: string;
  isActive?: boolean;
}

export interface UpdateCompanyDto {
  code?: string;
  nameEn?: string;
  nameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  storyEn?: string;
  storyAr?: string;
  missionEn?: string;
  missionAr?: string;
  visionEn?: string;
  visionAr?: string;
  titleEn?: string;
  titleAr?: string;
  primaryColor?: string;
  secondaryColor?: string;
  isActive?: boolean;
}
