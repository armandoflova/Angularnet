import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { environment } from '../../environments/environment';
import { ResultadoPagina } from '../model/Paginacion';
import { map, tap} from 'rxjs/Operators';



@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(private http: HttpClient) { }

  ObtenerUsuarios(pagina? , tamano?, usuarioparametros?): Observable<ResultadoPagina<Usuario[]>> {
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
    return this.http.post(environment.Urlapi + 'usuarios/' + idUsuario + '/fotos/' + id + '/esPrincipal' , {});
  }

  EliminarFoto(idUsuario: number , id: number){
    return this.http.delete(environment.Urlapi + 'usuarios/' + idUsuario + '/fotos/' + id);
  }
}
