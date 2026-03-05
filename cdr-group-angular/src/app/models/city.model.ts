export interface CityDto {
  id: string;
  nameEn: string;
  nameAr: string;
  latitude: number;
  longitude: number;
  countryId: string;
  countryNameEn?: string;
  countryNameAr?: string;
  createdAt: Date;
}

export interface CreateCityDto {
  nameEn: string;
  nameAr: string;
  latitude: number;
  longitude: number;
  countryId: string;
}

export interface UpdateCityDto {
  nameEn?: string;
  nameAr?: string;
  latitude?: number;
  longitude?: number;
  countryId?: string;
}

export interface CityPagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  countryId?: string;
}
