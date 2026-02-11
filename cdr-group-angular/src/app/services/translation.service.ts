import { Injectable, signal, computed, ApplicationRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export type Language = 'en' | 'ar';

export interface TranslationDictionary {
  [key: string]: string | TranslationDictionary;
}

@Injectable({
  providedIn: 'root'
})
export class TranslationService {
  private readonly STORAGE_KEY = 'app_language';
  private readonly DEFAULT_LANGUAGE: Language = 'en';

  private translations = signal<TranslationDictionary>({});
  private currentLanguage = signal<Language>(this.getStoredLanguage());
  private isLoading = signal<boolean>(false);

  // Public readonly signals
  readonly language = this.currentLanguage.asReadonly();
  readonly loading = this.isLoading.asReadonly();
  readonly isRtl = computed(() => this.currentLanguage() === 'ar');
  readonly dir = computed(() => this.isRtl() ? 'rtl' : 'ltr');

  constructor(
    private http: HttpClient,
    private appRef: ApplicationRef
  ) {
    this.loadTranslations(this.currentLanguage());
  }

  /**
   * Get stored language from localStorage or return default
   */
  private getStoredLanguage(): Language {
    if (typeof localStorage !== 'undefined') {
      const stored = localStorage.getItem(this.STORAGE_KEY);
      if (stored === 'en' || stored === 'ar') {
        return stored;
      }
    }
    return this.DEFAULT_LANGUAGE;
  }

  /**
   * Store language preference in localStorage
   */
  private storeLanguage(lang: Language): void {
    if (typeof localStorage !== 'undefined') {
      localStorage.setItem(this.STORAGE_KEY, lang);
    }
  }

  /**
   * Load translations from JSON file
   */
  async loadTranslations(lang: Language): Promise<void> {
    this.isLoading.set(true);
    try {
      const data = await firstValueFrom(
        this.http.get<TranslationDictionary>(`/assets/i18n/${lang}.json`)
      );
      this.translations.set(data);
      this.currentLanguage.set(lang);
      this.storeLanguage(lang);
      this.updateDocumentDirection();
      // Trigger change detection to update all translate pipes
      this.appRef.tick();
    } catch (error) {
      console.error(`Failed to load translations for ${lang}:`, error);
      // Fallback to default language if not already
      if (lang !== this.DEFAULT_LANGUAGE) {
        await this.loadTranslations(this.DEFAULT_LANGUAGE);
      }
    } finally {
      this.isLoading.set(false);
    }
  }

  /**
   * Switch between languages
   */
  setLanguage(lang: Language): void {
    if (lang !== this.currentLanguage()) {
      this.storeLanguage(lang);
      window.location.reload();
    }
  }

  /**
   * Toggle between Arabic and English
   */
  toggleLanguage(): void {
    const newLang: Language = this.currentLanguage() === 'en' ? 'ar' : 'en';
    this.setLanguage(newLang);
  }

  /**
   * Get translation by key path (e.g., 'common.home' or 'header.logo')
   */
  translate(key: string, fallback?: string): string {
    const keys = key.split('.');
    let result: string | TranslationDictionary = this.translations();

    for (const k of keys) {
      if (result && typeof result === 'object' && k in result) {
        result = result[k];
      } else {
        return fallback ?? key;
      }
    }

    return typeof result === 'string' ? result : (fallback ?? key);
  }

  /**
   * Shorthand method for translate
   */
  t(key: string, fallback?: string): string {
    return this.translate(key, fallback);
  }

  /**
   * Get a computed signal for a translation key (reactive)
   */
  t$(key: string, fallback?: string) {
    return computed(() => this.translate(key, fallback));
  }

  /**
   * Update document direction for RTL/LTR support
   */
  private updateDocumentDirection(): void {
    if (typeof document !== 'undefined') {
      const dir = this.isRtl() ? 'rtl' : 'ltr';
      const lang = this.currentLanguage();
      document.documentElement.setAttribute('dir', dir);
      document.documentElement.setAttribute('lang', lang);
      document.body.setAttribute('dir', dir);
    }
  }

  /**
   * Get current language display name
   */
  getLanguageName(lang?: Language): string {
    const language = lang ?? this.currentLanguage();
    return language === 'ar' ? 'العربية' : 'English';
  }

  /**
   * Get the opposite language (for toggle button display)
   */
  getOtherLanguageName(): string {
    return this.currentLanguage() === 'en' ? 'العربية' : 'English';
  }

}
