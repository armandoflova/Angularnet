import { Component, OnInit, Input } from '@angular/core';
import { Usuario } from '../../../model/Usuario';
import { AuthService } from '../../../Servicios/auth.service';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AlertasService } from '../../../Servicios/alertas.service';

@Component({
  selector: 'app-tarjeta-miembros',
  templateUrl: './tarjeta-miembros.component.html',
  styleUrls: ['./tarjeta-miembros.component.css']
})
export class TarjetaMiembrosComponent implements OnInit {
  @Input() Usuario: Usuario;
  constructor(private auth: AuthService,
              private usuarioServicio: UsuariosService,
              private alertas: AlertasService) { }

  ngOnInit() {
  }
  darLike(id: number) {
    this.usuarioServicio.darLike(this.auth.DecodeToken.nameid , id).subscribe(data => {
      this.alertas.exito('Se dio Me Gusta a ' + this.Usuario.nombre);
    }, error => {
      this.alertas.error(error);
    });
  }
}
