import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Recipe } from 'src/app/models/recipe';

@Component({
  selector: 'app-recipe-card',
  templateUrl: './recipe-card.component.html',
  styleUrls: ['./recipe-card.component.scss']
})
export class RecipeCardComponent {

  @Input('recipe') recipe: Recipe;

  isActive = false;

  constructor(private router: Router) { }

  goToPage(id: number) {
    this.router.navigate(['/recipes/details/', id]);
  }
}
