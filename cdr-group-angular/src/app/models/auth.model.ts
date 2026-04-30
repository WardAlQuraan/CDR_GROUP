export interface LoginDto {
  username: string;
  password: string;
}

export interface RegisterDto {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
  firstName?: string;
  lastName?: string;
}

export interface TokenDto {
  accessToken: string;
  refreshToken: string;
  accessTokenExpiration: Date;
  user: UserInfoDto;
}

export interface RefreshTokenRequestDto {
  refreshToken: string;
}

export interface UserInfoDto {
  id: string;
  username: string;
  email: string;
  firstName?: string;
  lastName?: string;
  roles: string[];
  permissions: string[];
}

export interface ChangePasswordRequestDto {
  currentPassword: string;
  newPassword: string;
  confirmNewPassword: string;
}

// Roles
export const Roles = {
  SUPER_ADMIN: 'Super Admin',
  ADMIN: 'Admin',
  USER: 'User'
} as const;

export type Role = typeof Roles[keyof typeof Roles];

// Permissions
export const Permissions = {
  // Users
  USERS_READ: 'users.read',
  USERS_CREATE: 'users.create',
  USERS_UPDATE: 'users.update',
  USERS_DELETE: 'users.delete',
  USERS_ACTIVATE: 'users.activate',

  // Employees
  EMPLOYEES_READ: 'employees.read',
  EMPLOYEES_CREATE: 'employees.create',
  EMPLOYEES_UPDATE: 'employees.update',
  EMPLOYEES_DELETE: 'employees.delete',
  EMPLOYEES_ASSIGN_MANAGER: 'employees.assign-manager',
  EMPLOYEES_LINK_TO_USER: 'employees.link-to-user',

  // Positions
  POSITIONS_READ: 'positions.read',
  POSITIONS_CREATE: 'positions.create',
  POSITIONS_UPDATE: 'positions.update',
  POSITIONS_DELETE: 'positions.delete',

  // Roles
  ROLES_READ: 'roles.read',
  ROLES_MANAGE: 'roles.manage',

  // Events
  EVENTS_READ: 'events.read',
  EVENTS_CREATE: 'events.create',
  EVENTS_UPDATE: 'events.update',
  EVENTS_DELETE: 'events.delete',

  // Companies
  COMPANIES_READ: 'companies.read',
  COMPANIES_CREATE: 'companies.create',
  COMPANIES_UPDATE: 'companies.update',
  COMPANIES_DELETE: 'companies.delete',

  // Company Backgrounds
  COMPANY_BACKGROUNDS_READ: 'company-backgrounds.read',
  COMPANY_BACKGROUNDS_CREATE: 'company-backgrounds.create',
  COMPANY_BACKGROUNDS_UPDATE: 'company-backgrounds.update',
  COMPANY_BACKGROUNDS_DELETE: 'company-backgrounds.delete',

  // Company Forms
  COMPANY_FORMS_READ: 'company-forms.read',
  COMPANY_FORMS_CREATE: 'company-forms.create',
  COMPANY_FORMS_UPDATE: 'company-forms.update',
  COMPANY_FORMS_DELETE: 'company-forms.delete',

  // Company Preferences
  COMPANY_PREFERENCES_READ: 'company-preferences.read',
  COMPANY_PREFERENCES_CREATE: 'company-preferences.create',
  COMPANY_PREFERENCES_UPDATE: 'company-preferences.update',
  COMPANY_PREFERENCES_DELETE: 'company-preferences.delete',

  // Contact Us
  CONTACTUS_READ: 'contactus.read',
  CONTACTUS_DELETE: 'contactus.delete',

  // Salary Histories
  SALARY_HISTORIES_READ: 'salary-histories.read',
  SALARY_HISTORIES_CREATE: 'salary-histories.create',
  SALARY_HISTORIES_UPDATE: 'salary-histories.update',
  SALARY_HISTORIES_DELETE: 'salary-histories.delete',

  // Company Contacts
  COMPANY_CONTACTS_READ: 'company-contacts.read',
  COMPANY_CONTACTS_CREATE: 'company-contacts.create',
  COMPANY_CONTACTS_UPDATE: 'company-contacts.update',
  COMPANY_CONTACTS_DELETE: 'company-contacts.delete',

  // Company Branches
  COMPANY_BRANCHES_READ: 'company-branches.read',
  COMPANY_BRANCHES_CREATE: 'company-branches.create',
  COMPANY_BRANCHES_UPDATE: 'company-branches.update',
  COMPANY_BRANCHES_DELETE: 'company-branches.delete',

  // Company Success Reasons
  COMPANY_SUCCESS_REASONS_READ: 'company-success-reasons.read',
  COMPANY_SUCCESS_REASONS_CREATE: 'company-success-reasons.create',
  COMPANY_SUCCESS_REASONS_UPDATE: 'company-success-reasons.update',
  COMPANY_SUCCESS_REASONS_DELETE: 'company-success-reasons.delete',

  // Company Distinguishes
  COMPANY_DISTINGUISHES_READ: 'company-distinguishes.read',
  COMPANY_DISTINGUISHES_CREATE: 'company-distinguishes.create',
  COMPANY_DISTINGUISHES_UPDATE: 'company-distinguishes.update',
  COMPANY_DISTINGUISHES_DELETE: 'company-distinguishes.delete',

  // Company Distribution Marketings
  COMPANY_DISTRIBUTION_MARKETINGS_READ: 'company-distribution-marketings.read',
  COMPANY_DISTRIBUTION_MARKETINGS_CREATE: 'company-distribution-marketings.create',
  COMPANY_DISTRIBUTION_MARKETINGS_UPDATE: 'company-distribution-marketings.update',
  COMPANY_DISTRIBUTION_MARKETINGS_DELETE: 'company-distribution-marketings.delete',

  // Company Pre Contract Studies
  COMPANY_PRE_CONTRACT_STUDIES_READ: 'company-pre-contract-studies.read',
  COMPANY_PRE_CONTRACT_STUDIES_CREATE: 'company-pre-contract-studies.create',
  COMPANY_PRE_CONTRACT_STUDIES_UPDATE: 'company-pre-contract-studies.update',
  COMPANY_PRE_CONTRACT_STUDIES_DELETE: 'company-pre-contract-studies.delete',

  // Company Geographic Expansions
  COMPANY_GEOGRAPHIC_EXPANSIONS_READ: 'company-geographic-expansions.read',
  COMPANY_GEOGRAPHIC_EXPANSIONS_CREATE: 'company-geographic-expansions.create',
  COMPANY_GEOGRAPHIC_EXPANSIONS_UPDATE: 'company-geographic-expansions.update',
  COMPANY_GEOGRAPHIC_EXPANSIONS_DELETE: 'company-geographic-expansions.delete',

  // Company Financial Clauses Rights
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_READ: 'company-financial-clauses-rights.read',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_CREATE: 'company-financial-clauses-rights.create',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_UPDATE: 'company-financial-clauses-rights.update',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_DELETE: 'company-financial-clauses-rights.delete',

  // Company Partnership Franchise Mechanisms
  COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_READ: 'company-partnership-franchise-mechanisms.read',
  COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_CREATE: 'company-partnership-franchise-mechanisms.create',
  COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_UPDATE: 'company-partnership-franchise-mechanisms.update',
  COMPANY_PARTNERSHIP_FRANCHISE_MECHANISMS_DELETE: 'company-partnership-franchise-mechanisms.delete',

  // Reviews
  REVIEWS_READ: 'reviews.read',
  REVIEWS_CREATE: 'reviews.create',
  REVIEWS_UPDATE: 'reviews.update',
  REVIEWS_DELETE: 'reviews.delete',

  // Complaints
  COMPLAINTS_READ: 'complaints.read',
  COMPLAINTS_CREATE: 'complaints.create',
  COMPLAINTS_UPDATE: 'complaints.update',
  COMPLAINTS_DELETE: 'complaints.delete',

  // Countries
  COUNTRIES_CREATE: 'countries.create',
  COUNTRIES_UPDATE: 'countries.update',
  COUNTRIES_DELETE: 'countries.delete',

  // Cities
  CITIES_CREATE: 'cities.create',
  CITIES_UPDATE: 'cities.update',
  CITIES_DELETE: 'cities.delete',

  // Partners
  PARTNERS_CREATE: 'partners.create',
  PARTNERS_UPDATE: 'partners.update',
  PARTNERS_DELETE: 'partners.delete',

  // Audit Logs
  AUDIT_LOGS_READ: 'audit-logs.read'
} as const;

export type Permission = typeof Permissions[keyof typeof Permissions];
