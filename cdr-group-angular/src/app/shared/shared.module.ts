import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

// Angular Material
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatBadgeModule } from '@angular/material/badge';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';

// Pipes
import { TranslatePipe } from '../pipes/translate.pipe';
import { SharedTableComponent } from './shared-table-component/shared-table-component';
import { BaseDialogComponent } from './components/base-dialog/base-dialog.component';
import { DataGridComponent } from './components/data-grid/data-grid.component';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { AsyncSelectComponent } from './components/async-select/async-select.component';
import { DatePickerComponent } from './components/date-picker/date-picker.component';
import { OrgChartComponent } from './components/org-chart/org-chart.component';
import { BulkUploadDialogComponent } from './components/bulk-upload-dialog/bulk-upload-dialog.component';
import { ImagePreviewDialogComponent } from './components/image-preview-dialog/image-preview-dialog.component';
import { FormFieldComponent } from './components/form-field/form-field.component';
import { MapPickerComponent } from './components/map-picker/map-picker.component';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { CustomPaginatorIntl } from './custom-paginator-intl';

@NgModule({
  providers: [
    { provide: MatPaginatorIntl, useClass: CustomPaginatorIntl }
  ],
  declarations: [
    SharedTableComponent,
    BaseDialogComponent,
    DataGridComponent,
    ConfirmDialogComponent,
    AsyncSelectComponent,
    DatePickerComponent,
    OrgChartComponent,
    BulkUploadDialogComponent,
    ImagePreviewDialogComponent,
    FormFieldComponent,
    MapPickerComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    TranslatePipe,
    // Angular Material
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatChipsModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatMenuModule,
    MatDividerModule,
    MatBadgeModule,
    MatProgressBarModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatSortModule,
    MatPaginatorModule,
    MatDialogModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatExpansionModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    TranslatePipe,
    // Angular Material
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatCardModule,
    MatTableModule,
    MatChipsModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatMenuModule,
    MatDividerModule,
    MatBadgeModule,
    MatProgressBarModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatSortModule,
    MatPaginatorModule,
    MatDialogModule,
    MatSelectModule,
    MatSlideToggleModule,
    MatProgressSpinnerModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatExpansionModule,
    // Shared Components
    SharedTableComponent,
    BaseDialogComponent,
    DataGridComponent,
    ConfirmDialogComponent,
    AsyncSelectComponent,
    DatePickerComponent,
    OrgChartComponent,
    BulkUploadDialogComponent,
    ImagePreviewDialogComponent,
    FormFieldComponent,
    MapPickerComponent
  ]
})
export class SharedModule {}
