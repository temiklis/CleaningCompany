import { Material } from "../Materials/Material";

export interface ProductCard {
  Id: number;
  Name: string;
  Description: string;
  BasePrice: number;
  Difficulty: string;
  DifficultyId: number;
  Materials: Material[];
}
