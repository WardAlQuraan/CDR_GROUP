export interface DepartmentDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive: boolean;
  parentDepartmentId?: string;
  parentDepartment?: DepartmentBasicDto;
  managerId?: string;
  managerName?: string;
  managerNameAr?: string;
  companyId?: string;
  companyName?: string;
  companyNameAr?: string;
  createdAt: Date;
  updatedAt?: Date;
}

export interface DepartmentBasicDto {
  id: string;
  code: string;
  nameEn: string;
  nameAr: string;
}

export interface DepartmentWithSubDepartmentsDto extends DepartmentDto {
  subDepartments: DepartmentBasicDto[];
}

export interface DepartmentWithEmployeesDto extends DepartmentDto {
  employeeCount: number;
}

export interface CreateDepartmentDto {
  code: string;
  nameEn: string;
  nameAr: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive?: boolean;
  parentDepartmentId?: string;
  managerId?: string;
  companyId?: string;
}

export interface UpdateDepartmentDto {
  code?: string;
  nameEn?: string;
  nameAr?: string;
  descriptionEn?: string;
  descriptionAr?: string;
  isActive?: boolean;
  parentDepartmentId?: string;
  managerId?: string;
  companyId?: string;
}
