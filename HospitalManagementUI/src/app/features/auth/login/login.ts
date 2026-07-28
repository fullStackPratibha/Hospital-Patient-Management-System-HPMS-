import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { map } from 'rxjs';

type Role = 'patient' | 'doctor' | 'admin';

interface RoleTab {
  key: Role;
  label: string;
}

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterLink],
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
}
