export interface EmployeeDto {
  id: string;
  firstNameEn: string;
  lastNameEn: string;
  firstNameAr: string;
  lastNameAr: string;
  fullNameEn: string;
  fullNameAr: string;
  email?: string;
  phone?: string;
  hireDate?: Date;
  salary?: number;
  isActive: boolean;

  companyId?: string;
  companyName?: string;
  companyNameAr?: string;

  positionId?: string;
  positionName?: string;

  managerId?: string;
  manager?: EmployeeBasicDto;

  userId?: string;
  username?: string;

  createdAt: Date;
  updatedAt?: Date;
}

export interface EmployeeBasicDto {
  id: string;
  firstNameEn: string;
  lastNameEn: string;
  firstNameAr: string;
  lastNameAr: string;
  fullNameEn: string;
  fullNameAr: string;
  positionId?: string;
  positionName?: string;
  companyId?: string;
  companyName?: string;
}

export interface EmployeeWithSubordinatesDto extends EmployeeDto {
  subordinates: EmployeeBasicDto[];
}

export interface CreateEmployeeDto {
  firstNameEn: string;
  lastNameEn: string;
  firstNameAr: string;
  lastNameAr: string;
  email?: string;
  phone?: string;
  hireDate?: Date;
  salary?: number;
  isActive?: boolean;
  companyId?: string;
  positionId?: string;
  managerId?: string;
  userId?: string;
}

export interface UpdateEmployeeDto {
  firstNameEn?: string;
  lastNameEn?: string;
  firstNameAr?: string;
  lastNameAr?: string;
  email?: string;
  phone?: string;
  hireDate?: Date;
  salary?: number;
  isActive?: boolean;
  companyId?: string;
  positionId?: string;
  managerId?: string;
  userId?: string;
  salaryChangeReason?: string;
}

export interface EmployeePagedRequest {
  pageNumber?: number;
  pageSize?: number;
  sortBy?: string;
  sortDescending?: boolean;
  searchTerm?: string;
  searchProperties?: string[];
  companyId?: string;
}

export interface EmployeeTreeNodeDto {
  id: string;
  firstNameEn: string;
  lastNameEn: string;
  firstNameAr: string;
  lastNameAr: string;
  fullNameEn: string;
  fullNameAr: string;
  email?: string;
  positionId?: string;
  positionNameEn?: string;
  positionNameAr?: string;
  companyId?: string;
  companyNameEn?: string;
  companyNameAr?: string;
  isActive: boolean;
  filePath?: string;
  children: EmployeeTreeNodeDto[];
}
