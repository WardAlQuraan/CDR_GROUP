export interface EmployeeDto {
  id: string;
  employeeCode: string;
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
  employeeCode: string;
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
  employeeCode: string;
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
  employeeCode?: string;
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

export interface EmployeeTreeNodeDto {
  id: string;
  employeeCode: string;
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
