import {
  Component,
  Input,
  OnInit,
  OnDestroy,
  AfterViewInit,
  forwardRef,
  ChangeDetectorRef,
  ViewEncapsulation,
  ElementRef,
  ViewChild,
  NgZone
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Subject, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, takeUntil, catchError } from 'rxjs/operators';
import * as L from 'leaflet';

export interface MapLocation {
  latitude: number;
  longitude: number;
  address: string;
}

interface NominatimResult {
  display_name: string;
  lat: string;
  lon: string;
}

// Fix default marker icon paths for Angular bundled builds
const DefaultIcon = L.icon({
  iconUrl: 'assets/leaflet/marker-icon.png',
  iconRetinaUrl: 'assets/leaflet/marker-icon-2x.png',
  shadowUrl: 'assets/leaflet/marker-shadow.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = DefaultIcon;

@Component({
  selector: 'app-map-picker',
  standalone: false,
  templateUrl: './map-picker.component.html',
  styleUrl: './map-picker.component.scss',
  encapsulation: ViewEncapsulation.None,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MapPickerComponent),
      multi: true
    }
  ]
})
export class MapPickerComponent implements OnInit, AfterViewInit, OnDestroy, ControlValueAccessor {
  @Input() placeholder = 'mapPicker.searchPlaceholder';
  @Input() mapHeight = '300px';
  @Input() defaultLat = 31.9539;
  @Input() defaultLng = 35.9106;
  @Input() defaultZoom = 13;

  @ViewChild('mapContainer', { static: true }) mapContainer!: ElementRef<HTMLDivElement>;

  private map!: L.Map;
  private marker: L.Marker | null = null;
  private destroy$ = new Subject<void>();
  searchText$ = new Subject<string>();
  searchResults: NominatimResult[] = [];
  searchLoading = false;
  showResults = false;
  currentAddress = '';
  disabled = false;
  value: MapLocation | null = null;

  private onChange: (value: MapLocation | null) => void = () => {};
  onTouched: () => void = () => {};

  private readonly NOMINATIM_URL = 'https://nominatim.openstreetmap.org';

