import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    // redirect to home if already logged in
    if (this.authService.currentUserValue != null && this.authService.currentUserValue.id > 0) {
      this.router.navigate(['/']);
    }
  }

  login(): void {
    this.error = '';
    this.authService.login(this.email, this.password)
      .subscribe({
        next: () => {
          // get return url from route parameters or default to '/'
          let returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigate([returnUrl]);
        },
        error: err => {
          if (err.error?.status == 400 || err.error?.status == 401 || err.error?.status == 500) {
            this.error = 'Forkert brugernavn eller kodeord';
          }
          else {
            this.error = err.error.title;
          }
        }
      });
  }
}
