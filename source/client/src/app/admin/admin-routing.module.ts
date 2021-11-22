import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ManageRecipesComponent } from '../components/admin/manage-recipes/manage-recipes.component';
import { RecipeFormComponent } from '../components/admin/recipe-form/recipe-form.component';

const adminRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'new', component: RecipeFormComponent },
      { path: ':recipeId', component: RecipeFormComponent },
      { path: '', component: ManageRecipesComponent },
     
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(adminRoutes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
