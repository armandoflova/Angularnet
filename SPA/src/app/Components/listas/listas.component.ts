import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../model/Usuario';
import { UsuariosService } from '../../Servicios/usuarios.service';
import { Paginacion, ResultadoPagina } from 'src/app/model/Paginacion';
import { ActivatedRoute } from '@angular/router';
import { AlertasService } from 'src/app/Servicios/alertas.service';


@Component({
  selector: 'app-listas',
  templateUrl: './listas.component.html',
  styleUrls: ['./listas.component.css']
})
export class ListasComponent implements OnInit  {
  Usuarios: Usuario[];
  parametrosUsuario: any = {};
  paginacion: Paginacion;
  likeesparams: string;
  constructor(private usuarioServicios: UsuariosService,
              private router: ActivatedRoute,
              private alertas: AlertasService) { }
  ngOnInit() {
    this.router.data.subscribe(data => {
    this.Usuarios = data['usuarios'].resultado;
    this.paginacion = data['usuarios'].paginacion;
    this.likeesparams = 'Likers';
    console.log(this.paginacion);
    });
 }

 cargarUsuarios() {
  this.usuarioServicios.ObtenerUsuarios(this.paginacion.paginaActaul, this.paginacion.itemsPorPagina, null , this.likeesparams)
  .subscribe( (res: ResultadoPagina<Usuario[]>) => {
    this.Usuarios = res.resultado;
    this.paginacion = res.paginacion;
  }, error => {
      this.alertas.error(error);
  });
}

cambioPagina(event: any): void{
  this.paginacion.paginaActaul = event.page;
  this.cargarUsuarios();
}
}
