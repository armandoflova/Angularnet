import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../model/Usuario';
import { environment } from '../../environments/environment';



@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(private http: HttpClient) { }

  ObtenerUsuarios(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(environment.Urlapi + 'Usuario');
  }

  ObtenerUsuario(id): Observable<Usuario> {
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
