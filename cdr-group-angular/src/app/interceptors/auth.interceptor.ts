import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, filter, take, switchMap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Add ngrok header to bypass browser warning (required for ngrok free tier)
    request = request.clone({
      setHeaders: {
        'ngrok-skip-browser-warning': 'true'
      }
    });

    // Skip auth header for refresh token and login/register requests
    if (this.isAuthEndpoint(request.url)) {
      return next.handle(request);
    }

    // Add token to request
    const token = this.authService.getAccessToken();
    if (token) {
      request = this.addToken(request, token);
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log('AuthInterceptor caught an error:', error);
        if (error.status === 401 && !this.isAuthEndpoint(request.url)) {
          return this.handle401Error(request, next);
        }
        return throwError(() => error);
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string): HttpRequest<any> {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  private isAuthEndpoint(url: string): boolean {
    return url.includes('/auth/login') ||
           url.includes('/auth/register') ||
           url.includes('/auth/refresh-token');
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.authService.isRefreshTokenInProgress()) {
      this.authService.setRefreshTokenInProgress(true);
      this.authService.getRefreshTokenSubject().next(null);

      return this.authService.refreshToken().pipe(
        switchMap(response => {
          this.authService.setRefreshTokenInProgress(false);
          if (response.success && response.data) {
            return next.handle(this.addToken(request, response.data.accessToken));
          }
          this.authService.logout();
          return throwError(() => new Error('Token refresh failed'));
        }),
        catchError(error => {
          this.authService.setRefreshTokenInProgress(false);
          this.authService.logout();
          return throwError(() => error);
        })
      );
    } else {
      // Wait for the refresh token to complete
      return this.authService.getRefreshTokenSubject().pipe(
        filter(token => token !== null),
        take(1),
        switchMap(token => {
          return next.handle(this.addToken(request, token!));
        })
      );
    }
  }
}
