import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/auth/register-request';
import { LoginRequest } from '../models/auth/login-request';
import { LoginResponse } from '../models/auth/login-response';
import { ApiResponse } from '../models/common/api-response';
import { CurrentUser } from '../models/auth/current-user';

@Injectable({
  providedIn: 'root'
})

export class Auth {

  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5103/api';

register(request: RegisterRequest): Observable<any> {
  return this.http.post<ApiResponse<string>>(
      `${this.apiUrl}/auth/register`,
      request,
      {
        withCredentials: true
      }
    );
  }

login(request: LoginRequest) {
  return this.http.post<ApiResponse<LoginResponse>>(
    `${this.apiUrl}/auth/login`,
    request,
    {
      withCredentials: true
    }
  );
}

getCurrentUser() {
  return this.http.get<ApiResponse<CurrentUser>>(
    `${this.apiUrl}/auth/me`,
    {
      withCredentials: true
    }
  );

}

isLoggedIn(): boolean {
  return false;
}

logout() {
  return this.http.post(
  `${this.apiUrl}/auth/logout`,
  {},
  {
    withCredentials: true
  });
}
}
