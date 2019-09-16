import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableDataSource, MatPaginatorModule } from '@angular/material';
import { Usuario } from '../../model/Usuario';
import { UsuariosService } from '../../Servicios/usuarios.service';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material';

@Component({
  selector: 'app-listas',
  templateUrl: './listas.component.html',
  styleUrls: ['./listas.component.css']
})
export class ListasComponent implements OnInit , AfterViewInit {
  displayedColumns: string[] = ['nombre', 'genero', 'edad', 'alias', 'city' , 'pais'];
  dataSource = new MatTableDataSource<Usuario>();
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild (MatPaginator, { static : true }) paginador: MatPaginator;
  constructor(private usuarioServicios: UsuariosService) { }

  ngOnInit() {

    this.ObtenerUsuarios();
  }
   ngAfterViewInit() {
    this.dataSource.sort = this.sort;
   }

  ObtenerUsuarios() {
    this.usuarioServicios.ObtenerUsuarios().subscribe( (resultado: any) => {
      this.dataSource = resultado;
      console.log(this.dataSource);
    });
  }
  applyFilter(texto: string) {
    this.dataSource.filter = texto.trim().toLowerCase();
    if ( this .dataSource.paginator) {
      this .dataSource.paginator.firstPage ();
   }
  }
}
