import { SuperHero } from './superHero';

export interface Team {
  id: number;
  name: string;
  members?: SuperHero[];
}









export function resetTeam() {
  return { id: 0, name: '', members: [] };
}
