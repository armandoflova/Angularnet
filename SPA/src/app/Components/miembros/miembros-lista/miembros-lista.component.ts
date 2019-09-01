import { Component, OnInit } from '@angular/core';
import { UsuariosService } from 'src/app/Servicios/usuarios.service';
import { AlertasService } from '../../../Servicios/alertas.service';
import { Usuario } from 'src/app/model/Usuario';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-miembros-lista',
  templateUrl: './miembros-lista.component.html',
  styleUrls: ['./miembros-lista.component.css']
})
export class MiembrosListaComponent implements OnInit {
  Usuarios: Usuario[];
  constructor(private usuarioServico: UsuariosService,
              private alertas: AlertasService,
              private router: ActivatedRoute) { }

  ngOnInit() {
    this.router.data.subscribe(data => {
      this.Usuarios = data['usuarios'];
    });
  }
}
