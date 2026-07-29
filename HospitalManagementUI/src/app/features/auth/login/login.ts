import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { map } from 'rxjs';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Auth } from '../../../core/services/auth';
import { LoginRequest } from '../../../core/models/auth/login-request';

type Role = 'patient' | 'doctor' | 'admin';

interface RoleTab {
  key: Role;
  label: string;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterLink,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class LoginComponent {
  tabs: RoleTab[] = [
    { key: 'patient', label: 'Patient' },
    { key: 'doctor', label: 'Doctor' },
    { key: 'admin', label: 'Admin' }
  ];

  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  private auth = inject(Auth);
  private router = inject(Router);
  errorMessage = '';

  // UI relate logic
  private roleParam = toSignal(
    this.route.paramMap.pipe(map((params) => (params.get('role') ?? 'patient').toLowerCase())),
    { initialValue: 'patient' }
  );

  role = computed<Role>(() => {
    const value = this.roleParam();
    return value === 'doctor' || value === 'admin' ? value : 'patient';
  });

  roleLabel = computed(() => {
    const map: Record<Role, string> = { patient: 'Patient', doctor: 'Doctor', admin: 'Admin' };
    return map[this.role()];
  });

  registerRoute = computed(() => {
  return this.role() === 'doctor'
    ? '/doctor-register'
    : '/patient-register';
});

  emailPlaceholder = computed(() => {
    const map: Record<Role, string> = {
      patient: 'name@example.com',
      doctor: 'doctor@clinic.com',
      admin: 'admin@careplus.com'
    };
    return map[this.role()];
  });

  showPassword = signal(false);
  rememberMe = signal(false);

  togglePasswordVisibility(): void {
    this.showPassword.update((v) => !v);
  }

  toggleRememberMe(): void {
    this.rememberMe.update((v) => !v);
  }

  // Backend related logic
  loginForm = this.fb.group({
  email: ['', [Validators.required, Validators.email]],
  password: ['', [Validators.required]]
});

onSubmit(): void {

  this.errorMessage = '';

  if (this.loginForm.invalid) {
    this.loginForm.markAllAsTouched();
    return;
  }

  const request = this.loginForm.getRawValue() as LoginRequest;

  this.auth.login(request).subscribe({
    next: (response) => {
      alert("Login Successful");
      console.log(response);
       localStorage.setItem(
    'token',
    response.data.token
  );

  localStorage.setItem(
    'email',
    response.data.email
  );

  localStorage.setItem(
    'role',
    response.data.role
  );
    },
    error: (error) => {
      console.log(error);
       console.log("Status:", error.status);
  console.log("Error Object:", error.error);

  this.errorMessage =
    error.error?.message ||
    error.error?.Message ||
    "Something went wrong.";
    console.log(this.errorMessage)
    }
  });
   

}
}
