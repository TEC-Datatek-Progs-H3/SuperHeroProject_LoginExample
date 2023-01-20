import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { SuperHeroService } from './_services/super-hero.service';
import { SuperHero } from './_models/superHero';
import { CartService } from './_services/cart.service';
import { CartItem } from './_models/cartItem';

@Component({
  selector: 'app-hero-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <p>
      hero-detail works!
    </p>
    <div>
      <h2>{{ superHero.name | uppercase }}</h2>
      <p>{{ superHero.firstName }} {{ superHero.lastName }}, {{ superHero.team?.name }}</p>
    </div>
    <button (click)="addToCart()" class="btn btn-primary btn-sm">Put {{ superHero.name }} i kurven</button><br>
    <a routerLink="/">Forside</a>
  `,
  styles: [
  ]
})
export class HeroDetailComponent implements OnInit {
  superHero: SuperHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 };

  constructor(private superHeroService: SuperHeroService, private route: ActivatedRoute, private cartService: CartService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.superHeroService.getById(Number(params.get("heroId")))
        .subscribe(x => this.superHero = x);
    })
  }

  addToCart(item?: CartItem): void {

    // in a real situation the cartItem would be a product!
    if (item == null) {
      item = {
        productId: this.superHero.id,
        price: Math.random() * 999.99,
        quantity: 1,
        title: this.superHero.name
      } as CartItem;

    }

    this.cartService.addToBasket(item);
  }
}
