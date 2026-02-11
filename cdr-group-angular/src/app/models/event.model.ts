export interface EventDto {
  id: string;
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  eventUrl?: string;
  eventDate?: Date;
  companyId?: string;
  companyNameEn?: string;
  companyNameAr?: string;
  primaryFileUrl?: string;

  createdAt: Date;
  updatedAt?: Date;
}

export interface EventBasicDto {
  id: string;
  titleEn: string;
  titleAr: string;
}

export interface CreateEventDto {
  titleEn: string;
  titleAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  eventUrl?: string;
  eventDate?: Date;
  companyId?: string;
}

export interface UpdateEventDto {
  titleEn?: string;
  titleAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  eventUrl?: string;
  eventDate?: Date;
  companyId?: string;
}
