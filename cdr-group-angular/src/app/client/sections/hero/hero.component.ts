import { Component, Input, inject, OnInit } from '@angular/core';
import { TranslationService } from '../../../services/translation.service';
import { CompanyDto } from '../../../models/company.model';

@Component({
  selector: 'app-hero',
  standalone: false,
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss',
})
export class HeroComponent implements OnInit {
  @Input() isSmall = false;
  @Input() title = '';
  @Input() company?: CompanyDto;

  showMore = false;

  constructor(private translationService: TranslationService) 
  {
  }

  ngOnInit(): void {
    console.log('HeroComponent initialized with company:', this.company);
  }

  get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  get companyName(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.nameAr : this.company.nameEn) || '';
  }

  get companyTitle(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.titleAr : this.company.titleEn) || '';
  }

  get companyDescription(): string {
    if (!this.company) return '';
    return (this.isArabic ? this.company.descriptionAr : this.company.descriptionEn) || '';
  }

  toggleMore(): void {
    this.showMore = !this.showMore;
  }
}
