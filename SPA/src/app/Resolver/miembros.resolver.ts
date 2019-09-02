import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Usuario } from '../model/Usuario';
import { UsuariosService } from '../Servicios/usuarios.service';
import { AlertasService } from '../Servicios/alertas.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/Operators';


@Injectable()
export class MiembrosResolver implements Resolve<Usuario[]> {

    constructor(private usuarioServicio: UsuariosService,
                private router: Router,
                private alertas: AlertasService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Usuario[]> {
    return this.usuarioServicio.ObtenerUsuarios()
    .pipe( catchError (error => {
        this.alertas.error('Hubo un error al cargar los datos');
        this.router.navigate(['/home']);
        return of(null);
    })
    );

}
}
