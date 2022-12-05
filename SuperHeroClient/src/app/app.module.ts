import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { SuperHeroComponent } from './admin/super-hero/super-hero.component';
import { FormsModule } from '@angular/forms';
import { TeamComponent } from './admin/team/team.component';
import { CartDemoComponent } from './cart-demo/cart-demo.component';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './_helpers/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    SuperHeroComponent,
    TeamComponent,
    CartDemoComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true } // setup JwtInterceptor to act on all HTTP trafic
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
