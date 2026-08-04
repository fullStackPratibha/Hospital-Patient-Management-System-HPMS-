import { Component,inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientState } from '../../../core/services/patient-state';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile {
  patientState = inject(PatientState);

  patient =
 this.patientState.patient;

  vitals = {
    bloodGroup: 'A+',
    allergies: 'Penicillin',
    chronicConditions: ['Type 2 Diabetes', 'Hypertension']
  };

  personal = {
    dob: 'May 14, 1985 (39 years)',
    gender: 'Male',
    address: ['4521 Oakwood Avenue, Apartment 4B', 'San Francisco, CA 94110'],
    language: 'English (Native), Spanish'
  };

  emergencyContact = {
    name: 'Sarah Smith',
    relation: 'Spouse',
    phone: '+1 (555) 987-6543'
  };

  insurance = {
    provider: 'BlueCross Health',
    policyNumber: '#BC-9921-X90',
    status: 'Active & Verified'
  };

  lastUpdated = 'Today at 09:42 AM';
}