  constructor(
    private cdr: ChangeDetectorRef,
    private http: HttpClient,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    this.searchText$.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      takeUntil(this.destroy$),
      switchMap(query => {
        if (!query || query.trim().length < 3) {
          this.searchResults = [];
          this.showResults = false;
          this.searchLoading = false;
          this.cdr.markForCheck();
          return of([]);
        }
        this.searchLoading = true;
        this.cdr.markForCheck();
        return this.geocodeSearch(query).pipe(
          catchError(() => {
            this.searchLoading = false;
            this.cdr.markForCheck();
            return of([]);
          })
        );
      })
    ).subscribe(results => {
      this.searchResults = results;
      this.showResults = results.length > 0;
      this.searchLoading = false;
      this.cdr.markForCheck();
    });
  }

  ngAfterViewInit(): void {
    this.initMap();

    if (this.value) {
      this.setMarkerAt(this.value.latitude, this.value.longitude);
      this.map.setView([this.value.latitude, this.value.longitude], this.defaultZoom);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
    if (this.map) {
      this.map.remove();
    }
  }

  private initMap(): void {
    this.map = L.map(this.mapContainer.nativeElement, {
      center: [this.defaultLat, this.defaultLng],
      zoom: this.defaultZoom,
      zoomControl: true
    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      maxZoom: 19
    }).addTo(this.map);

    this.map.on('click', (e: L.LeafletMouseEvent) => {
      if (this.disabled) return;
      this.ngZone.run(() => {
        this.setMarkerAt(e.latlng.lat, e.latlng.lng);
        this.reverseGeocode(e.latlng.lat, e.latlng.lng);
        this.onTouched();
      });
    });
  }

  invalidateMapSize(): void {
    if (this.map) {
      setTimeout(() => {
        this.map.invalidateSize();
      }, 200);
    }
  }

  private setMarkerAt(lat: number, lng: number): void {
    if (this.marker) {
      this.marker.setLatLng([lat, lng]);
    } else {
      this.marker = L.marker([lat, lng], { draggable: !this.disabled }).addTo(this.map);

      this.marker.on('dragend', () => {
        const pos = this.marker!.getLatLng();
        this.ngZone.run(() => {
          this.reverseGeocode(pos.lat, pos.lng);
        });
      });
    }
  }

  private geocodeSearch(query: string) {
    return this.http.get<NominatimResult[]>(`${this.NOMINATIM_URL}/search`, {
      params: {
        q: query,
        format: 'json',
        addressdetails: '1',
        limit: '5'
      }
    });
  }

  onSearchInput(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.searchText$.next(value);
  }

  selectSearchResult(result: NominatimResult): void {
    const lat = parseFloat(result.lat);
    const lng = parseFloat(result.lon);

    this.setMarkerAt(lat, lng);
    this.map.setView([lat, lng], 16);
    this.currentAddress = result.display_name;

    this.value = { latitude: lat, longitude: lng, address: result.display_name };
    this.onChange(this.value);
    this.onTouched();

    this.showResults = false;
    this.searchResults = [];
    this.cdr.markForCheck();
  }

  private reverseGeocode(lat: number, lng: number): void {
    this.value = { latitude: lat, longitude: lng, address: this.currentAddress || '' };
    this.onChange(this.value);

    this.http.get<{ display_name: string }>(`${this.NOMINATIM_URL}/reverse`, {
      params: {
        lat: lat.toString(),
        lon: lng.toString(),
        format: 'json'
      }
    }).pipe(
      takeUntil(this.destroy$),
      catchError(() => of(null))
    ).subscribe(result => {
      if (result) {
        this.currentAddress = result.display_name;
        this.value = { latitude: lat, longitude: lng, address: result.display_name };
        this.onChange(this.value);
        this.cdr.markForCheck();
      }
    });
  }

  geocodeAddress(address: string): void {
    if (!address || address.trim().length < 2) return;

    this.geocodeSearch(address).pipe(
      takeUntil(this.destroy$),
      catchError(() => of([]))
    ).subscribe(results => {
      if (results.length > 0) {
        const result = results[0];
        const lat = parseFloat(result.lat);
        const lng = parseFloat(result.lon);

        this.setMarkerAt(lat, lng);
        if (this.map) {
          this.map.setView([lat, lng], 16);
        }
        this.currentAddress = result.display_name;
        this.value = { latitude: lat, longitude: lng, address: result.display_name };
        this.onChange(this.value);
        this.cdr.markForCheck();
      }
    });
  }

  clearLocation(): void {
    if (this.marker) {
      this.map.removeLayer(this.marker);
      this.marker = null;
    }
    this.value = null;
    this.currentAddress = '';
    this.onChange(null);
    this.onTouched();
    this.map.setView([this.defaultLat, this.defaultLng], this.defaultZoom);
    this.cdr.markForCheck();
  }

  closeSearchResults(): void {
    this.showResults = false;
  }

  // ControlValueAccessor
  writeValue(value: MapLocation | null): void {
    this.value = value;
    if (value) {
      this.currentAddress = value.address;
      if (this.map) {
        this.setMarkerAt(value.latitude, value.longitude);
        this.map.setView([value.latitude, value.longitude], this.defaultZoom);
      }
    } else {
      this.currentAddress = '';
      if (this.marker && this.map) {
        this.map.removeLayer(this.marker);
        this.marker = null;
      }
    }
    this.cdr.markForCheck();
  }

  registerOnChange(fn: (value: MapLocation | null) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
    if (this.marker) {
      if (isDisabled) {
        this.marker.dragging?.disable();
      } else {
        this.marker.dragging?.enable();
      }
    }
    this.cdr.markForCheck();
  }
}
