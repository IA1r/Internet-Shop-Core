import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';


import { OrderModel } from "../models/OrderModel";
import { ProductTypeModel } from "../models/ProductTypeModel";
import { ProductModel } from "../models/ProductModel";

@Injectable()
export class ProductService {
	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	getProductTypes(): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProductTypes')
			.map(response => response.json() as any)
			.toPromise();
	}

	getProductsByType(typeID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProducts/' + typeID)
			.map(response => response.json() as any)
			.toPromise();
	}

	initDictionaryFields(typeID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/InitDictionaryFields/' + typeID)
			.map(response => response.json() as any)
			.toPromise();
	}

	addProductToDB(model: ProductModel): Promise<any> {
		return this.http.post('/api/ProductAPI/AddProductToDB', JSON.stringify(model), { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	getProducts() {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProducts')
			.map(response => response.json() as any)
			.toPromise();
	}

	getProduct(id: number) {
		return this.http.get(this.originUrl + '/api/ProductAPI/GetProduct/' + id)
			.map(response => response.json() as any)
			.toPromise();
	}

	updateProduct(model: ProductModel): Promise<any> {
		return this.http.post('/api/ProductAPI/UpdateProduct', JSON.stringify(model), { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	searchProducts(keyword: string): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/SearchProduct/' + keyword)
			.map(response => response.json() as any)
			.toPromise();
	}

	findProducts(productID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/ProductAPI/FindProduct/' + productID)
			.map(response => response.json() as any)
			.toPromise();
	}
}