import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthState } from './core/services/auth-state';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
   private authState = inject(AuthState);

   constructor() {
    this.authState
      .loadCurrentUser()
      .subscribe(response => {
          this.authState.setCurrentUser(response.data);
      });
  }
}