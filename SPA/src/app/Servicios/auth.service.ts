import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {map} from 'rxjs/Operators';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   jwtHelper = new JwtHelperService();
   DecodeToken: any;
  constructor(private http: HttpClient) { }
 

  Registar(model: any) {
  return this.http.post(environment.Urlapi + '/Auth/Registro', model);
  }

  login(model: any) {
    return this.http.post(environment.Urlapi + '/Auth/Login', model).pipe(map((respuesta: any) => {
      const usuario = respuesta.token;
      if(usuario) {
        localStorage.setItem('token' , usuario);
        this.DecodeToken = this.jwtHelper.decodeToken(usuario);
        console.log(this.DecodeToken);
        
      }
    }));
  }
logueado() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

}
