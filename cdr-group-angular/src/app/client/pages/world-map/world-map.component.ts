import { Component, AfterViewInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as L from 'leaflet';
import { CountriesService } from '../../../services/countries.service';
import { PartnersService } from '../../../services/partners.service';
import { TranslationService } from '../../../services/translation.service';
import { CountryDto } from '../../../models/country.model';
import { PartnerDto } from '../../../models/partner.model';

interface City {
  name: string;
  companyName: string;
  countryName: string;
  status: string;
  lat: number;
  lng: number;
  color: string;
  marker?: L.CircleMarker;
}

@Component({
  selector: 'app-world-map',
  standalone: false,
  templateUrl: './world-map.component.html',
  styleUrl: './world-map.component.scss',
})
export class WorldMapComponent implements AfterViewInit, OnDestroy {
  private map!: L.Map;
  private countries: CountryDto[] = [];

  private companyId?: string;

  constructor(
    private route: ActivatedRoute,
    private countriesService: CountriesService,
    private partnersService: PartnersService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef
  ) {
    this.companyId = this.route.snapshot.queryParams['company'];
  }

  private get isArabic(): boolean {
    return this.translationService.language() === 'ar';
  }

  loading = true;
  searchTerm = '';
  filteredCities: City[] = [];
  private partners: PartnerDto[] = [];
  private cities: City[] = [];
  private pendingRequests = 2;

  private readonly statusColorMap: Record<string, string> = {
    'Present': '#e74c3c',      // red
    'NotAvailable': '#ff9800', // orange
    'Available': '#4caf50'     // green
  };

  private readonly statusTranslationMap: Record<string, string> = {
    'Present': 'admin.partners.present',
    'NotAvailable': 'admin.partners.notAvailable',
    'Available': 'admin.partners.available'
  };

  ngAfterViewInit(): void {
    this.initMap();
    this.loadCountries();
    this.loadPartners();
  }

  private onRequestComplete(): void {
    this.pendingRequests--;
    if (this.pendingRequests <= 0) {
      this.loading = false;
      this.cdr.markForCheck();
    }
  }

  private loadCountries(): void {
    this.countriesService.getAllCached().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.countries = response.data;
          this.addCountryLabels();
        }
        this.onRequestComplete();
      },
      error: () => this.onRequestComplete()
    });
  }

  private loadPartners(): void {
    this.partnersService.getAllByCompanyId(this.companyId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.partners = response.data;
          this.cities = this.partners
            .filter(p => p.cityLatitude && p.cityLongitude)
            .map(p => ({
              name: (this.isArabic ? p.cityNameAr : p.cityNameEn) || '-',
              companyName: (this.isArabic ? p.companyNameAr : p.companyNameEn) || '-',
              countryName: (this.isArabic ? p.countryNameAr : p.countryNameEn) || '-',
              status: p.status,
              lat: p.cityLatitude!,
              lng: p.cityLongitude!,
              color: this.statusColorMap[p.status] || '#2196f3'
            }));
          this.filteredCities = [...this.cities];
          this.addCities();
        }
        this.onRequestComplete();
      },
      error: () => this.onRequestComplete()
    });
  }

  onSearch(): void {
    const term = this.searchTerm.toLowerCase().trim();
    if (!term) {
      this.filteredCities = [...this.cities];
    } else {
      this.filteredCities = this.cities.filter(c =>
        c.name.toLowerCase().includes(term) ||
        c.companyName.toLowerCase().includes(term) ||
        c.countryName.toLowerCase().includes(term)
      );
    }
    // Show/hide markers based on filter
    for (const city of this.cities) {
      if (city.marker) {
        const isVisible = this.filteredCities.includes(city);
        if (isVisible) {
          city.marker.addTo(this.map);
        } else {
          city.marker.removeFrom(this.map);
        }
      }
    }
  }

  getStatusLabel(status: string): string {
    return this.translationService.translate(this.statusTranslationMap[status] || status);
  }

  ngOnDestroy(): void {
    this.map?.remove();
  }

  private initMap(): void {
    const middleEastBounds = L.latLngBounds(
      L.latLng(12, -18),  // Southwest (Morocco, Yemen)
      L.latLng(42, 60),   // Northeast (Turkey, Iran)
    );

    this.map = L.map('world-map', {
      center: [29, 20],
      zoom: 4,
      minZoom: 3,
      maxZoom: 12,
      maxBounds: middleEastBounds.pad(0.1),
      maxBoundsViscosity: 1.0,
    });

    this.map.fitBounds(middleEastBounds);

    // Clean map without labels — no country names shown
    L.tileLayer('https://{s}.basemaps.cartocdn.com/light_nolabels/{z}/{x}/{y}{r}.png', {
      attribution: '&copy; OpenStreetMap &copy; CARTO',
    }).addTo(this.map);

    // Country labels are added after API response in loadCountries()
  }

  private addCountryLabels(): void {
    for (const country of this.countries) {
      const name = this.isArabic ? country.nameAr : country.nameEn;
      L.marker([country.latitude, country.longitude], {
        icon: L.divIcon({
          className: 'country-label',
          html: `<span>${name}</span>`,
          iconSize: [100, 20],
          iconAnchor: [50, 10],
        }),
        interactive: false,
      }).addTo(this.map);
    }
  }

  private addCities(): void {
    for (const city of this.cities) {
      const marker = L.circleMarker([city.lat, city.lng], {
        radius: 7,
        fillColor: city.color,
        color: '#fff',
        weight: 2,
        fillOpacity: 0.9,
      }).addTo(this.map);

      const statusLabel = this.translationService.translate(this.statusTranslationMap[city.status] || city.status);
      marker.bindTooltip(`${city.companyName} - ${city.name}<br>${statusLabel}`, { direction: 'top', offset: [0, -8] });
      city.marker = marker;
    }
  }
}
