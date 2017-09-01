
import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';
import { OrderModel } from "./order.component";

@Injectable()
export class OrderService {

	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	getOrder(orderID: number): Promise<any> {
		return this.http.get(this.originUrl + '/api/OrderAPI/GetOrder/' + orderID)
			.map(res => res.json() as any)
			.toPromise()
	}

	getOrderList(): Promise<any> {
		return this.http.get(this.originUrl + '/api/OrderAPI/GetOrderList')
			.map(res => res.json() as any)
			.toPromise()
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error.json() as any);
	}

}