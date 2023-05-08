import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role';

const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./frontpage.component')
      .then(_ => _.FrontpageComponent)
  },
  {
    path: 'hero/:heroId',
    loadComponent: () => import('./hero-detail.component')
      .then(_ => _.HeroDetailComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./login/login.component')
      .then(_ => _.LoginComponent)
  },
  {
    path: 'cartdemo',
    loadComponent: () => import('./cart-demo/cart-demo.component')
      .then(_ => _.CartDemoComponent)
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
