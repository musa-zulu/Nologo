import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatDialog, MatTable } from '@angular/material';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { SnackbarService } from 'src/app/services/snackbar.service';
import { DeleteRecipeComponent } from '../delete-recipe/delete-recipe.component';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-manage-recipes',
  templateUrl: './manage-recipes.component.html',
  styleUrls: ['./manage-recipes.component.scss']
})
export class ManageRecipesComponent implements OnInit, OnDestroy {

  displayedColumns: string[] = ['name', 'author', 'instructions', 'operation'];

  dataSource = new MatTableDataSource<Recipe>();
  pageLength: number = 0;
  recipes: Recipe[];
  filteredRecipes: Recipe[] = [];

  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  private unsubscribe$ = new Subject<void>();
  constructor(
    private recipeService: RecipeService,
    public dialog: MatDialog,
    private snackBarService: SnackbarService) {
  }

  ngOnInit() {
    this.getAllRecipeData();
  }

  getAllRecipeData() {
    this.recipeService.getAllRecipes()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((recipes: any) => {        
        this.recipes = recipes.data;
        this.onPageChanged(null);
      }, error => {
        console.log('Error ocurred while fetching recipe details : ', error);
      });
  }

  applyFilter(filterValue: any) {
    let searchTerm = filterValue.target.value.toLocaleLowerCase();
    const filteredUsers = searchTerm
      ? this.recipes.filter((p) => p.name.toLowerCase().includes(searchTerm))
      : this.recipes;

    this.initializeTable(filteredUsers);
  }

  deleteConfirm(id: string): void {
    const dialogRef = this.dialog.open(DeleteRecipeComponent, {
      data: id
    });

    dialogRef.afterClosed()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(result => {
        if (result === 1) {
          this.getAllRecipeData();
          this.snackBarService.showSnackBar('Data deleted successfully');
        } else {
          this.snackBarService.showSnackBar('Error occurred!! Try again');
        }
      });
  }

  refreshTable() {
    this.getAllRecipeData();
  }

  onPageChanged(e: { pageIndex: number; pageSize: number }): void {
    let filteredRecipes = [];
    if (e == null) {
      let firstCut = 0;
      let secondCut = firstCut + 5;
      filteredRecipes = this.recipes.slice(firstCut, secondCut);
    } else {
      let firstCut = e.pageIndex * e.pageSize;
      let secondCut = firstCut + e.pageSize;
      filteredRecipes = this.recipes.slice(firstCut, secondCut);
    }
    this.initializeTable(filteredRecipes);
  }

  private initializeTable(recipes: Recipe[]) {
    this.dataSource = new MatTableDataSource<Recipe>(recipes);
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
