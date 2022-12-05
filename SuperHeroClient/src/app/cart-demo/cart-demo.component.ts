import { Component, OnInit } from '@angular/core';
import { randomString } from '../_helpers/tools'; // only used for demo!
import { CartItem } from '../_models/cartItem';
import { CartService } from '../_services/cart.service';

@Component({
  selector: 'app-cart-demo',
  templateUrl: './cart-demo.component.html',
  styleUrls: ['./cart-demo.component.css']
})
export class CartDemoComponent implements OnInit {
  cartItems: CartItem[] = [];
  amount: number = 1;
  constructor(public cartService: CartService) { }

  ngOnInit(): void {
    this.amount = Math.floor(Math.random() * 10) + 1; // only for demo!
    this.cartService.currentBasket.subscribe(x => this.cartItems = x);
  }

  // OBS! this method belongs on productpage and other places where items can be placed in basket
  addToCart(item?: CartItem): void {

    if (item == null) {
      // create a random product only for demo!
      item = {
        productId: Math.floor(Math.random() * 1000000),
        price: Math.random() * 999.99,
        quantity: this.amount,
        title: randomString(12)
      } as CartItem;
      this.amount = Math.floor(Math.random() * 10) + 1;
    }

    this.cartService.addToBasket(item);
  }

  clearCart(): void {
    this.cartService.clearBasket();
  }

  updateCart(): void {
    this.cartService.saveBasket(this.cartItems);
  }

  removeItem(item: CartItem): void {
    if (confirm(`Er du sikker på du vil fjerne ${item.productId} ${item.title}?`)) {
      this.cartItems = this.cartItems.filter(x => x.productId != item.productId);
      this.cartService.saveBasket(this.cartItems);
    }
  }
}
