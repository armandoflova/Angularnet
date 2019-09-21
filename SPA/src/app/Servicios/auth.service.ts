import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {map} from 'rxjs/Operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { Usuario } from '../model/Usuario';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
   jwtHelper = new JwtHelperService();
   DecodeToken: any;
   usuarioActual: Usuario;
   fotoUrl = new BehaviorSubject<string>('../../assets/user.png');
   UrlActual = this.fotoUrl.asObservable();
  constructor(private http: HttpClient,
              private router: Router) { }


  cambiarFotoMiembro(fotourl: string) {
    this.fotoUrl.next(fotourl);
  }
  Registar(usuario: Usuario) {
  return this.http.post(environment.Urlapi + 'Auth/Registro', usuario);
  }

  login(model: any) {
    return this.http.post(environment.Urlapi + 'Auth/Login', model).pipe(map((respuesta: any) => {
      const token = respuesta.token;
      const usuario = respuesta.usuario;
      if ( usuario ) {
        localStorage.setItem('token' , token );
        localStorage.setItem('usuario' , JSON.stringify(usuario));
        this.DecodeToken = this.jwtHelper.decodeToken(token);
        this.usuarioActual = usuario;
        this.cambiarFotoMiembro(this.usuarioActual.url);
        this.router.navigate(['/miembros']);
      }
    }));
  }
logueado() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

}
