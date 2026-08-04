import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthState } from '../services/auth-state';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {

  const authState = inject(AuthState);
  const router = inject(Router);

  if(authState.isLoggedIn()) {
    return true;
  }

  return authState.loadCurrentUser()
.pipe(

map(()=>{

if(authState.isLoggedIn()){
 return true;
}

router.navigate([
 '/login/patient'
]);

return false;

})
);

};