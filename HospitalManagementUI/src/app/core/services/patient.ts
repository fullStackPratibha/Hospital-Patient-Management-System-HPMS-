import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PatientProfile } from '../models/patient-profile';


interface ApiResponse<T>{
  success:boolean;
  statusCode:number;
  message:string;
  data:T;
}


@Injectable({
  providedIn:'root'
})
export class Patient {
  private http = inject(HttpClient);

  private apiUrl = 
  'http://localhost:5103/api/patient';

  getMyProfile():Observable<ApiResponse<PatientProfile>>{
    console.log(
   "PATIENT ME API CALLED",
   new Error().stack
 );
    return this.http.get<ApiResponse<PatientProfile>>(
      `${this.apiUrl}/me`,
      {
        withCredentials:true
      }
    );
  }

}