export interface CountryDto {
  id: string;
  nameEn: string;
  nameAr: string;
  latitude: number;
  longitude: number;
  createdAt: Date;
}

export interface CreateCountryDto {
  nameEn: string;
  nameAr: string;
  latitude: number;
  longitude: number;
}

export interface UpdateCountryDto {
  nameEn?: string;
  nameAr?: string;
  latitude?: number;
  longitude?: number;
}
