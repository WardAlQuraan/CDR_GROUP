export interface PartnerDto {
  id: string;
  status: string;
  companyId: string;
  companyNameEn?: string;
  companyNameAr?: string;
  cityId: string;
  cityNameEn?: string;
  cityNameAr?: string;
  cityLatitude?: number;
  cityLongitude?: number;
  countryNameEn?: string;
  countryNameAr?: string;
  createdAt: Date;
}

export interface CreatePartnerDto {
  companyId: string;
  cityId: string;
  status: string;
}

export interface UpdatePartnerDto {
  companyId?: string;
  cityId?: string;
  status?: string;
}

export interface PartnerPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
  cityId?: string;
  countryId?: string;
  status?: string;
}
