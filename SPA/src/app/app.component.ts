import { Component, OnInit } from '@angular/core';
import { AuthService } from './Servicios/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { Usuario } from './model/Usuario';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  jwtHelper = new JwtHelperService();

  constructor(public authServicio: AuthService) {}

  ngOnInit(){
    const token = localStorage.getItem('token');
    const usuario: Usuario = JSON.parse(localStorage.getItem('usuario'));
    if (token) {
     this.authServicio.DecodeToken = this.jwtHelper.decodeToken(token);
    }
    if ( usuario) {
      this.authServicio.usuarioActual = usuario;
      this.authServicio.cambiarFotoMiembro(usuario.url);
    }
  }
}
