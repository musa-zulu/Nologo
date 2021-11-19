import { Ingredients } from "./ingredients";

export class Recipe {
  recipeId:number;
  name: string;
  author: string;
  recipeFileName: string;
  instructions: string;
  ingredients: Ingredients[];
}
