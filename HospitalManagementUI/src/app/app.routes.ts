import { Routes } from '@angular/router';
import { PatientRegister } from './features/auth/patient-register/patient-register';
import { DoctorRegister } from './features/auth/doctor-register/doctor-register';
import { LoginComponent } from './features/auth/login/login';
import { LandingPage } from './features/landing/landing';


export const routes: Routes = [
  {
    path: '',
    redirectTo:'LandingPage',
    pathMatch:'full'
  },
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
  }
];