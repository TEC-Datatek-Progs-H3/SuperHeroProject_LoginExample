import { Team } from "./team";

export interface SuperHero {
  id: number;
  name: string;
  firstName: string;
  lastName: string;
  place: string;
  debutYear: number;
  teamId:number;
  team?: Team;
}
