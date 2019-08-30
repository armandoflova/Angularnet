import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Servicios/auth.service';
import { AlertasService } from '../../Servicios/alertas.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
   model: any = {};
   token: '';
  constructor(public authServicio: AuthService,
              private alertas: AlertasService) { }

  ngOnInit() {
  }
  login() {
    this.authServicio.login(this.model).subscribe(next => {
     this.alertas.exito('Se logueo de mnaera correcta');
        }, error => {
      this.alertas.error(error);
    });
  }

  logueado() {
    return this.authServicio.logueado();
  }

  Logout() {
    localStorage.removeItem('token');
    this.alertas.info('Cerrando Sesión');
  }
}
