import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { Auth } from '../services/auth';


export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const auth = inject(Auth);
  const router = inject(Router);

  const token = auth.getToken();

  let request = req;

  if(token){

    request = req.clone({
      setHeaders:{
        Authorization:`Bearer ${token}`
      }

    });
  }

  return next(request).pipe(

    catchError(error => {

      if(error.status === 401){
        console.log(
          "Unauthorized - Logging out"
        );

        auth.logout();

        router.navigate([
          '/login/patient'
        ]);

      }
      return throwError(() => error);
    })
  );


};