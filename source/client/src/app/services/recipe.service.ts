import { EventEmitter, Injectable, Output } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Recipe } from '../models/recipe';
import { ServerConfig } from './server-config';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  baseURL: string;
  constructor(private http: HttpClient, private _serverConfig: ServerConfig) {
    this.baseURL = this._serverConfig.getBaseUrl() + 'api/';
  }

  getAllRecipes() {
    return this.http.get(this.baseURL + 'recipe/get-all')
      .pipe(map(response => {
        return response;
      }));
  }

  addRecipe(recipeDto: Recipe) {
    return this.http.post(this.baseURL + 'recipe/create', recipeDto, this._serverConfig.getRequestOptions())
      .pipe(map(response => {
        return response;
      }));
  }

  getRecipeById(recipeId: string) {
    return this.http.get(this.baseURL +'recipe/get-by-id?recipeId=' + recipeId)
      .pipe(map(response => {
        return response;
      }));
  }

  updateRecipeDetails(recipe) {
    return this.http.put(this.baseURL + 'recipe/update', recipe)
      .pipe(map(response => {
        return response;
      }));
  }

  deleteRecipe(recipeId: string) {
    return this.http.delete(this.baseURL +'recipe/delete?recipeId='+ recipeId)
      .pipe(map(response => {
        return response;
      }));
  }

  uploadFile(formData) {
    this.http.post(this.baseURL + 'recipe/uploadFile', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.Response) {
          localStorage.removeItem('imagePath');
          localStorage.setItem('imagePath', JSON.stringify(event.body));
        }
      });
  }
}
