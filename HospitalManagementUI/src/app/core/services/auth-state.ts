import { inject, Injectable, signal } from '@angular/core';
import { CurrentUser } from '../models/auth/current-user';
import { Auth } from './auth';
import { catchError, EMPTY,tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthState {
    private auth = inject(Auth);

    private currentUserSignal =
    signal<CurrentUser | null>(null);

    readonly currentUser =
  this.currentUserSignal.asReadonly();

  setCurrentUser(
    user: CurrentUser | null
): void {
    this.currentUserSignal.set(user);
}


loadCurrentUser() {

  return this.auth.getCurrentUser()
    .pipe(
      tap(response=>{

        this.setCurrentUser(
          response.data
        );

      }),
      catchError(() => {
        this.setCurrentUser(null);
        return EMPTY;
      })
    );
}

    isLoggedIn(): boolean {
    return this.currentUser() !== null;
}

}