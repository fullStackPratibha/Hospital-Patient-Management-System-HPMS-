import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleTabsComponent } from '../shared/role-tabs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register-doctor',
  standalone: true,
  imports: [CommonModule, RoleTabsComponent,RouterLink],
  templateUrl: './doctor-register.html',
  styleUrl: './doctor-register.css'
})
export class DoctorRegister {
  showPassword = signal(false);
  showConfirmPassword = signal(false);
  agreedToTerms = signal(false);
  errorMessage = signal('Doactor registration feature will be available soon.');

  togglePassword(): void {
    this.showPassword.update((v) => !v);
  }

  toggleConfirmPassword(): void {
    this.showConfirmPassword.update((v) => !v);
  }

  toggleAgree(): void {
    this.agreedToTerms.update((v) => !v);
  }
  
}
