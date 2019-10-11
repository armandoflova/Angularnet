import { Component, OnInit, Input } from '@angular/core';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AuthService } from '../../../Servicios/auth.service';
import { Mensaje } from 'src/app/model/Mensaje';
import { AlertasService } from '../../../Servicios/alertas.service';
import { tap } from 'rxjs/Operators';


@Component({
  selector: 'app-miembros-mensajes',
  templateUrl: './miembros-mensajes.component.html',
  styleUrls: ['./miembros-mensajes.component.css']
})
export class MiembrosMensajesComponent implements OnInit {
  @Input() recipienteId: number;
  Mensajes: Mensaje[];
  newMensaje: any = {};
  constructor(private auth: AuthService,
              private UsuarioServicio: UsuariosService,
              private alertas: AlertasService) { }

  ngOnInit() {
    this.obtenerMensajes();
  }

  obtenerMensajes() {
    const idremitenet = +this.auth.DecodeToken.nameid;
    this.UsuarioServicio.obtenerchat(this.auth.DecodeToken.nameid , this.recipienteId)
    .pipe(
      tap((mensajes: Mensaje[]) => {
         // tslint:disable-next-line: prefer-for-of
         for (let i = 0; i < mensajes.length; i++) {
          if ( mensajes[i].estaLeido === false && mensajes[i].remitenteId === idremitenet ) {
            console.log(mensajes[1].estaLeido , mensajes[1].id);
            this.UsuarioServicio.marcarLeido(idremitenet , mensajes[i].id);
          }
        }
      }))
    .subscribe( mensajes => {
      this.Mensajes = mensajes as Mensaje[];
      console.log(this.Mensajes);
    }, error => {
      this.alertas.error(error);
    });
  }
  enviarMensaje() {
    this.newMensaje.destinatarioId = this.recipienteId;
    console.log(this.newMensaje);
    this.UsuarioServicio.enviarMensaje(this.auth.DecodeToken.nameid , this.newMensaje)
    .subscribe((mensaje: Mensaje) => {
      this.Mensajes.unshift(mensaje);
      this.newMensaje.contenido = '';
    }, error => {
      this.alertas.error(error);
    });
  }
}
