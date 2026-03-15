import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CompanyDto } from '../models/company.model';

@Injectable({ providedIn: 'root' })
export class CompanyStateService {
  private companiesSubject = new BehaviorSubject<CompanyDto[]>([]);
  private selectedCompanySubject = new BehaviorSubject<CompanyDto | undefined>(undefined);

  companies$ = this.companiesSubject.asObservable();
  selectedCompany$ = this.selectedCompanySubject.asObservable();

  get companies(): CompanyDto[] {
    return this.companiesSubject.value;
  }

  get selectedCompany(): CompanyDto | undefined {
    return this.selectedCompanySubject.value;
  }

  setCompanies(companies: CompanyDto[]): void {
    this.companiesSubject.next(companies);
  }

  setSelectedCompany(company: CompanyDto | undefined): void {
    this.selectedCompanySubject.next(company);
  }

  findCompany(companies: CompanyDto[], id: string): CompanyDto | undefined {
    for (const company of companies) {
      if (company.id === id) return company;
      if (company.children?.length) {
        const found = this.findCompany(company.children, id);
        if (found) return found;
      }
    }
    return undefined;
  }

  selectById(id: string): void {
    const company = this.findCompany(this.companies, id);
    this.setSelectedCompany(company);
  }
}
