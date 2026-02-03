import { Injectable, inject } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

export type SnackbarType = 'success' | 'error' | 'warning' | 'info';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  private snackBar = inject(MatSnackBar);

  private readonly defaultConfig: MatSnackBarConfig = {
    duration: 4000,
    horizontalPosition: 'end',
    verticalPosition: 'top'
  };

  success(message: string, action = 'Close', config?: MatSnackBarConfig): void {
    this.show(message, action, 'success', config);
  }

  error(message: string, action = 'Close', config?: MatSnackBarConfig): void {
    this.show(message, action, 'error', { ...config, duration: config?.duration ?? 6000 });
  }

  warning(message: string, action = 'Close', config?: MatSnackBarConfig): void {
    this.show(message, action, 'warning', config);
  }

  info(message: string, action = 'Close', config?: MatSnackBarConfig): void {
    this.show(message, action, 'info', config);
  }

private show(message: string, action: string, type: SnackbarType, config?: MatSnackBarConfig): void {
  const snackRef = this.snackBar.open(message, action, {
    ...this.defaultConfig,
    ...config,
    panelClass: [`snackbar-${type}`],
  });

  
}

  dismiss(): void {
    this.snackBar.dismiss();
  }
}
