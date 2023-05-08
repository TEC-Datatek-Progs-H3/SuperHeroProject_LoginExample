import { Team } from "./team";

export interface SuperHero {
  id: number;
  name: string;
  firstName: string;
  lastName: string;
  place: string;
  debutYear: number;
  teamId: number;
  team?: Team;
}

export function resetSuperHero() {
  return { id: 0, name: '', firstName: '', lastName: '', place: '', debutYear: 0, teamId: 0 };
}
