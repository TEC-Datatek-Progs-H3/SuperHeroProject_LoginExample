import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Team } from '../_models/team';

@Injectable({
  providedIn: 'root'
})
export class TeamService {

  private apiUrl = environment.apiUrl + 'team';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Team[]> {
    return this.http.get<Team[]>(this.apiUrl);
  }
  getById(teamId: number): Observable<Team> {
    return this.http.get<Team>(`${this.apiUrl}/${teamId}`);
  }
  create(superHero: Team): Observable<Team> {
    return this.http.post<Team>(this.apiUrl, superHero);
  }
  update(superHero: Team): Observable<Team> {
    return this.http.put<Team>(`${this.apiUrl}/${superHero.id}`, superHero);
  }
  delete(teamId: number): Observable<Team> {
    return this.http.delete<Team>(`${this.apiUrl}/${teamId}`);
  }
}
