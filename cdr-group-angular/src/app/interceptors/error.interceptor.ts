import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SnackbarService } from '../services/snackbar.service';
import { ApiResponse } from '../models/api-response.model';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private snackbar: SnackbarService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        // Don't show error for refresh token failures (auth interceptor handles logout)
        if (request.url.includes('/auth/refresh-token')) {
          return throwError(() => ({
            status: error.status,
            message: 'Token refresh failed',
            originalError: error
          }));
        }

        let errorMessage = 'An unexpected error occurred';
        if (error.error instanceof ErrorEvent) {
          // Client-side error
          errorMessage = error.error.message;
        } else {
          // Handle specific HTTP status codes first
          switch (error.status) {
            case 0:
              errorMessage = 'Unable to connect to server. Please check your internet connection.';
              break;
            case 401:
              // AuthInterceptor handles logout and redirect, skip showing error
              return throwError(() => ({
                status: error.status,
                message: 'Session expired',
                originalError: error
              }));
            case 403:
              errorMessage = 'Access denied. You do not have permission to perform this action.';
              break;
            case 404:
              errorMessage = 'Resource not found.';
              break;
            case 500:
              errorMessage = 'Internal server error. Please try again later.';
              break;
            case 502:
              errorMessage = 'Bad gateway. The server is temporarily unavailable.';
              break;
            case 503:
              errorMessage = 'Service unavailable. Please try again later.';
              break;
            case 504:
              errorMessage = 'Gateway timeout. The server took too long to respond.';
              break;
            default:
              // Try to extract message from API response
              const apiResponse = error.error as ApiResponse<any>;
              if (apiResponse && !apiResponse.success) {
                if (apiResponse.errors && apiResponse.errors.length > 0) {
                  errorMessage = apiResponse.errors.join(', ');
                } else if (apiResponse.message) {
                  errorMessage = apiResponse.message;
                }
              } else if (error.error?.message) {
                errorMessage = error.error.message;
              } else if (error.status) {
                errorMessage = `Error: ${error.status}`;
              }
          }
        }

        // Show error message in snackbar
        this.snackbar.error(errorMessage);

        // Return the error for further handling if needed
        return throwError(() => ({
          status: error.status,
          message: errorMessage,
          originalError: error
        }));
      })
    );
  }
}
