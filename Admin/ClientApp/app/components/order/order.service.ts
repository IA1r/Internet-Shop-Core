import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';

import { OrderModel } from "../models/OrderModel";

@Injectable()
export class OrderService {

	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	getOrder(orderID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/OrderAPI/GetOrder/' + orderID)
			.map(response => response.json() as any)
			.toPromise()
	}

	getOrderList(): Promise<any> {
		return this.http.get(this.originUrl + '/api/OrderAPI/GetOrderList')
			.map(response => response.json() as any)
			.toPromise()
	}

	confirmOrder(orderID: number): Promise<any> {
		return this.http.post('/api/OrderAPI/ConfirmOrder/' + orderID, null, { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	deleteOrder(orderID: number): Promise<any> {
		return this.http.post('/api/OrderAPI/DeleteOrder/' + orderID, null, { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error as any);
	}

}