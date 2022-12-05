import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartItem } from '../_models/cartItem';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  // this is the localStorage name where our basket is placed
  private basketName = "webshopProjectBasket";

  // this is the current cartItems stored in basket
  currentBasketSubject: BehaviorSubject<CartItem[]>;

  // this is the observable basket others can subscribe to
  currentBasket: Observable<CartItem[]>;

  constructor() {
    // when the constructor runs, we grab what is stored in localStorage,
    // or create an empty Array, if storage is empty
    this.currentBasketSubject = new BehaviorSubject<CartItem[]>(
      JSON.parse(localStorage.getItem(this.basketName) || "[]")
    );
    this.currentBasket = this.currentBasketSubject.asObservable();
  }

  get currentBasketValue(): CartItem[] {
    return this.currentBasketSubject.value;
  }

  saveBasket(basket: CartItem[]): void {
    localStorage.setItem(this.basketName, JSON.stringify(basket));
    this.currentBasketSubject.next(basket);
  }

  addToBasket(item: CartItem): void {
    let productFound = false;
    let basket = this.currentBasketValue;

    basket.forEach(basketItem => {
      if (basketItem.productId == item.productId) {
        basketItem.quantity += item.quantity;
        productFound = true;
        if (basketItem.quantity <= 0) {
          this.removeItemFromBasket(item.productId);
        }
      }
    });

    if (!productFound) {
      basket.push(item);
    }
    this.saveBasket(basket);
  }

  removeItemFromBasket(productId: number): void {
    let basket = this.currentBasketValue;
    for (let i = basket.length; i >= 0; i--) {
      if (basket[i].productId == productId) {
        basket.splice(i, 1);
      }
    }
    this.saveBasket(basket);
  }

  clearBasket(): void {
    let basket: CartItem[] = [];
    this.saveBasket(basket);
  }

  getBasketTotal(): number {
    let total: number = 0;
    this.currentBasketSubject.value.forEach(item => {
      total += item.price * item.quantity;
    });
    return total;
  }
}
