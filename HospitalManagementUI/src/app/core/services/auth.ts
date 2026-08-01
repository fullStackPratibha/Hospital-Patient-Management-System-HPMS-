import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterRequest } from '../models/auth/register-request';
import { LoginRequest } from '../models/auth/login-request';
import { LoginResponse } from '../models/auth/login-response';
import { ApiResponse } from '../models/common/api-response';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"?: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"?: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?: string;
  exp?: number;

}

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

isLoggedIn(): boolean {
  const token = this.getToken();

  if(!token){
    return false;
  }
  
  return !this.isTokenExpired();
}


logout(): void {
  localStorage.removeItem('token');
  localStorage.removeItem('email');
  localStorage.removeItem('role');
}

getToken(): string | null {
  return localStorage.getItem('token');
}

getUserRole(): string | null {

  const decoded = this.getDecodedToken();

  const role =
    decoded?.[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ];

  return role
    ? role.toLowerCase()
    : null;

}

getUserEmail(): string | null {
  const decoded = this.getDecodedToken();
  if(!decoded){
    return null;
  }

  return decoded[
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
  ] ?? null;

}

getDecodedToken(): JwtPayload | null {

  const token = this.getToken();
  if(!token) {
    return null;
  }

  try {
    return jwtDecode<JwtPayload>(token);
  }
  catch(error) {
    return null;
  }

}

isTokenExpired(): boolean {
  const decoded = this.getDecodedToken();
  if(!decoded || !decoded.exp) {
    return true;
  }
  const expiryTime = decoded.exp * 1000;
  return Date.now() > expiryTime;

}

}