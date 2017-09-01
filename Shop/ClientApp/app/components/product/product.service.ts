
import { Injectable, Inject, InjectionToken } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';

import { ProductModel } from '../product/products.component';
//import { ShoppingCartDataUserModel } from '../product/product.component'
import { ShoppingCartService } from "../shopping-cart/shopping-cart.service";

@Injectable()
export class ProductService {

	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string, private shoppingCartService: ShoppingCartService) { }

	getProducts(): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProducts')
			.map(response => response.json() as any)
			.toPromise();
	}

	getProductsByType(typeID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProducts/' + typeID)
			.map(response => response.json() as any)
			.toPromise();
	}

	getProduct(id: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProduct/' + id)
			.map(response => response.json() as any)
			.toPromise();
	}

	searchProducts(keyword: string): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/SearchProduct/' + keyword)
			.map(response => response.json() as any)
			.toPromise();
	}

	addProduct(productID: number): Promise<any> {
		return this.shoppingCartService.addProduct(productID);
	}

	
}