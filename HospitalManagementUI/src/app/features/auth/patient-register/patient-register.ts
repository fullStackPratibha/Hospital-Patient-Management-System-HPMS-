import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleTabsComponent } from '../shared/role-tabs';
import { RouterLink } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { RegisterRequest } from '../../../core/models/auth/register-request'
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-register-patient',
  standalone: true,
  imports: [CommonModule, RoleTabsComponent,RouterLink,ReactiveFormsModule],
  templateUrl: './patient-register.html',
  styleUrl: './patient-register.css'
})

export class PatientRegister {
  showPassword = signal(false);
  showConfirmPassword = signal(false);
  agreedToTerms = signal(false);

  togglePassword(): void {
    this.showPassword.update((v) => !v);
  }

  toggleConfirmPassword(): void {
    this.showConfirmPassword.update((v) => !v);
  }

  toggleAgree(): void {
    this.agreedToTerms.update((v) => !v);
  }

  errorMessage = signal('');
  private fb = inject(FormBuilder);
  private auth = inject(Auth);

  registerForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required,Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', [Validators.required]],
    phone: ['', [Validators.required,Validators.pattern(/^[0-9]{10}$/)]],
    gender: ['', [Validators.required]],
    dateOfBirth: ['', [Validators.required]],
    address: ['', [Validators.required]]
  });

  onSubmit(): void {

  this.errorMessage.set('');

  if (this.registerForm.invalid) {
    this.registerForm.markAllAsTouched();
    return;
  }

  const request = this.registerForm.getRawValue() as RegisterRequest;
  this.auth.register(request).subscribe({
    next: (response:any) => {
      alert("Registration Successful");
      console.log(response);
    },

    error: (error:any) => {
        this.errorMessage.set(error.error.Message);
    }

  });
}

  getError(controlName: string): string {

  const control = this.registerForm.get(controlName);

      if (!control || !control.touched) {
        return '';
      }

      if (control.hasError('required')) {
        return 'This field is required.';
      }

  if (control.hasError('email')) {
    return 'Please enter a valid email address.';
  }

  if (control.hasError('pattern') && controlName === 'phone') {
    return 'Phone number must contain 10 digits.';
  }

  if (control.hasError('minlength')) {
    return 'Minimum 8 characters are required.';
  }

  return '';
}

passwordsMatch(): boolean {

  return this.registerForm.value.password ===
         this.registerForm.value.confirmPassword;

}

}
