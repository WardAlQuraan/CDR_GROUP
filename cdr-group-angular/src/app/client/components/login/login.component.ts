import { Component, Output, EventEmitter, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { SnackbarService } from '../../../services/snackbar.service';
import { TranslationService } from '../../../services/translation.service';
import { LoginDto, Roles } from '../../../models/auth.model';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private snackbar: SnackbarService,
    private translationService: TranslationService,
    private router: Router,
    private fb: FormBuilder,
    private cdr: ChangeDetectorRef
  ) { }

  @Output() loginSuccess = new EventEmitter<void>();

  loginForm!: FormGroup;
  loginError = '';
  hidePassword = true;
  isLoading = false;

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(4)]]
    });
  }

  onLogin(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loginError = '';
    this.isLoading = true;

    const dto: LoginDto = {
      username: this.loginForm.value.username,
      password: this.loginForm.value.password
    };

    this.authService.login(dto).subscribe({
      next: (response) => {
        this.isLoading = false;
        if (response.success) {
          this.loginForm.reset();
          this.loginSuccess.emit();
          this.snackbar.success(this.translationService.t('header.loginSuccess'));
          this.cdr.markForCheck();
          const isAdmin = this.authService.hasRole(Roles.SUPER_ADMIN) || this.authService.hasRole(Roles.ADMIN);
          this.closeOffcanvasAndNavigate(isAdmin);
        }
      },
      error: () => {
        this.cdr.markForCheck();
        this.isLoading = false;
      }
    });
  }

  private closeOffcanvasAndNavigate(isAdmin: boolean): void {
    const offcanvas = document.getElementById('offcanvasLogin');
    if (offcanvas) {
      const bsOffcanvas = (window as any).bootstrap?.Offcanvas?.getInstance(offcanvas);
      if (bsOffcanvas) {
        offcanvas.addEventListener('hidden.bs.offcanvas', () => {
          if (isAdmin) {
            this.router.navigate(['/admin']);
          }
        }, { once: true });
        bsOffcanvas.hide();
      } else if (isAdmin) {
        this.router.navigate(['/admin']);
      }
    } else if (isAdmin) {
      this.router.navigate(['/admin']);
    }
  }

  get f() {
    return this.loginForm.controls;
  }
}
