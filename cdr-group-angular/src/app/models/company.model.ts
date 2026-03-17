export interface CompanyDto {
  id: string;
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
  openingStartDay: string;
  openingEndDay: string;
  openingStartTime: string;
  openingEndTime: string;
  openingHoursNoteEn?: string;
  openingHoursNoteAr?: string;
  parentId?: string;
  parentNameEn?: string;
  parentNameAr?: string;
  isActive: boolean;
  numberOfEmployees?: number;
  children: CompanyDto[];
  partnershipFormUrl?: string;
  partnersCount: number;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CompanyBasicDto {
  id: string;
  nameEn: string;
  nameAr: string;
}

export interface CreateCompanyDto {
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
  openingStartDay?: string;
  openingEndDay?: string;
  openingStartTime?: string;
  openingEndTime?: string;
  openingHoursNoteEn?: string;
  openingHoursNoteAr?: string;
  numberOfEmployees?: number;
  partnershipFormUrl?: string;
  parentId?: string;
  isActive?: boolean;
}

export interface UpdateCompanyDto {
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
  openingStartDay?: string;
  openingEndDay?: string;
  openingStartTime?: string;
  openingEndTime?: string;
  openingHoursNoteEn?: string;
  openingHoursNoteAr?: string;
  numberOfEmployees?: number;
  partnershipFormUrl?: string;
  parentId?: string;
  isActive?: boolean;
}
