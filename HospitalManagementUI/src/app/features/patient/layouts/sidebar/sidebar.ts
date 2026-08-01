import { Component,inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive,Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {
  private router = inject(Router);

  navItems = [
    { path: '/dashboard', label: 'Dashboard', icon: 'grid' },
    { path: '/appointments', label: 'Appointments', icon: 'calendar' },
    { path: '/prescriptions', label: 'Prescriptions', icon: 'file' },
    { path: '/medical-history', label: 'Medical History', icon: 'history' },
    { path: '/profile', label: 'Profile', icon: 'user' },
    { path: '/settings', label: 'Settings', icon: 'gear' }
  ];

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    localStorage.removeItem('role');
    this.router.navigate(['/login/patient']);
  }
}
