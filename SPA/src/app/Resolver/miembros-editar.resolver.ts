import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Usuario } from '../model/Usuario';
import { UsuariosService } from '../Servicios/usuarios.service';
import { AlertasService } from '../Servicios/alertas.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/Operators';
import { AuthService } from '../Servicios/auth.service';


@Injectable()
export class MiembrosEditarResolve implements Resolve<Usuario> {

    constructor(private usuarioServicio: UsuariosService,
                private router: Router,
                private alertas: AlertasService,
                private authservicio: AuthService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Usuario> {
    return this.usuarioServicio.ObtenerUsuario(this.authservicio.DecodeToken.nameid)
    .pipe( catchError (error => {
        this.alertas.error('hubo un error al recibir los datos');
        this.router.navigate(['/miembros']);
        return of(null);
    })
    );

}
}
