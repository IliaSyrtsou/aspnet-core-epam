import { Category } from './models/category';
import { ProductService } from './services/product.service';
import { Product } from './models/product';
import { Component, OnInit } from '@angular/core';
import { CategoryService } from './services/category.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public products: Product[];
  public categories: Category[];
  private defaultPageNumber = 1;
  private defaultPageSize = 10;

  constructor(
    private readonly productService: ProductService,
    private readonly categoryService: CategoryService
    ) {
  }

  public ngOnInit() {
    this.productService
      .getPagedProducts(this.defaultPageNumber, this.defaultPageSize)
      .toPromise()
      .then(products => {
        this.products = products;
      });

    this.categoryService
      .getPagedCategories(this.defaultPageNumber, this.defaultPageSize)
      .toPromise()
      .then(products => {
        this.categories = products;
      });
  }
}
