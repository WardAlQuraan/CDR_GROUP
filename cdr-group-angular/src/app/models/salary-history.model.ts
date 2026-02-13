export interface SalaryHistoryDto {
  id: string;
  employeeId: string;
  employeeNameEn: string;
  employeeNameAr: string;
  oldSalary: number | null;
  newSalary: number;
  effectiveDate: string;
  reason: string | null;
  createdAt: string;
  updatedAt: string | null;
}

export interface CreateSalaryHistoryDto {
  employeeId: string;
  oldSalary?: number | null;
  newSalary: number;
  effectiveDate: string;
  reason?: string | null;
}

export interface UpdateSalaryHistoryDto {
  oldSalary?: number | null;
  newSalary?: number | null;
  effectiveDate?: string | null;
  reason?: string | null;
}
