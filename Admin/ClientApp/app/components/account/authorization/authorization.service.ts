
import { Injectable, Inject } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/Rx';

import { RegistrationModel } from '../authorization/authorization.component'
import { SignInModel } from '../authorization/authorization.component'

@Injectable()
export class AuthorizationService {

	private headers = new Headers({ 'Content-Type': 'application/json' });

	constructor(private http: Http, @Inject('ORIGIN_URL') private originUrl: string) { }

	registration(model: RegistrationModel): Promise<any> {
		return this.http.post('/api/AccountAPI/Registration', JSON.stringify(model), { headers: this.headers })
			.toPromise()
			.then(res => res as any)
	}

	signIn(model: SignInModel): Promise<any> {
		return this.http.post('/api/AccountAPI/SignIn', JSON.stringify(model), { headers: this.headers })
			.map(response => response.json() as any)
			.toPromise()
	}

	signOut(): Promise<any> {
		return this.http.get('/api/AccountAPI/SignOut')
			.toPromise()
			.then(res => res as any)
	}

	private handleError(error: any): Promise<any> {
		console.error('An error occurred', error);
		return Promise.reject(error.message || error.json() as any);
	}
}