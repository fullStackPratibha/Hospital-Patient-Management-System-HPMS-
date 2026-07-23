import { Routes } from '@angular/router';
import { Register } from './components/auth/register/register';
import { Login } from './components/auth/login/login';

export const routes: Routes = [
  {
    path: '',
    redirectTo:'login',
    pathMatch:'full'
  },
  {
    path:'login',
    component:Login
  },
  {
    path:'register',
    component:Register
  }
];