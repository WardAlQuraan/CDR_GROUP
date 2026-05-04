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

  // Company Title Descriptions
  COMPANY_TITLE_DESCRIPTIONS_READ: 'company-title-descriptions.read',
  COMPANY_TITLE_DESCRIPTIONS_CREATE: 'company-title-descriptions.create',
  COMPANY_TITLE_DESCRIPTIONS_UPDATE: 'company-title-descriptions.update',
  COMPANY_TITLE_DESCRIPTIONS_DELETE: 'company-title-descriptions.delete',

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

  // Company Financial Clauses Rights
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_READ: 'company-financial-clauses-rights.read',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_CREATE: 'company-financial-clauses-rights.create',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_UPDATE: 'company-financial-clauses-rights.update',
  COMPANY_FINANCIAL_CLAUSES_RIGHTS_DELETE: 'company-financial-clauses-rights.delete',

  // Company Client Reaches
  COMPANY_CLIENT_REACHES_READ: 'company-client-reaches.read',
  COMPANY_CLIENT_REACHES_CREATE: 'company-client-reaches.create',
  COMPANY_CLIENT_REACHES_UPDATE: 'company-client-reaches.update',
  COMPANY_CLIENT_REACHES_DELETE: 'company-client-reaches.delete',

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
