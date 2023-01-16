import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Team } from 'src/app/_models/team';
import { TeamService } from 'src/app/_services/team.service';

@Component({
  selector: 'app-team',
  standalone:true,
  imports: [CommonModule, FormsModule],
  templateUrl: './team.component.html',
  styles: []
})
export class TeamComponent implements OnInit {
  message: string = '';
  teams: Team[] = [];
  team: Team = { id: 0, name: '' };

  constructor(private teamService: TeamService) { }

  ngOnInit(): void {
    this.teamService.getAll().subscribe(t => this.teams = t);
  }

  edit(team: Team): void {
    // copies values
    this.team = {
      id: team.id,
      name: team.name
    };
    // this.team = team;
    // this.team.teamId = team.team?.id || 0;
  }

  delete(team: Team): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.teamService.delete(team.id)
        .subscribe(() => {
          this.teams = this.teams.filter(x => x.id != team.id)
        });
    }
  }

  cancel(): void {
    this.message = '';
    this.team = { id: 0, name: '' };
  }

  save(): void {
    this.message = '';
    if (this.team.id == 0) {
      // create
      this.teamService.create(this.team)
        .subscribe({
          next: (x) => {
            this.teams.push(x);
            this.team = { id: 0, name: '' };
          },
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          }
        });
    } else {
      // update
      this.teamService.update(this.team)
        .subscribe({
          error: (err) => {
            this.message = Object.values(err.error.errors).join(', ');
          },
          complete: () => {
            this.teamService.getAll().subscribe(x => this.teams = x);
            this.team = { id: 0, name: '' };
          }
        });
    }
  }

}
