import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {
  modoregistro = false;
  constructor() { }

  ngOnInit() {
  }
  registrotoogle(){
    this.modoregistro = true;
  }

  cancelRegistroMode(modoregistro: boolean) {
    this.modoregistro = modoregistro;
  }
}
