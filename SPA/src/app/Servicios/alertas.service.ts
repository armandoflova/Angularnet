import { Injectable } from '@angular/core';
declare let alertify: any;
@Injectable({
  providedIn: 'root'
})
export class AlertasService {

  constructor() { }

  confirmar( mensaje: string, okCallBack: () => any) {
    // tslint:disable-next-line: only-arrow-functions
    alertify.confirm(mensaje , function(e) {
      if (e){
        okCallBack();
      }else{}
    });
  }

  exito(mensaje: string ){
    alertify.success(mensaje);
  }

  error(mensaje: string){
    alertify.error(mensaje);
  }
  cuidado(mensaje: string){
    alertify.warning(mensaje);
  }

  info(mensaje : string){
    alertify.message(mensaje);
  }
}
