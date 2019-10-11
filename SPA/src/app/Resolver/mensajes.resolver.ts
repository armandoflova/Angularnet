import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { UsuariosService } from '../Servicios/usuarios.service';
import { AlertasService } from '../Servicios/alertas.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/Operators';
import { Mensaje } from '../model/Mensaje';
import { AuthService } from '../Servicios/auth.service';


@Injectable()
export class MensajesResolver implements Resolve<Mensaje[]> {
    numeroPagina = 1;
    tamanoPagina = 5;
    TipoMensaje = 'noLeido';
    constructor(private usuarioServicio: UsuariosService,
                private router: Router,
                private alertas: AlertasService,
                private auth: AuthService) {}

resolve(route: ActivatedRouteSnapshot): Observable<Mensaje[]> {
    return this.usuarioServicio.obtenerMensajes(this.auth.DecodeToken.nameid , this.numeroPagina, this.tamanoPagina, this.TipoMensaje)
    .pipe( catchError (error => {
        this.alertas.error('Hubo un error al cargar Mensajes');
        this.router.navigate(['/home']);
        return of(null);
    })
    );

}
}
