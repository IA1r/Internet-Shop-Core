import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, ParamMap } from '@angular/router';


import { ProductService } from '../product/product.service'
import { ProductModel } from "./products.component";
import { AuthModel } from "../account/authorization/authorization.component";
import { ResponseStatusModel } from "../model/ResponseStatusModel";
import { ShoppingCartService } from "../shopping-cart/shopping-cart.service";

@Component({
	selector: 'product',
	templateUrl: './product.component.html',
	providers: [ProductService, ShoppingCartService],
	styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit {

	constructor(private productService: ProductService, private route: ActivatedRoute) { }

	public product: ProductModel;
	public responseStatus: ResponseStatusModel;

	ngOnInit(): void {
		this.route.paramMap
			.switchMap((params: ParamMap) => this.productService.getProduct(+params.get('id')))
			.subscribe(
			response => {
				this.responseStatus = response.responseStatus;
				this.product = response.product;
			},
			error => {
				this.responseStatus = (error.json() as any).responseStatus
			});
	}

	handleError(error: any): Promise<any> {
		return error.json as any;
	}

	addProduct(): void {
		this.productService.addProduct(this.product.productID)
			.then(response => {
				document.getElementById('successAddToCart').hidden = false;
				document.getElementById('successAddToCart').textContent = response.responseStatus.message;
				setTimeout(() => {
					document.getElementById('successAddToCart').hidden = true;
				}, 5000);
			});
	}
}

export class ShoppingCartDataUserModel {
	public UserName: string;
	public Phone: string;
}

