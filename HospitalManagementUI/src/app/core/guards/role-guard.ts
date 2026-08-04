import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';


export const roleGuard = (allowedRoles: string[]): CanActivateFn => {

  return () => {
    
    const auth = inject(Auth);
    const router = inject(Router);
    

    const userRole = localStorage
      .getItem('role')
      ?.toLowerCase();

    if(userRole && allowedRoles.includes(userRole)) {
      return true;
    }

    router.navigate(['/login/patient']);

    return false;

  };

};