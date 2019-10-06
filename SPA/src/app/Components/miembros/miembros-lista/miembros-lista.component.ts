import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/model/Usuario';
import { ActivatedRoute } from '@angular/router';
import { Paginacion, ResultadoPagina } from '../../../model/Paginacion';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AlertasService } from '../../../Servicios/alertas.service';

@Component({
  selector: 'app-miembros-lista',
  templateUrl: './miembros-lista.component.html',
  styleUrls: ['./miembros-lista.component.css']
})
export class MiembrosListaComponent implements OnInit {
  Usuarios: Usuario[];
  usuario: Usuario = JSON.parse(localStorage.getItem('usuario'));
  generoLista = [{value: 'Masculino', display: 'Masculinos'}, {value: 'Femenino', display: 'Femeninos'}];
  parametrosUsuario: any = {};
  paginacion: Paginacion;
  constructor(private router: ActivatedRoute,
              private UsuariosServicios: UsuariosService,
              private alertas: AlertasService) { }

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.Usuarios = data['usuarios'].resultado;
      this.paginacion = data['usuarios'].paginacion;
      console.log(this.paginacion);
      });
    this.parametrosUsuario.genero = this.usuario.genero === 'Femenino' ? 'Masculino' : 'Femenino';
    this.parametrosUsuario.minEdad = 18;
    this.parametrosUsuario.maxEdad = 99;
    this.parametrosUsuario.ordenarPor = 'ultimaConexion';
  }

  cambioPagina(event: any): void{
    this.paginacion.paginaActaul = event.page;
    this.cargarUsuarios();
  }

  resetearFiltro() {
    this.parametrosUsuario.genero = this.usuario.genero === 'Femenino' ? 'Masculino' : 'Femenino';
    this.parametrosUsuario.minEdad = 18;
    this.parametrosUsuario.maxEdad = 99;
    this.cargarUsuarios();
  }

  cargarUsuarios() {
    this.UsuariosServicios.ObtenerUsuarios(this.paginacion.paginaActaul, this.paginacion.itemsPorPagina, this.parametrosUsuario)
    .subscribe( (res: ResultadoPagina<Usuario[]>) => {
      this.Usuarios = res.resultado;
      this.paginacion = res.paginacion;
    }, error => {
        this.alertas.error(error);
    });
  }
}
