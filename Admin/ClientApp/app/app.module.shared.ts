import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ModalModule, BsDropdownModule } from 'ngx-bootstrap';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FilterPipeModule } from 'ngx-filter-pipe';
import { FileSelectDirective } from 'ng2-file-upload';
import { FileUploadModule } from 'ng2-file-upload';
import { CustomFormsModule } from 'ng2-validation';
import { NgxPaginationModule } from 'ngx-pagination';


import { AppComponent } from './components/app/app.component'
import { AuthorizationComponent } from './components/account/authorization/authorization.component'
import { AuthorizationService } from './components/account/authorization/authorization.service';
import { AccountService } from "./components/account/account.service";
import { OrderComponent, SortOrdersPipe, FilterOrdersPipe } from "./components/order/order.component";
import { OrderService } from "./components/order/order.service";
import { ProductService } from "./components/product/product.service";
import { ProductComponent } from "./components/product/product.component";
import { ProductsComponent } from "./components/product/products.component";
import { PCGameComponent } from "./components/product/pc-game.component";
import { MangaComponent } from "./components/product/manga.component";
import { AddProductComponent } from "./components/product/add-product/addproduct.component";
import { AddPCGameComponent } from "./components/product/add-product/add-pc-game.component";
import { AddMangaComponent } from "./components/product/add-product/add-manga.component";
import { AddTShirtComponent } from "./components/product/add-product/add-t-shirt.component";
import { AddSSDComponent } from "./components/product/add-product/add-ssd.component";
import { AddRAMComponent } from "./components/product/add-product/add-ram.component";
import { TShirtComponent } from "./components/product/t-shirt.component";
import { SSDComponent } from "./components/product/ssd.component";
import { RAMComponent } from "./components/product/ram.component";

export const sharedConfig: NgModule = {
	bootstrap: [AppComponent],
	declarations: [
		AppComponent,
		AuthorizationComponent,
		OrderComponent,
		AddProductComponent,
		AddPCGameComponent,
		ProductComponent,
		ProductsComponent,
		AddMangaComponent,
		PCGameComponent,
		MangaComponent,
		TShirtComponent,
		SSDComponent,
		RAMComponent,
		AddTShirtComponent,
		AddSSDComponent,
		AddRAMComponent,
		FileSelectDirective,
		SortOrdersPipe,
		FilterOrdersPipe
	],
	imports: [
		FormsModule,
		CustomFormsModule,
		BrowserModule,
		HttpModule,
		FilterPipeModule,
		NgxPaginationModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'products', pathMatch: 'full' },
			{ path: 'product/:id', component: ProductComponent },
			{ path: 'products', component: ProductsComponent },
			{ path: 'orders', component: OrderComponent },
			{ path: 'add-product', component: AddProductComponent }
		]),
		ModalModule.forRoot(),
		BsDropdownModule.forRoot()
	],
	providers: [AuthorizationService, AccountService, OrderService, ProductService]
};
