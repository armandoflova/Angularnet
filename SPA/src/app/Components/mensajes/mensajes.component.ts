import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Servicios/auth.service';
import { UsuariosService } from '../../Servicios/usuarios.service';
import { AlertasService } from '../../Servicios/alertas.service';
import { ActivatedRoute } from '@angular/router';
import { Mensaje } from '../../model/Mensaje';
import { Paginacion } from '../../model/Paginacion';
import { findIndex } from 'rxjs/Operators';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.css']
})
export class MensajesComponent implements OnInit {
  mensajes: Mensaje[];
  paginacion: Paginacion;
  TipoMensaje: 'noLeido';
  constructor(private auth: AuthService,
              private usuariosServicio: UsuariosService,
              private alertas: AlertasService,
              private router: ActivatedRoute) { }

  ngOnInit() {
     this.router.data.subscribe( data => {
      this.mensajes = data['mensajes'].resultado;
      this.paginacion = data['mensajes'].paginacion;
       // console.log(this.mensajes);
     });
  }

  cargarMensajes() {
    this.usuariosServicio.obtenerMensajes(this.auth.DecodeToken.nameid , this.paginacion.paginaActaul 
      , this.paginacion.itemsTotal , this.TipoMensaje)
    .subscribe( result => {
      this.mensajes = result.resultado;
      this.paginacion = result.paginacion;
    }, error => {
      this.alertas.error(error);
    });
  }

   cambioPagina(event: any): void{
      this.paginacion.paginaActaul = event.page;
      this.cargarMensajes();
   }

   eliminarMensaje(idmensaje: number) {
     this.usuariosServicio.eliminarMensaje(this.auth.DecodeToken.nameid , idmensaje).subscribe(() => {
       this.alertas.confirmar('Esta seguro de eliminar esteMensaje' , () => {
        this.mensajes.splice(this.mensajes.findIndex(m => m.id === idmensaje) , 1);
        this.alertas.exito('Se elimino Mensaje');
       });
     }, error => {
      this.alertas.error(error);
     });
   }
}
