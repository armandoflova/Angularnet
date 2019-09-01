import { Component, OnInit, Input } from '@angular/core';
import { Usuario } from '../../../model/Usuario';

@Component({
  selector: 'app-tarjeta-miembros',
  templateUrl: './tarjeta-miembros.component.html',
  styleUrls: ['./tarjeta-miembros.component.css']
})
export class TarjetaMiembrosComponent implements OnInit {
  @Input() Usuario: Usuario;
  constructor() { }

  ngOnInit() {
  }

}
