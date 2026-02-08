import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';

interface CacheEntry {
  source$: Observable<any>;
  expiry: number;
}

@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private static readonly DEFAULT_TTL = 5 * 60 * 1000; // 5 minutes
  private cache = new Map<string, CacheEntry>();

  get<T>(key: string, source$: () => Observable<T>, ttl = CacheService.DEFAULT_TTL): Observable<T> {
    const entry = this.cache.get(key);
    if (!entry || Date.now() > entry.expiry) {
      this.cache.set(key, {
        source$: source$().pipe(shareReplay(1)),
        expiry: Date.now() + ttl
      });
    }
    return this.cache.get(key)!.source$ as Observable<T>;
  }

  clear(key: string): void {
    this.cache.delete(key);
  }

  clearAll(): void {
    this.cache.clear();
  }
}
