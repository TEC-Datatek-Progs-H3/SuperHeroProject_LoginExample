import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SuperHeroComponent } from './admin/super-hero/super-hero.component';
import { TeamComponent } from './admin/team/team.component';
import { CartDemoComponent } from './cart-demo/cart-demo.component';
import { FrontpageComponent } from './frontpage.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./frontpage.component').then(_ => _.FrontpageComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./login/login.component').then(_ => _.LoginComponent)
  },
  {
    path: 'cartdemo',
    loadComponent: () => import('./cart-demo/cart-demo.component').then(_ => _.CartDemoComponent)
  },
  {
    path: 'admin/super-hero',
    loadComponent: () => import('./admin/super-hero/super-hero.component')
      .then(_ => _.SuperHeroComponent),
    canActivate: [AuthGuard], data: { roles: [Role.Admin] }
  },
  {
    path: 'admin/team',
    loadComponent: () => import('./admin/team/team.component')
      .then(_ => _.TeamComponent),
    canActivate: [AuthGuard], data: { roles: [Role.Admin] }
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
