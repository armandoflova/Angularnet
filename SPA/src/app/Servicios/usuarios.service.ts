import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { environment } from '../../environments/environment';
import { ResultadoPagina } from '../model/Paginacion';
import { map, tap} from 'rxjs/Operators';
import { Mensaje } from '../model/Mensaje';



@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(private http: HttpClient) { }

  ObtenerUsuarios(pagina? , tamano?, usuarioparametros?, likeesparams?): Observable<ResultadoPagina<Usuario[]>> {
    const resultPagina: ResultadoPagina<Usuario[]> = new ResultadoPagina<Usuario[]>();
    let params = new HttpParams();
    if (pagina != null && tamano != null) {
     params = params.append('NumeroPaginas', pagina);
     params = params.append('TamanoPagina', tamano);
    }
    if (usuarioparametros != null ) {
      params = params.append('Genero' , usuarioparametros.genero);
      params = params.append('MinEdad' , usuarioparametros.minEdad);
      params = params.append('MaxEdad' , usuarioparametros.maxEdad);
      params = params.append('ordenarPor' , usuarioparametros.ordenarPor);
    }
    if (likeesparams === 'Likers') {
      params = params.append('Likers' , 'true' );
    }

    if (likeesparams === 'Likees') {
      params = params.append('Likees' , 'true' );
    }

    return this.http.get<Usuario[]>(environment.Urlapi + 'Usuario', { params, observe: 'response'}).pipe
    (map(response => {
      resultPagina.resultado = response.body;
      if ( response.headers.get('paginacion') != null) {
        resultPagina.paginacion = JSON.parse(response.headers.get('paginacion'));
      }
      return resultPagina;
    }));
  }

  ObtenerUsuario(id: number): Observable<Usuario> {
    return this.http.get<Usuario>(environment.Urlapi + 'Usuario/' + id );
  }
   EditarUsuario(id: number, usuario: Usuario): Observable<Usuario> {
    return this.http.put<Usuario>(environment.Urlapi + 'Usuario/' +  id, usuario );
  }
  FotoPrincipal(idUsuario: number, id: number): Observable<any> {
    return this.http.post(environment.Urlapi + 'Usuario/' + idUsuario + '/fotos/' + id + '/esPrincipal' , {});
  }

  EliminarFoto(idUsuario: number , id: number){
    return this.http.delete(environment.Urlapi + 'Usuario/' + idUsuario + '/fotos/' + id);
  }

  darLike(id: number, recipientId: number){
    return this.http.post(environment.Urlapi + 'Usuario/' + id + '/Like/' + recipientId , {});
  }

  obtenerMensajes(id: number , pagina? , tamano?, TipoMensaje?) {
    const resultPagina: ResultadoPagina<Mensaje[]> = new ResultadoPagina<Mensaje[]>();
    let params = new HttpParams();
    if (pagina != null && tamano != null) {
      params = params.append('NumeroPaginas', pagina);
      params = params.append('TamanoPagina', tamano);
     }
    params = params.append('TipoContenido', TipoMensaje);

    return this.http.get(environment.Urlapi + 'Usuario/' + id + '/mensajes/' , {observe: 'response', params})
    .pipe(map(respuesta => {
      resultPagina.resultado = respuesta.body as Mensaje[];
      if (respuesta.headers.get('Paginacion') != null) {
         resultPagina.paginacion = JSON.parse(respuesta.headers.get('Paginacion'));
      }
      return resultPagina;
    })
    );
  }

  obtenerchat(id: number , destinatarioId: number) {
    return this.http.get(environment.Urlapi + 'Usuario/' + id + '/mensajes/chat/' + destinatarioId);
  }

  enviarMensaje(id: number , mensaje: Mensaje){
    return this.http.post(environment.Urlapi + 'Usuario/' + id + '/mensajes/' , mensaje );
  }
  eliminarMensaje(usuarioId: number , id: number) {
    return this.http.post(environment.Urlapi + 'Usuario/' + usuarioId + '/mensajes/' + id, {});
  }

  marcarLeido(usuarioId: number , id: number){
    return this.http.post(environment.Urlapi + 'Usuario/' + usuarioId + '/mensajes/Leido/' + id , {}).subscribe();
  }
}
