import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../Servicios/auth.service';
import { AlertasService } from '../../Servicios/alertas.service';


@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  model: any = {};
  @Output() cancelRegistro  = new EventEmitter();
  constructor(private authServicio: AuthService,
              private alertasS: AlertasService) { }

  ngOnInit() {
  }

  Registrar(){
    this.authServicio.Registar(this.model).subscribe(() => {
     this.alertasS.exito('Registro de manera correcta');
    }, error => {
      console.log(error);
      this.alertasS.error(error);
    });
    console.log(this.model);

  }
  Cancelar() {
    this.cancelRegistro.emit(false);
    console.log('se cancelo');
    }

}
