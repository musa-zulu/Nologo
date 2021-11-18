import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  //public object: Object[];
  public filteredProducts: Object[];
  category: string;

  constructor(private route: ActivatedRoute, /*private objectService: ObjectService*/) { }

  ngOnInit() {
    //this.getAllObjectData();
  }

  // getAllObjectData() {
  //   this.objectService.getAllObjects().pipe(switchMap(
  //     (data: Object[]) => {
  //       this.filteredProducts = data;
  //       return this.route.queryParams;
  //     }
  //   )).subscribe(params => {
  //     this.category = params['category'];
  //     this.object = (this.category) ?
  //       this.filteredProducts.filter(b => b.category.toLowerCase() === this.category.toLowerCase()) :
  //       this.filteredProducts;
  //   });
  // }
}
