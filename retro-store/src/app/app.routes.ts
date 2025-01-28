import { Routes } from '@angular/router';
import {
  CartPageComponent,
  HomePageComponent,
  LoginPageComponent,
  ProfilePageComponent,
} from '@project';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'profile', component: ProfilePageComponent },
  { path: 'cart', component: CartPageComponent },
];
