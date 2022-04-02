import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent {
  public products: Product[];
  productName: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.productName = params.get('productName');

      this.http.get<Product[]>(this.baseUrl + 'api/product?productName=' + this.productName).subscribe(result => {
        this.products = result;
      }, error => console.error(error));
    });
  }
}

interface Product {
  name: string;
  detail: string;
  category: number;
  path: string;
}
