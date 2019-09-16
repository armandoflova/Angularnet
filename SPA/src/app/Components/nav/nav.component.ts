import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Servicios/auth.service';
import { AlertasService } from '../../Servicios/alertas.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
   model: any = {};
   token: '';
   fotoUrl: string;
  constructor(public authServicio: AuthService,
              private alertas: AlertasService,
              private router: Router) { }

  ngOnInit() {
    this.authServicio.fotoUrl.subscribe(fotourl => this.fotoUrl = fotourl);
  }
  login() {
    this.authServicio.login(this.model).subscribe(next => {
     this.alertas.exito('Se logueo de manera correcta');
        }, error => {
      this.alertas.error(error);
    });
  }

  logueado() {
    return this.authServicio.logueado();
  }

  Logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.authServicio.usuarioActual = null;
    this.authServicio.DecodeToken = null;
    this.alertas.info('Cerrando Sesión');
    this.router.navigate(['/']);
  }
}
