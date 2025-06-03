import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {
  _productApiURI: string;
  _userApiURI: string;

  constructor() {
    // Local
    this._productApiURI = 'https://localhost:44357/api/';
    this._userApiURI = 'https://localhost:7261/api/';

    // IIS
    // this._productApiURI = 'http://localhost:8080/api/';
    // this._userApiURI = 'http://localhost:8081/api/';
  }

  getProductApiURI() {
    return this._productApiURI;
  }

  getUserApiURI() {
    return this._userApiURI;
  }
}
