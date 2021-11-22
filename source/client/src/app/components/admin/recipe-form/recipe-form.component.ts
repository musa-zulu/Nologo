import { Component, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Ingredient, Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/models/user';

import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.scss']
})
export class RecipeFormComponent implements OnInit, OnDestroy {

  files;
  recipeId;
  recipeImagePath;
  formTitle = 'Add';
  ingredientsList: [];
  recipeForm: FormGroup;
  recipe: Recipe = new Recipe();
  private formData = new FormData();
  private unsubscribe$ = new Subject<void>();

  visible = true;
  addOnBlur = true;
  selectable = true;
  removable = true;
  ingredients: Ingredient[] = [];
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(
    private recipeService: RecipeService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router) {

    this.recipeForm = this.fb.group({
      recipeId: 0,
      name: [''],
      ingredients: [''],
      instructions: ['', Validators.required]
    });

    if (this.route.snapshot.params['recipeId']) {
      this.recipeId = this.route.snapshot.paramMap.get('recipeId');
    }
  }

  get name() {
    return this.recipeForm.get('name');
  }

  get author() {
    return this.recipeForm.get('author');
  }

  get instructions() {
    return this.recipeForm.get('instructions');
  }

  ngOnInit() {
    let savedIngredients: Ingredient[] = [];
    if (this.recipeId) {
      this.formTitle = 'Edit';
      this.recipeService.getRecipeById(this.recipeId)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          (result: any) => {
            if (result.data !== null && result.data !== undefined) {
              var values = result.data.ingredients.split(',');
              
              if (values.length > 0) {
                values.forEach(function (value) {
                  savedIngredients.push({ name: value.trim() });
                });

              }
            }
            this.ingredients = savedIngredients;
            this.setRecipeFormData(result.data);
          }, error => {
            console.log('Error ocurred while fetching recipe data : ', error);
          });
    }
  }

  saveRecipeData() {
    if (!this.recipeForm.valid) {
      return;
    }
    if (this.files && this.files.length > 0) {
      for (let j = 0; j < this.files.length; j++) {
        this.formData.append('file' + j, this.files[j]);
      }
    }
    this.formData.append('recipeFormData', JSON.stringify(this.recipeForm.value));
    if (this.recipeId) {
      var recipe = this.getRecipeData();

      this.recipeService.updateRecipeDetails(recipe)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          () => {
            this.router.navigate(['/admin/recipes']);
          }, error => {
            console.log('Error ocurred while updating recipe data : ', error);
          });
    } else {
      var recipe = this.getRecipeData();

      this.recipeService.addRecipe(recipe)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          () => {
            this.router.navigate(['/admin/recipes']);
          }, error => {
            this.recipeForm.reset();
            console.log('Error ocurred while adding recipe data : ', error);
          });
    }
  }

  setUserDetails(userDetails: User) {
    const decodeUserDetails = JSON.parse(atob(localStorage.getItem('authToken').split('.')[1]));

    userDetails.userId = decodeUserDetails.userid;
    userDetails.email = decodeUserDetails.sub;
    userDetails.role = Number(decodeUserDetails.role);
  }

  getRecipeData() {
    const userDetails = new User();
    this.setUserDetails(userDetails);

    var recipe = new Recipe();
    recipe.name = this.name.value;
    recipe.instructions = this.instructions.value;
    recipe.author = userDetails.email;
    recipe.recipeId = this.recipeId;
    recipe.ingredients = this.ingredients;
    var imagePath = JSON.parse(localStorage.getItem("imagePath"));

    if (imagePath !== null) {
      recipe.recipeFileName = environment.baseUrl + imagePath["dbPath"];
      localStorage.removeItem("imagePath");
    } else {
      localStorage.removeItem("imagePath");
    }
    return recipe;
  }

  cancel() {
    this.router.navigate(['/admin/recipes']);
  }

  setRecipeFormData(recipeFormData) {
    this.recipeForm.setValue({
      recipeId: recipeFormData.recipeId,
      name: recipeFormData.name,
      instructions: recipeFormData.instructions,
      ingredients: recipeFormData.ingredients
    });
    this.recipeImagePath = recipeFormData.recipeFileName;
  }

  uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    const fileToUpload = files[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.recipeImagePath = (myevent.target as FileReader).result;
    };

    this.recipeService.uploadFile(formData);
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      this.ingredients.push({ name: value.trim() });
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  remove(ingredient: Ingredient): void {
    const index = this.ingredients.indexOf(ingredient);

    if (index >= 0) {
      this.ingredients.splice(index, 1);
    }
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
