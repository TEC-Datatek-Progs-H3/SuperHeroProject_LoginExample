import { SuperHero } from './superHero';

export interface Team {
  id: number;
  name: string;
  members?: SuperHero[];
}
