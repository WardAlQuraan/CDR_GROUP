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

  departmentId?: string;
  departmentName?: string;

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
  departmentId?: string;
  departmentName?: string;
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
  departmentId?: string;
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
  departmentId?: string;
  positionId?: string;
  managerId?: string;
  userId?: string;
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
  departmentId?: string;
  departmentNameEn?: string;
  departmentNameAr?: string;
  isActive: boolean;
  filePath?: string;
  children: EmployeeTreeNodeDto[];
}
