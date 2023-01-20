import { Component, OnInit } from '@angular/core';
import { SuperHero } from './_models/superHero';
import { SuperHeroService } from './_services/super-hero.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-frontpage',
  standalone:true,
  imports:[CommonModule, RouterModule],
  template:`
    <p>frontpage works!</p>
    <div *ngFor="let superHero of superHeroes">
      <h2><a [routerLink]="['/hero',superHero.id]">{{ superHero.name | uppercase }}</a></h2>
      <p>{{ superHero.firstName }} {{ superHero.lastName }}, {{ superHero.team?.name }}</p>
    </div>
  `,
  styles: []
})
export class FrontpageComponent implements OnInit {
  superHeroes:SuperHero[] = [];

  constructor(private superHeroService: SuperHeroService) { }

  ngOnInit(): void {
      this.superHeroService.getAll()
        .subscribe(x => this.superHeroes = x);
  }


}
