import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CartItem } from './_models/cartItem';
import { User } from './_models/user';
import { AuthService } from './_services/auth.service';
import { CartService } from './_services/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SuperHeroClient';
  basket: CartItem[] = [];
  currentUser: User = { id: 0, email: '', username: '' };

  constructor(
    private cartService: CartService,
    private router: Router,
    private authService: AuthService) {
    // get the current user from auth service
    this.authService.currentUser.subscribe(x => this.currentUser = x);
  }

  ngOnInit(): void {
    console.log("test");
    this.cartService.currentBasket.subscribe(x => this.basket = x);
  }
  logout() {
    if (confirm('Er du sikker på du vil logge ud')) {
      // ask auth service to perform logout
      this.authService.logout();

      // subscribe to the changes in currentUser, and load Home component
      this.authService.currentUser.subscribe(x => {
        this.currentUser = x
        this.router.navigate(['/']);
      });
    }
  }
}
