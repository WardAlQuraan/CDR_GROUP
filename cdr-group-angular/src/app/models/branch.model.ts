export interface BranchDto {
  id: string;
  code: string;
  address?: string;
  latitude?: number;
  longitude?: number;
  isPrimary: boolean;
  isActive: boolean;
  companyId: string;
  companyNameEn: string;
  companyNameAr: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreateBranchDto {
  code: string;
  address?: string;
  latitude?: number;
  longitude?: number;
  isPrimary: boolean;
  isActive: boolean;
  companyId: string;
}

export interface UpdateBranchDto {
  code?: string;
  address?: string;
  latitude?: number;
  longitude?: number;
  isPrimary?: boolean;
  isActive?: boolean;
  companyId?: string;
}
