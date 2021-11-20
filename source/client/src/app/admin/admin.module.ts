import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin-routing.module';
import { NgMaterialModule } from '../ng-material/ng-material.module';
import { RecipeFormComponent } from '../components/admin/recipe-form/recipe-form.component';
import { ManageRecipesComponent } from '../components/admin/manage-recipes/manage-recipes.component';
import { DeleteRecipeComponent } from '../components/admin/delete-recipe/delete-recipe.component';

@NgModule({
  declarations: [
    RecipeFormComponent,
    ManageRecipesComponent,
    DeleteRecipeComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgMaterialModule
  ],
  entryComponents: [DeleteRecipeComponent]
})
export class AdminModule { }
