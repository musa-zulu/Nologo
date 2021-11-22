import { Component, OnInit, OnDestroy } from '@angular/core';
import { Ingredient, Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Subject, Observable, combineLatest } from 'rxjs';
import { takeUntil, map } from 'rxjs/operators';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.scss']
})
export class RecipeDetailsComponent implements OnInit, OnDestroy {

  public recipe: Recipe;
  recipeId;
  ingredients: Ingredient[] = [];
  private unsubscribe$ = new Subject<void>();

  constructor(private recipeService: RecipeService, private route: ActivatedRoute, private router: Router) {
    this.recipeId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.route.queryParams.subscribe(param => {
      this.route.params.subscribe(
        params => {
        //   this.recipeId = +params['id'];
          this.getRecipeDetails();
        }
      );
    });
  }

  getRecipeDetails() {
    let savedIngredients: Ingredient[] = [];
    this.recipeService.getRecipeById(this.recipeId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        (result: any) => {
          this.recipe = result.data;
          if (result.data !== null && result.data !== undefined) {
            var values = result.data.ingredients.split(',');
            
            if (values.length > 0) {
              values.forEach(function (value) {
                savedIngredients.push({ name: value.trim() });
              });

            }
          }
          this.ingredients = savedIngredients;
        }, error => {
          console.log('Error ocurred while fetching recipe data : ', error);
        });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
