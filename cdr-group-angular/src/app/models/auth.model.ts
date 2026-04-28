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
