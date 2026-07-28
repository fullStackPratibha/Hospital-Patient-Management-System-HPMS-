import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/auth/register-request';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private http = inject(HttpClient);

   private apiUrl = 'http://localhost:5103/api';

  register(request: RegisterRequest): Observable<any> {

    return this.http.post<any>(
      `${this.apiUrl}/auth/register`,
      request
    );

  }
}