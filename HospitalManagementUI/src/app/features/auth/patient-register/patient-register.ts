import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { RegisterRequest } from '../../../core/models/auth/register-request'
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-register',
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './patient-register.html',
  styleUrl: './patient-register.css',
})

export class PatientRegister {

  errorMessage = '';
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

 

  onSubmit():void{
    alert("Submitted")
    const request = this.registerForm.getRawValue() as RegisterRequest;

    this.auth.register(request).subscribe({
      next: (response:any) => {
        console.log("Registration Success");
        console.log(response);
      },

      error: (error:any) => {
        if (error.status === 409) {
          console.log(error);

          console.log(error.error);

          console.log(error.error.Message);
          this.errorMessage = error.error.Message;   
        }

      }
    });
  }
}
