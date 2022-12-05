import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SuperHero } from '../_models/superHero';

@Injectable({
  providedIn: 'root'
})
export class SuperHeroService {
  private apiUrl = environment.apiUrl + 'superHero';

  constructor(private http: HttpClient) { }

  getAll(): Observable<SuperHero[]> {
    return this.http.get<SuperHero[]>(this.apiUrl);
  }
  getById(superHeroId: number): Observable<SuperHero> {
    return this.http.get<SuperHero>(`${this.apiUrl}/${superHeroId}`);
  }
  create(superHero: SuperHero): Observable<SuperHero> {
    return this.http.post<SuperHero>(this.apiUrl, superHero);
  }
  update(superHero: SuperHero): Observable<SuperHero> {
    return this.http.put<SuperHero>(`${this.apiUrl}/${superHero.id}`, superHero);
  }
  delete(superHeroId: number): Observable<SuperHero> {
    return this.http.delete<SuperHero>(`${this.apiUrl}/${superHeroId}`);
  }
}
