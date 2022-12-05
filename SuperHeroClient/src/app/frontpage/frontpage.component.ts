import { Component, OnInit } from '@angular/core';
import { SuperHero } from '../_models/superHero';
import { CartService } from '../_services/cart.service';
import { SuperHeroService } from '../_services/super-hero.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {
  superHeroes:SuperHero[] = [];

  constructor(private superHeroService: SuperHeroService) { }

  ngOnInit(): void {
      this.superHeroService.getAll()
        .subscribe(x => this.superHeroes = x);
  }


}
