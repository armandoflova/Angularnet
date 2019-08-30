import { Injectable } from '@angular/core';
import {   CanActivate, Router } from '@angular/router';
import { AuthService } from '../Servicios/auth.service';
import { AlertasService } from '../Servicios/alertas.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authservicio: AuthService,
              private alertas: AlertasService,
              private router: Router){}
  canActivate(): boolean {
   if(this.authservicio.logueado()) {
     return true;
   }

   this.alertas.error('Necesita iniciar sesión');
   this.router.navigate(['/home']);
   return false;
  }
}
