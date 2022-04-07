import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent {
  public products: Product[];
  public categories: Category[];
  productName: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.productName = params.get('productName');

      const headers = { 'Authorization': 'Bearer ' + sessionStorage.getItem("token") };
      this.http.get<Product[]>(this.baseUrl + 'api/product?productName=' + this.productName, { headers }).subscribe(result => {
        this.products = result;
      }, error => console.error(error));

      this.http.get<Category[]>(this.baseUrl + 'api/Category', { headers }).subscribe(result => {
        this.categories = result;
      }, error => console.error(error));
    });
  }
}

interface Product {
  name: string;
  detail: string;
  path: string;
}

interface Category {
  id: number;
  name: string;
  detail: string;
  value: number;
}
