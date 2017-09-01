import { Component, Inject, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';


import { ProductService } from '../product/product.service'
import { ProductModel } from "../models/ProductModel";
import { ResponseStatusModel } from "../models/ResponseStatusModel";

@Component({
	selector: 'product',
	templateUrl: './product.component.html',
	providers: [ProductService],
	styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit {

	constructor(private productService: ProductService, private route: ActivatedRoute) { }

	public uploader: FileUploader;
	public product: ProductModel;
	private backupProduct: ProductModel;
	public IsEdit: boolean;
	public responseStatus: ResponseStatusModel;

	ngOnInit(): void {
		this.route.paramMap
			.switchMap((params: ParamMap) => this.productService.getProduct(+params.get('id')))
			.subscribe(response => {
				this.responseStatus = response.responseStatus;
				this.product = response.product;
			},
			error => {
				this.responseStatus = (error.json() as any).responseStatus
			});


	}

	updateProduct(product: ProductModel) {
		this.productService.updateProduct(product)
			.then(response => {
				if (response.responseStatus.success) {
					document.getElementById('successUpdate').hidden = false;
					document.getElementById('successUpdate').textContent = response.responseStatus.message;
					setTimeout(() => {
						document.getElementById('successUpdate').hidden = true;
					}, 3000);
				}
			});
	}

	cancelUpdate() {
		this.product = this.backupProduct;
	}

	setIsEdit(isEdit: boolean) {
		if (isEdit)
			this.uploader = new FileUploader({ url: '/api/ProductAPI/ImageUpload/' + this.product.productID });

		this.IsEdit = isEdit;
	}


	updateImage() {
		this.uploader.uploadAll();
		this.uploader.onCompleteItem = (response: any) => {
			this.product.characteristic.Image = JSON.parse(response._xhr.response).imageID;
		}
	}
}
