import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/auth/register-request';
import { LoginRequest } from '../models/auth/login-request';
import { LoginResponse } from '../models/auth/login-response';
import { ApiResponse } from '../models/common/api-response';

@Injectable({
  providedIn: 'root'
})
export class Auth {

  private http = inject(HttpClient);

   private apiUrl = 'http://localhost:5103/api';

  register(request: RegisterRequest): Observable<any> {

    return this.http.post<ApiResponse<string>>(
      `${this.apiUrl}/auth/register`,
      request
    );

  }

  login(request: LoginRequest) {
  return this.http.post<ApiResponse<LoginResponse>>(
    `${this.apiUrl}/auth/login`,
    request
  );
}
}