import { Component, OnDestroy, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { CompaniesService } from '../services/companies.service';

@Component({
  selector: 'app-client-layout',
  standalone: false,
  templateUrl: './client-layout.component.html',
  styleUrl: './client-layout.component.scss',
})
export class ClientLayoutComponent implements OnDestroy {
  private route = inject(ActivatedRoute);
  private companiesService = inject(CompaniesService);
  private sub: Subscription;

  constructor() {
    this.sub = this.route.queryParams.pipe(
      switchMap(params => {
        const code = params['company'] || 'CDR';
        return this.companiesService.getByCode(code);
      })
    ).subscribe(response => {
      if (response.success && response.data) {
        this.applyColors(response.data.primaryColor, response.data.secondaryColor);
      }
    });
  }

  private applyColors(primary?: string, secondary?: string): void {
    const root = document.documentElement;
    if (primary) {
      root.style.setProperty('--primary-color', primary);
      root.style.setProperty('--custom-btn-bg-color', primary);
      root.style.setProperty('--custom-btn-bg-hover-color', this.darkenColor(primary, 15));
      root.style.setProperty('--link-hover-color', primary);
    }
    if (secondary) {
      root.style.setProperty('--secondary-color', secondary);
    }
  }

  private darkenColor(hex: string, percent: number): string {
    const num = parseInt(hex.replace('#', ''), 16);
    const r = Math.max(0, (num >> 16) - Math.round(2.55 * percent));
    const g = Math.max(0, ((num >> 8) & 0x00FF) - Math.round(2.55 * percent));
    const b = Math.max(0, (num & 0x0000FF) - Math.round(2.55 * percent));
    return `#${(r << 16 | g << 8 | b).toString(16).padStart(6, '0')}`;
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
    const root = document.documentElement;
    root.style.removeProperty('--primary-color');
    root.style.removeProperty('--custom-btn-bg-color');
    root.style.removeProperty('--custom-btn-bg-hover-color');
    root.style.removeProperty('--link-hover-color');
    root.style.removeProperty('--secondary-color');
  }
}
