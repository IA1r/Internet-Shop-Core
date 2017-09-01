import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';


import { ProductService } from '../product/product.service'
import { ProductModel } from "../models/ProductModel";

@Component({
	selector: 'products',
	templateUrl: './products.component.html',
	providers: [ProductService],
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

	searchPoducts(productID: number = null, keyword: string = null) {
		if (keyword != null)
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
		if (productID != null)
			this.productService.findProducts(productID)
				.then(response => {
					if (response.responseStatus.success) {
						this.searchedProducts = [];
						this.searchedProducts.push(response.product);
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

	cancel() {
		this.searchedProducts = null;
		document.getElementById('searchProduct').hidden = true;
		document.getElementById('noReulst').hidden = true;
	}

}