import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './forgot-password.html',
  styleUrl: './forgot-password.css'
})
export class ForgotPassword {
  email = signal('');
  newPassword = signal('');
  confirmPassword = signal('');

  errorMessage = signal('');
  successMessage = signal('');

  goBack() {
    // Hook this up to Router / Location as needed
    window.history.back();
  }

  onSubmit() {
    this.errorMessage.set('');
    this.successMessage.set('');

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!this.email() || !emailPattern.test(this.email())) {
      this.errorMessage.set('Please enter a valid email address.');
      return;
    }

    if (this.newPassword().length < 8) {
      this.errorMessage.set('Password must be at least 8 characters.');
      return;
    }

    if (this.newPassword() !== this.confirmPassword()) {
      this.errorMessage.set('Passwords do not match.');
      return;
    }

    // TODO: replace with actual reset-password API call
    this.successMessage.set('Password reset successfully!');
  }
}
