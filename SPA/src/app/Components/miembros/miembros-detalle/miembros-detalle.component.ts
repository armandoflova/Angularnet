import { Component, OnInit } from '@angular/core';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AlertasService } from '../../../Servicios/alertas.service';
import { ActivatedRoute } from '@angular/router';
import { Usuario } from 'src/app/model/Usuario';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { AuthService } from '../../../Servicios/auth.service';

@Component({
  selector: 'app-miembros-detalle',
  templateUrl: './miembros-detalle.component.html',
  styleUrls: ['./miembros-detalle.component.css']
})
export class MiembrosDetalleComponent implements OnInit {
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  usuario: Usuario;
  constructor(private usuarioServicio: UsuariosService,
              private alertas: AlertasService,
              private router: ActivatedRoute,
              private auth: AuthService) { }

  ngOnInit() {
    this.router.data.subscribe( data => {
      this.usuario = data['usuario'];
    });

    this.galleryOptions = [
      {
          width: '500px',
          height: '500px',
          imagePercent: 100,
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          preview: false
      }
    ];

    this.galleryImages = this.obtenerImagenes();
    }

    obtenerImagenes() {
     const imagenesUrl = [];
     for (const foto of this.usuario.fotos) {
      imagenesUrl.push({
        small: foto.url,
        medium: foto.url,
        big: foto.url,
        description: foto.url
        });
    }
     return imagenesUrl;
    }

    megusta(id: number) {
      this.usuarioServicio.darLike(this.auth.DecodeToken.nameid, id).subscribe( data => {
        this.alertas.exito('Le dio me Gusta a: ' + this.usuario.nombre);
      }, error => {
          console.log(error);
          this.alertas.error(error);
      });
    }
}
