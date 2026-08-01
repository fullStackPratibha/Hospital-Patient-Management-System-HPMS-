import { Component,inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive,Router } from '@angular/router';
import { Auth } from '../../../../core/services/auth';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {
  private router = inject(Router);
  private auth = inject(Auth);

  navItems = [
  { path: '/patient/dashboard', label: 'Dashboard', icon: 'grid' },
  { path: '/patient/appointments', label: 'Appointments', icon: 'calendar' },
  { path: '/patient/prescriptions', label: 'Prescriptions', icon: 'file' },
  { path: '/patient/history', label: 'Medical History', icon: 'history' },
  { path: '/patient/profile', label: 'Profile', icon: 'user' },
];

  logout(): void {

   this.auth.logout();

  console.log(
    "Logout Status:",
    this.auth.isLoggedIn()
  );

    this.router.navigate(['/login/patient']);
  }
}
