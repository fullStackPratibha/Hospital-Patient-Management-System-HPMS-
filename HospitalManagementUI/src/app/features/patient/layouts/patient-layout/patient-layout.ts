import { Component,inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Sidebar } from '../sidebar/sidebar';
import { PatientState } from '../../../../core/services/patient-state';

@Component({
  selector: 'app-patient-layout',
  standalone: true,
  imports: [
    CommonModule,
    Sidebar,
    RouterOutlet
  ],
  templateUrl: './patient-layout.html',
  styleUrl: './patient-layout.css'
})
export class PatientLayout {

  patientState = inject(PatientState);

  ngOnInit(){

  this.patientState
      .loadPatient()
      .subscribe({
        next:()=>{
          console.log(
            "Patient data loaded successfully"
          );
        },
        error:(err)=>{
          console.log(
            "Patient loading error",
            err
          );
        }
      });
}
}