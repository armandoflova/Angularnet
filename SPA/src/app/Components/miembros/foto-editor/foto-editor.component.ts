import { Component, OnInit, Input } from '@angular/core';
import { Foto } from '../../../model/Foto';
import { FileUploader } from 'ng2-file-upload/ng2-file-upload';
import { AuthService } from '../../../Servicios/auth.service';
import { environment } from '../../../../environments/environment';
import { UsuariosService } from '../../../Servicios/usuarios.service';
import { AlertasService } from '../../../Servicios/alertas.service';



@Component({
  selector: 'app-foto-editor',
  templateUrl: './foto-editor.component.html',
  styleUrls: ['./foto-editor.component.css']
})
export class FotoEditorComponent implements OnInit {
  @Input() Fotos: Foto[];
  fotoActual: Foto;
  public uploader: FileUploader;
   hasBaseDropZoneOver = false;
   baseUrl = environment.Urlapi;
   constructor(private Auth: AuthService,
               private UsuarioServio: UsuariosService,
               private Alertas: AlertasService) { }

  ngOnInit() {
    this.iniciarUpload();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  iniciarUpload() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'usuarios/' + this.Auth.DecodeToken.nameid + '/fotos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
     });

    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };
    this.uploader.onSuccessItem = (item, response, status , headers) => {
      if (response) {
        const res: Foto = JSON.parse(response);
        const foto = {
          id: res.id,
          url: res.url,
          fechaAgregada: res.fechaAgregada,
          descripcion: res.descripcion,
          esPrincipal: res.esPrincipal
        };
        this.Fotos.push(foto);
        if (foto.esPrincipal) {
          this.Auth.cambiarFotoMiembro(foto.url);
          this.Auth.usuarioActual.url = foto.url;
          localStorage.setItem('usuario', JSON.stringify(this.Auth.usuarioActual));
        }
      }
    };
  }

  fotoPrincipal(foto: Foto) {
    this.UsuarioServio.FotoPrincipal(this.Auth.DecodeToken.nameid , foto.id).subscribe(() => {
      this.fotoActual = this.Fotos.filter(f => f.esPrincipal === true)[0];
      this.fotoActual.esPrincipal = false;
      foto.esPrincipal = true;
      this.Auth.cambiarFotoMiembro(foto.url);
      this.Auth.usuarioActual.url = foto.url;
      localStorage.setItem('usuario', JSON.stringify(this.Auth.usuarioActual));
    }, error => {
        console.log('Ocurrio un error');
    });
  }

  eliminarFoto(id: number) {
    this.Alertas.confirmar('desea eliminar esta foto?' , () => {
      this.UsuarioServio.EliminarFoto(this.Auth.DecodeToken.nameid, id).subscribe(() => {
        this.Fotos.splice(this.Fotos.findIndex(f => f.id === id), 1);
        this.Alertas.exito('foto eliminada')
      }, error => {
        this.Alertas.error('ocurrio un error');
      });
    });
  }
}
