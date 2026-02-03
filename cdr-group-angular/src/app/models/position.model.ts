export interface PositionDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  minSalary?: number;
  maxSalary?: number;
  isActive: boolean;
  departmentId?: string;
  departmentNameEn?: string;
  departmentNameAr?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface PositionBasicDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
}

export interface PositionWithEmployeesDto extends PositionDto {
  employeeCount: number;
}

export interface CreatePositionDto {
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  minSalary?: number;
  maxSalary?: number;
  isActive?: boolean;
  departmentId?: string;
}

export interface UpdatePositionDto {
  code?: string;
  nameEn?: string;
  nameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  minSalary?: number;
  maxSalary?: number;
  isActive?: boolean;
  departmentId?: string;
}
