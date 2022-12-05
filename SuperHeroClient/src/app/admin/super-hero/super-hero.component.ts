import { Component, OnInit } from '@angular/core';
import { SuperHero } from 'src/app/_models/superHero';
import { Team } from 'src/app/_models/team';
import { SuperHeroService } from 'src/app/_services/super-hero.service';
import { TeamService } from 'src/app/_services/team.service';

@Component({
  selector: 'app-super-hero',
  templateUrl: './super-hero.component.html',
  styleUrls: ['./super-hero.component.css']
})
export class SuperHeroComponent implements OnInit {
  message: string = '';
  superHeroes: SuperHero[] = [];
  superHero: SuperHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 };
  teams: Team[] = [];

  constructor(private superHeroService: SuperHeroService, private teamService: TeamService) { }

  ngOnInit(): void {
    this.superHeroService.getAll()
      .subscribe(x => this.superHeroes = x);
    this.teamService.getAll()
      .subscribe(t => this.teams = t);
  }

  edit(superHero: SuperHero): void {
    // copies values
    this.superHero = {
      id: superHero.id,
      name: superHero.name,
      firstName: superHero.firstName,
      lastName: superHero.lastName,
      place: superHero.place,
      debutYear: superHero.debutYear,
      teamId: superHero.team?.id || 0
    };
    // this.superHero = superHero;
    // this.superHero.teamId = superHero.team?.id || 0;
  }

  delete(superHero: SuperHero): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.superHeroService.delete(superHero.id)
        .subscribe(() => {
          this.superHeroes = this.superHeroes.filter(x => x.id != superHero.id)
        });
    }
  }

  cancel(): void {
    this.message = '';
    this.superHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 }
  }

  save(): void {
    this.message = '';
    if (this.superHero.id == 0) {
      // create
      this.superHeroService.create(this.superHero)
        .subscribe({
          next: (x) => {
            this.superHeroes.push(x);
            this.superHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 };
          },
          error: (err) => {
            console.log(err);
            this.message = Object.values(err.error.errors).join(', ');
          }
        });
    } else {
      // update
      this.superHeroService.update(this.superHero)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          },
          complete: () => {
            this.superHeroService.getAll().subscribe(x => this.superHeroes = x);
            this.superHero = { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 };
          }
        });
    }
  }
}
