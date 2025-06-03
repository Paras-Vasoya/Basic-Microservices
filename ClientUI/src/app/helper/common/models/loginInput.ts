export interface ILoginInputModel {
    username: string;
    password: string;
  }
  
  export class LoginInputModel implements ILoginInputModel {
    username!: string;
    password!: string;
  
    constructor(data?: ILoginInputModel) {
      if (data) {
        for (var property in data) {
          if (data.hasOwnProperty(property))
            (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }
  