import { Injectable, Inject, InjectionToken } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';

import { ProductModel } from '../product/products.component';
import { ShoppingCartModel, OrderModel } from "./shopping-cart.component";

@Injectable()
export class ShoppingCartService {

	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	getShoppingCart(): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetShoppingCart')
			.map(response => response.json() as any)
			.toPromise();
	}

	addProduct(productID: number): Promise<any> {
		return this.http.post('/api/ProductAPI/AddProduct/' + productID, null, { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	deleteItem(productID: number): Promise<any> {
		return this.http.delete('/api/ProductAPI/DeleteItem/' + productID)
			.map(response => response.json() as any)
			.toPromise()
	}

	checkout(order: OrderModel): Promise<any> {
		return this.http.post('/api/ProductAPI/Checkout', JSON.stringify(order), { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error.json() as any);
	}
}