import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loadingCount = 0;
  private isLoadingSignal = signal(false);
  private loadingKeys = signal<Set<string>>(new Set());

  readonly isLoading = this.isLoadingSignal.asReadonly();

  show(key?: string): void {
    this.loadingCount++;
    this.isLoadingSignal.set(true);

    if (key) {
      this.loadingKeys.update(keys => new Set(keys).add(key));
    }
  }

  hide(key?: string): void {
    this.loadingCount--;
    if (this.loadingCount <= 0) {
      this.loadingCount = 0;
      this.isLoadingSignal.set(false);
    }

    if (key) {
      this.loadingKeys.update(keys => {
        const newKeys = new Set(keys);
        newKeys.delete(key);
        return newKeys;
      });
    }
  }

  forceHide(): void {
    this.loadingCount = 0;
    this.isLoadingSignal.set(false);
    this.loadingKeys.set(new Set());
  }

  isLoadingKey(key: string): boolean {
    return this.loadingKeys().has(key);
  }

  isLoadingAnyKey(...keys: string[]): boolean {
    const currentKeys = this.loadingKeys();
    return keys.some(key => currentKeys.has(key));
  }
}
