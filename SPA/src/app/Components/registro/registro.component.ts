import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../../Servicios/auth.service';
import { AlertasService } from '../../Servicios/alertas.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Usuario } from '../../model/Usuario';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  Usuario: Usuario;
  formRegistro: FormGroup;
  @Output() cancelRegistro  = new EventEmitter();
  constructor(private authServicio: AuthService,
              private alertasS: AlertasService,
              private fb: FormBuilder,
              private router: Router) { }

  ngOnInit() {
    this.CrearRegistroForm();
  }

  CrearRegistroForm() {
    this.formRegistro = this.fb.group({
      genero: ['Masculino' , Validators.required],
      nombre: ['' , Validators.required],
      fechaNacimiento: [null, Validators.required],
      alias: ['', Validators.required],
      city: ['' , Validators.required],
      pais: ['' , Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validators: this.compararPasswords});
  }
  compararPasswords(form: FormGroup) {
    return form.get('password').value === form.get('confirmPassword').value ? null : {'mismatch': true};
  }

  Registrar() {
   if (this.formRegistro.status) {
    this.Usuario = Object.assign({}, this.formRegistro.value);
    console.log(this.Usuario);
    this.authServicio.Registar(this.Usuario).subscribe( () => {
      this.alertasS.exito('se registro de manera correcta');
    }, error => {
      this.alertasS.error(error);
    }, () => {
      this.authServicio.login(this.Usuario).subscribe( () => {
        this.router.navigate(['/miembros']);
      });
    });
   }

  }
  Cancelar() {
    this.cancelRegistro.emit(false);
    console.log('se cancelo');
    }

}
