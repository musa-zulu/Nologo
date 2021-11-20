import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.scss']
})
export class RecipeFormComponent implements OnInit, OnDestroy {

  private formData = new FormData();
  recipeForm: FormGroup;
  recipe: Recipe = new Recipe();
  formTitle = 'Add';
  recipeImagePath;
  recipeId;
  files;
  categoryList: [];
  private unsubscribe$ = new Subject<void>();

  constructor(
    private recipeService: RecipeService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router) {

    this.recipeForm = this.fb.group({
      recipeId: 0,
      name: [''],
      author: ['', Validators.required],
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

  ngOnInit() { }

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
      this.recipeService.updateRecipeDetails(this.formData)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          () => {
            this.router.navigate(['/admin/recipes']);
          }, error => {
            console.log('Error ocurred while updating recipe data : ', error);
          });
    } else {
      this.recipeService.addRecipe(this.formData)
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe(
          () => {
            this.router.navigate(['/admin/recipes']);
          }, error => {
            // reset form and show a toaster
            this.recipeForm.reset();
            console.log('Error ocurred while adding recipe data : ', error);
          });
    }
  }

  cancel() {
    this.router.navigate(['/admin/recipes']);
  }

  setRecipeFormData(recipeFormData) {
    this.recipeForm.setValue({
      recipeId: recipeFormData.recipeId,
      title: recipeFormData.title,
      author: recipeFormData.author,
      category: recipeFormData.category,
      price: recipeFormData.price
    });
    this.recipeImagePath = '/Upload/' + recipeFormData.recipeFileName;
  }

  uploadImage(event) {
    this.files = event.target.files;
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.recipeImagePath = (myevent.target as FileReader).result;
    };
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
