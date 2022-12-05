import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SuperHeroComponent } from './admin/super-hero/super-hero.component';
import { TeamComponent } from './admin/team/team.component';
import { CartDemoComponent } from './cart-demo/cart-demo.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_helpers/auth.guard';
import { Role } from './_models/role';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'cartdemo', component: CartDemoComponent },
  { path: 'admin/super-hero', component: SuperHeroComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: 'admin/team', component: TeamComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
