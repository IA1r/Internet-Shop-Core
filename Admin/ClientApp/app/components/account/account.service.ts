import { Injectable, Inject, InjectionToken } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';
import { AuthModel } from "./authorization/authorization.component";

@Injectable()
export class AccountService {

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	isAuthenticated(): Promise<AuthModel> {
		return this.http.get(this.originUrl + '/api/AccountAPI/IsAuthenticated')
			.map(response => response.json() as AuthModel)
			.toPromise();
	}
}