import { Routes } from '@angular/router';
import { PatientRegister } from './features/auth/patient-register/patient-register';
import { DoctorRegister } from './features/auth/doctor-register/doctor-register';
import { LoginComponent } from './features/auth/login/login';
import { LandingPage } from './features/landing/landing';
import { PatientLayout } from './features/patient/layouts/patient-layout/patient-layout';
import { Dashboard } from './features/patient/dashboard/dashboard';
import { Appointments } from './features/patient/appointments/appointments';
import { Prescriptions } from './features/patient/prescriptions/prescriptions';
import { MedicalHistory } from './features/patient/history/history';
import { Profile } from './features/patient/profile/profile';
import { authGuard } from './core/guards/auth-guard';
import { roleGuard } from './core/guards/role-guard';


export const routes: Routes = [
  {
    path:'',
    component:LandingPage
  },
   {
      path:'login',
      redirectTo:'login/patient',
      pathMatch:'full'
  },

  {
      path:'login/:role',
      component:LoginComponent
  },
  {
    path:'patient-register',
    component:PatientRegister
  },
  {
    path:'doctor-register',
    component:DoctorRegister
  },
  {
  path: 'patient',
  component: PatientLayout,
  canActivate: [authGuard, roleGuard(['patient'])],
  children: [
    {
      path: 'dashboard',
      component: Dashboard
    },
    {
      path: 'appointments',
      component: Appointments
    },
    {
      path: 'history',
      component: MedicalHistory
    },
    {
      path: 'prescriptions',
      component: Prescriptions
    },
    {
      path: 'profile',
      component: Profile
    },
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    }

  ]
}
];