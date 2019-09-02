import { Injectable, Component } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { EditarMiembroComponent } from '../Components/miembros/editar-miembro/editar-miembro.component';


@Injectable({
  providedIn: 'root'
})
export class GuardarCambiosGuard implements CanDeactivate<EditarMiembroComponent> {
  canDeactivate( component: EditarMiembroComponent) {
    if (component.formEditar.dirty) {
      return confirm('Va salir sin guardar los cambios. Desea Contunuar?');
    }
    return true;
  }
}
