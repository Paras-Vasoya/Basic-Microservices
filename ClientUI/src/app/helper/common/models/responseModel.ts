export interface IResponseModel {
    success: boolean;
    message: string;
    data: any | any[] | null;
    errorMessage: string | null;
  }
  
  export class ResponseModel implements IResponseModel {
      success!: boolean;
      message!: string;
      data: any | any[] | null;
      errorMessage!: string | null;
  
      constructor(data?: IResponseModel) {
          if (data) {
              for (var property in data) {
                  if (data.hasOwnProperty(property))
                      (<any>this)[property] = (<any>data)[property];
              }
          }
      }
    }
  