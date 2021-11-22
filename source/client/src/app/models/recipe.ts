export class Recipe {
  recipeId:string;
  name: string;
  author: string;
  recipeFileName: string;
  instructions: string;
  dateCreated: Date;
  ingredients: Ingredient[];
}

export class Ingredient {
  name: string;
}
