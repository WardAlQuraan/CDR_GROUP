import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap, catchError, throwError, BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import {
  LoginDto,
  RegisterDto,
  TokenDto,
  UserInfoDto,
  ChangePasswordRequestDto,
  RefreshTokenRequestDto,
  Roles,
  type Role,
  type Permission
} from '../models/auth.model';

export interface User {
  id: string;
  username: string;
  name: string;
  email: string;
  roles: string[];
  permissions: string[];
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly API_URL = `${environment.apiUrl}/auth`;
  private readonly ACCESS_TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly USER_KEY = 'auth_user';

  private currentUser = signal<User | null>(null);
  private refreshTokenInProgress = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  readonly isLoggedIn = computed(() => this.currentUser() !== null);
  readonly user = computed(() => this.currentUser());
  readonly isSuperAdmin = computed(() => this.currentUser()?.roles?.includes(Roles.SUPER_ADMIN) ?? false);
  readonly isAdmin = computed(() => this.hasAnyRole([Roles.SUPER_ADMIN, Roles.ADMIN]));

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage(): void {
    const stored = localStorage.getItem(this.USER_KEY);
    const token = this.getAccessToken();
    if (stored && token) {
      try {
        this.currentUser.set(JSON.parse(stored));
      } catch {
        this.clearStorage();
      }
    }
  }

  login(dto: LoginDto): Observable<ApiResponse<TokenDto>> {
    return this.http.post<ApiResponse<TokenDto>>(`${this.API_URL}/login`, dto).pipe(
      tap(response => {
        if (response.success && response.data) {
          this.handleAuthSuccess(response.data);
        }
      }),
      catchError(error => this.handleError(error))
    );
  }

  register(dto: RegisterDto): Observable<ApiResponse<TokenDto>> {
    return this.http.post<ApiResponse<TokenDto>>(`${this.API_URL}/register`, dto).pipe(
      tap(response => {
        if (response.success && response.data) {
          this.handleAuthSuccess(response.data);
        }
      }),
      catchError(error => this.handleError(error))
    );
  }

  refreshToken(): Observable<ApiResponse<TokenDto>> {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      return throwError(() => new Error('No refresh token available'));
    }

    const dto: RefreshTokenRequestDto = { refreshToken };
    return this.http.post<ApiResponse<TokenDto>>(`${this.API_URL}/refresh-token`, dto).pipe(
      tap(response => {
        if (response.success && response.data) {
          this.handleAuthSuccess(response.data);
          this.refreshTokenSubject.next(response.data.accessToken);
        }
      }),
      catchError(error => {
        this.logout();
        return this.handleError(error);
      })
    );
  }

  revokeToken(): Observable<ApiResponse<null>> {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      return throwError(() => new Error('No refresh token available'));
    }

    const dto: RefreshTokenRequestDto = { refreshToken };
    return this.http.post<ApiResponse<null>>(`${this.API_URL}/revoke-token`, dto).pipe(
      catchError(error => this.handleError(error))
    );
  }

  getCurrentUser(): Observable<ApiResponse<UserInfoDto>> {
    return this.http.get<ApiResponse<UserInfoDto>>(`${this.API_URL}/me`).pipe(
      tap(response => {
        if (response.success && response.data) {
          const user = this.mapUserInfoToUser(response.data);
          this.currentUser.set(user);
          localStorage.setItem(this.USER_KEY, JSON.stringify(user));
        }
      }),
      catchError(error => this.handleError(error))
    );
  }

  changePassword(dto: ChangePasswordRequestDto): Observable<ApiResponse<null>> {
    return this.http.post<ApiResponse<null>>(`${this.API_URL}/change-password`, dto).pipe(
      catchError(error => this.handleError(error))
    );
  }

  logout(): void {
    const refreshToken = this.getRefreshToken();
    if (refreshToken) {
      this.revokeToken().subscribe({
        error: () => {} // Silently fail if revoke fails
      });
    }
    this.clearStorage();
    this.currentUser.set(null);
    this.router.navigate(['/']);
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  isTokenExpired(): boolean {
    const token = this.getAccessToken();
    if (!token) return true;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expiry = payload.exp * 1000;
      return Date.now() >= expiry;
    } catch {
      return true;
    }
  }

  getRefreshTokenSubject(): BehaviorSubject<string | null> {
    return this.refreshTokenSubject;
  }

  setRefreshTokenInProgress(value: boolean): void {
    this.refreshTokenInProgress = value;
  }

  isRefreshTokenInProgress(): boolean {
    return this.refreshTokenInProgress;
  }

  hasRole(role: Role | string): boolean {
    return this.currentUser()?.roles?.includes(role) ?? false;
  }

  hasPermission(permission: Permission | string): boolean {
    return this.currentUser()?.permissions?.includes(permission) ?? false;
  }

  hasAnyRole(roles: (Role | string)[]): boolean {
    return roles.some(role => this.hasRole(role));
  }

  hasAnyPermission(permissions: (Permission | string)[]): boolean {
    return permissions.some(permission => this.hasPermission(permission));
  }

  hasAllPermissions(permissions: (Permission | string)[]): boolean {
    return permissions.every(permission => this.hasPermission(permission));
  }

  private handleAuthSuccess(tokenDto: TokenDto): void {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, tokenDto.accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, tokenDto.refreshToken);

    const user = this.mapUserInfoToUser(tokenDto.user);
    this.currentUser.set(user);
    localStorage.setItem(this.USER_KEY, JSON.stringify(user));
  }

  private mapUserInfoToUser(userInfo: UserInfoDto): User {
    return {
      id: userInfo.id,
      username: userInfo.username,
      name: userInfo.firstName && userInfo.lastName
        ? `${userInfo.firstName} ${userInfo.lastName}`
        : userInfo.username,
      email: userInfo.email,
      roles: userInfo.roles,
      permissions: userInfo.permissions
    };
  }

  private clearStorage(): void {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.USER_KEY);
  }

  private handleError(error: any): Observable<never> {
    let errorMessage = 'An error occurred';
    if (error.error?.message) {
      errorMessage = error.error.message;
    } else if (error.error?.errors?.length) {
      errorMessage = error.error.errors.join(', ');
    } else if (error.message) {
      errorMessage = error.message;
    }
    return throwError(() => new Error(errorMessage));
  }
}
