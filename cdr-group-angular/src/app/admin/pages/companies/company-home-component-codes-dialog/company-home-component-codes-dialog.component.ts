import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import {
  CompanyHomeComponentSetupDto,
  CompanyHomeComponentSetupRankUpdate,
} from '../../../../models/company-home-component-setup.model';
import { CompanyHomeComponentSetupsService } from '../../../../services/company-home-component-setups.service';
import { SnackbarService } from '../../../../services/snackbar.service';
import { TranslationService } from '../../../../services/translation.service';

export interface CompanyHomeComponentCodesDialogData {
  companyId: string;
  companyName: string;
}

@Component({
  selector: 'app-company-home-component-codes-dialog',
  standalone: false,
  templateUrl: './company-home-component-codes-dialog.component.html',
  styleUrl: './company-home-component-codes-dialog.component.scss',
})
export class CompanyHomeComponentCodesDialogComponent implements OnInit {
  setups: CompanyHomeComponentSetupDto[] = [];
  loading = false;
  saving = false;
  hasChanges = false;

  private originalRanks = new Map<string, number>();

  constructor(
    private dialogRef: MatDialogRef<CompanyHomeComponentCodesDialogComponent>,
    private companyHomeComponentSetupsService: CompanyHomeComponentSetupsService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private cdr: ChangeDetectorRef,
    @Inject(MAT_DIALOG_DATA) public data: CompanyHomeComponentCodesDialogData
  ) {}

  ngOnInit(): void {
    this.load();
  }

  private translate(key: string): string {
    return this.translationService.translate(key);
  }

  private load(): void {
    this.loading = true;
    this.companyHomeComponentSetupsService.getByCompany(this.data.companyId).subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.setups = [...response.data].sort((a, b) => a.rank - b.rank);
        } else {
          this.setups = [];
        }
        this.snapshotOriginalRanks();
        this.hasChanges = false;
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.setups = [];
        this.hasChanges = false;
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }

  private snapshotOriginalRanks(): void {
    this.originalRanks.clear();
    for (const setup of this.setups) {
      this.originalRanks.set(setup.id, setup.rank);
    }
  }

  private recomputeHasChanges(): void {
    this.hasChanges = this.setups.some(
      (setup, index) => this.originalRanks.get(setup.id) !== index
    );
  }

  onDrop(event: CdkDragDrop<CompanyHomeComponentSetupDto[]>): void {
    if (this.saving || event.previousIndex === event.currentIndex) {
      return;
    }

    moveItemInArray(this.setups, event.previousIndex, event.currentIndex);
    this.setups = this.setups.map((setup, index) => ({ ...setup, rank: index }));
    this.recomputeHasChanges();
    this.cdr.markForCheck();
  }

  onSave(): void {
    if (this.saving || !this.hasChanges) {
      return;
    }

    const payload: CompanyHomeComponentSetupRankUpdate[] = this.setups
      .filter((setup, index) => this.originalRanks.get(setup.id) !== index)
      .map((setup) => ({ id: setup.id, rank: setup.rank }));

    if (payload.length === 0) {
      this.hasChanges = false;
      return;
    }

    this.saving = true;
    this.companyHomeComponentSetupsService.reorder(payload).subscribe({
      next: () => {
        this.saving = false;
        this.snapshotOriginalRanks();
        this.hasChanges = false;
        this.snackbar.success(this.translate('admin.companies.homeComponentCodesReordered'));
        this.cdr.markForCheck();
      },
      error: (error: Error) => {
        this.saving = false;
        this.snackbar.error(
          error.message || this.translate('admin.companyHomeComponentSetups.errors.updateFailed')
        );
        this.load();
      },
    });
  }

  onClose(): void {
    this.dialogRef.close(this.hasChanges ? false : undefined);
  }
}
