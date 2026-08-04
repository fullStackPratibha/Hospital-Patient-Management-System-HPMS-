import { Component,inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive,Router } from '@angular/router';
import { AuthState } from '../../../../core/services/auth-state';
import { Auth } from '../../../../core/services/auth';
import { PatientState } from '../../../../core/services/patient-state';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {
  private router = inject(Router);
  private authState = inject(AuthState);
  private auth = inject(Auth);
  private patientState = inject(PatientState);
  
  navItems = [
  { path: '/patient/dashboard', label: 'Dashboard', icon: 'grid' },
  { path: '/patient/appointments', label: 'Appointments', icon: 'calendar' },
  { path: '/patient/prescriptions', label: 'Prescriptions', icon: 'file' },
  { path: '/patient/history', label: 'Medical History', icon: 'history' },
  { path: '/patient/profile', label: 'Profile', icon: 'user' },
];

  logout(){
   this.auth.logout().subscribe({
      next:()=>{
         this.authState.setCurrentUser(null);
         this.patientState.clearPatient();
         this.router.navigate(['/login/patient']);
      }
   });
}

}
