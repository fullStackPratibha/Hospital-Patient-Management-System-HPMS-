import { Injectable, inject, signal } from '@angular/core';
import { Patient } from './patient';
import { PatientProfile } from '../models/patient-profile';
import { tap,catchError,throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PatientState {

  private patientService = inject(Patient);


    private patientSignal =
    signal<PatientProfile | null>(null);


    readonly patient =
    this.patientSignal.asReadonly();

   private loadingSignal = signal<boolean>(false);

readonly loading =
 this.loadingSignal.asReadonly();



private errorSignal = signal<string | null>(null);

readonly error =
 this.errorSignal.asReadonly();


  loadPatient(){

 this.loadingSignal.set(true);

 this.errorSignal.set(null);


 return this.patientService
 .getMyProfile()
 .pipe(

 tap(response=>{

   this.patientSignal.set(
      response.data
   );

   this.loadingSignal.set(false);

 }),


 catchError(error=>{

   this.loadingSignal.set(false);

   this.errorSignal.set(
      "Failed to load patient data"
   );


   return throwError(()=>error);

 })

 );

}


  clearPatient(){
    this.patientSignal.set(null);
  }

}