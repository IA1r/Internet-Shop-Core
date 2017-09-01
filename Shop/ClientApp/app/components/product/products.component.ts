import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';


import { ProductService } from '../product/product.service'
import { ShoppingCartService } from "../shopping-cart/shopping-cart.service";

@Component({
	selector: 'products',
	templateUrl: './products.component.html',
	providers: [ProductService, ShoppingCartService],
	styleUrls: ['../product/product.component.css']
})

export class ProductsComponent implements OnInit {
	constructor(private productService: ProductService) { }
	public products: ProductModel[];
	public searchedProducts: ProductModel[];
	public filterType: string;
	public p: number = 1;
	public ps: number = 1;

	ngOnInit() {
		this.getProducts();
	}

	searchPoducts(keyword: string) {
		this.productService.searchProducts(keyword)
			.then(response => {
				if (response.responseStatus.success) {
					this.searchedProducts = response.products
					document.getElementById('searchProduct').hidden = false;
				}
				else {
					this.searchedProducts = response.products;
					document.getElementById('searchProduct').hidden = false;
					document.getElementById('noReulst').hidden = false;
					document.getElementById('noReulst').textContent = response.responseStatus.message;
					setTimeout(() => {
						document.getElementById('searchProduct').hidden = true;
						document.getElementById('noReulst').hidden = true;
					}, 3000);	
				}
			})
	}

	getProducts() {
		this.productService.getProducts()
			.then(response => {
				this.filterType = "All Products";
				this.products = response.products;
			});
	}

	getProductsByType(typeID: number) {
		this.productService.getProductsByType(typeID)
			.then(response => {
				this.filterType = response.products[0].type;
				this.products = response.products;
			});
	}

	cancel() {
		this.searchedProducts = null;
		document.getElementById('searchProduct').hidden = true;
		document.getElementById('noReulst').hidden = true;
	}
}


export class ProductModel {
	productID: number;
	cartContentID: number;
	type: string;
	characteristic: { [key: string]: string };

}