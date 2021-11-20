import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseURL: string;

  constructor(private http: HttpClient) {
    this.baseURL = '/api/recipe/';
  }

  getAllRecipes() {
    return this.http.get(this.baseURL)
      .pipe(map(response => {
        return response;
      }));
  }

  getIngredients() {
    return this.http.get(this.baseURL + 'GetIngredientsList')
      .pipe(map(response => {
        return response;
      }));
  }

  addRecipe(recipe) {
    return this.http.post(this.baseURL, recipe)
      .pipe(map(response => {
        return response;
      }));
  }

  getRecipeById(recipeId: number) {
    return this.http.get(this.baseURL + recipeId)
      .pipe(map(response => {
        return response;
      }));
  }

  getsimilarRecipes(recipeId: number) {
    return this.http.get(this.baseURL + 'GetSimilarRecipes/' + recipeId)
      .pipe(map(response => {
        return response;
      }));
  }

  updateRecipeDetails(recipe) {
    return this.http.put(this.baseURL, recipe)
      .pipe(map(response => {
        return response;
      }));
  }

  deleteRecipe(recipeId: number) {
    return this.http.delete(this.baseURL + recipeId)
      .pipe(map(response => {
        return response;
      }));
  }
}
