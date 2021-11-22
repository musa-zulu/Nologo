import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public recipes: Recipe[];

  constructor(private _route: ActivatedRoute, private _recipeService: RecipeService) { }

  ngOnInit() {
    this.getAllRecipeData();
  }

  getAllRecipeData() {
    this._recipeService.getAllRecipes().pipe(switchMap(
      (data: any) => {
        this.recipes = data.data;
        return this._route.queryParams;
      }
    )).subscribe(params => {
      console.log(params);
    });
  }
}
