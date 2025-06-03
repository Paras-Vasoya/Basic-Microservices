import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigService } from '../helper/common/config.service';
import { map, Observable } from 'rxjs';
import { ResponseModel } from '../helper/common/models/responseModel';
import { LoginInputModel } from '../helper/common/models/loginInput';
import { SocialLoginInputModel } from '../helper/common/models/socilLoginInput';

@Injectable({
  providedIn: 'root'
})
export class ServiceProxyService {
  productBaseUrl: string = '';
  userBaseUrl: string = '';

  constructor(private http: HttpClient, private configService: ConfigService) {
    this.productBaseUrl = configService.getProductApiURI();
    this.userBaseUrl = configService.getUserApiURI();
  }

  login(input: LoginInputModel): Observable<ResponseModel> {
    return this.http.post(this.userBaseUrl + 'User/login', input).pipe(map((response: any) => {
      return response;
    }));
  }

  register(input: LoginInputModel): Observable<ResponseModel> {
    return this.http.post(this.userBaseUrl + 'User/register', input).pipe(map((response: any) => {
      return response;
    }));
  }
  loginWithGoogle(input: SocialLoginInputModel): Observable<ResponseModel> {
    return this.http.post(this.userBaseUrl + 'User/login/google', input).pipe(map((response: any) => {
          return response;
        }));
  }

  loginWithFacebook(input: SocialLoginInputModel): Observable<ResponseModel> {
    return this.http.post(this.userBaseUrl + 'User/login/facebook', input).pipe(map((response: any) => {
          return response;
        })
      );
  }

  get(): Observable<any[]> {
    return this.http.get(this.userBaseUrl + 'WeatherForecast').pipe(map((response: any) => {
        return response;
      })
    );
  }

  getProductList(): Observable<ResponseModel> {
    return this.http.get(this.productBaseUrl + 'Product/GetAll').pipe(map((response: any) => {
          return response;
        })
      );
  }

  userLoggedIn(): boolean {
    return localStorage.getItem('token') != null;
  }
}
