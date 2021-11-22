import { Component, OnInit, Inject } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-delete-recipe',
  templateUrl: './delete-recipe.component.html',
  styleUrls: ['./delete-recipe.component.scss']
})
export class DeleteRecipeComponent implements OnInit {

  public recipeData = new Recipe();

  constructor(
    public dialogRef: MatDialogRef<DeleteRecipeComponent>,
    @Inject(MAT_DIALOG_DATA) public recipeid: string,
    private recipeService: RecipeService) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  confirmDelete(): void {
    this.recipeService.deleteRecipe(this.recipeid).subscribe(
      () => {
      }, error => {
        console.log('Error ocurred while fetching recipe data : ', error);
      });
  }

  ngOnInit() {
    this.recipeService.getRecipeById(this.recipeid).subscribe(
      (result: any) => {
        this.recipeData = result.data;
      }, error => {
        console.log('Error ocurred while fetching recipe data : ', error);
      });
  }
}
