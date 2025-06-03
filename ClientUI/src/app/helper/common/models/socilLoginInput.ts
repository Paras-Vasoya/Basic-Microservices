export interface ISocialLoginInputModel {
    idToken: string;
  }
  
  export class SocialLoginInputModel implements ISocialLoginInputModel {
    idToken!: string;
  
    constructor(data?: ISocialLoginInputModel) {
      if (data) {
        for (var property in data) {
          if (data.hasOwnProperty(property))
            (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }
  