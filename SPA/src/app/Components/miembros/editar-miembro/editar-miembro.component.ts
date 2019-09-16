import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Usuario } from '../../../model/Usuario';
import { AlertasService } from '../../../Servicios/alertas.service';
import { NgForm } from '@angular/forms';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AuthService } from '../../../Servicios/auth.service';

@Component({
  selector: 'app-editar-miembro',
  templateUrl: './editar-miembro.component.html',
  styleUrls: ['./editar-miembro.component.css']
})
export class EditarMiembroComponent implements OnInit {
  @ViewChild('formEditar' , {static: true}) formEditar: NgForm;
  usuario: Usuario;
  fotoUrl: string;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotifation($event: any) {
    if (this.formEditar.dirty) {
      $event.returnValue = true;
    }
  }
  constructor(private route: ActivatedRoute,
              private alertas: AlertasService,
              private usuarioServico: UsuariosService,
              private auth: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe( data => {
      this.usuario = data['usuario'];
     });
    this.auth.fotoUrl.subscribe(fotourl => this.fotoUrl = fotourl);
  }
  Editar() {
   this.usuarioServico.EditarUsuario(this.auth.DecodeToken.nameid, this.formEditar.value).subscribe(next => {
    this.alertas.exito('Se guardaron los datos de manera correcta');
    this.formEditar.reset(this.usuario);
   }, error => {
     this.alertas.error(error);
   });
  }

}
