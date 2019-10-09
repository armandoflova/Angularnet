import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MiembrosListaComponent } from './Components/miembros/miembros-lista/miembros-lista.component';
import { MensajesComponent } from './Components/mensajes/mensajes.component';
import { ListasComponent } from './Components/listas/listas.component';
import { AuthGuard } from './Guard/auth.guard';
import { InicioComponent } from './Components/inicio/inicio.component';
import { MiembrosDetalleComponent } from './Components/miembros/miembros-detalle/miembros-detalle.component';
import { MiembrosDetalleResolver } from './Resolver/miembros-detalle.Resolver';
import { MiembrosResolver } from './Resolver/miembros.resolver';
import { EditarMiembroComponent } from './Components/miembros/editar-miembro/editar-miembro.component';
import { MiembrosEditarResolve } from './Resolver/miembros-editar.resolver';
import { GuardarCambiosGuard } from './Guard/guardar-cambios.guard';
import { ListasResolver } from './Resolver/Listas.Resolver';


const routes: Routes = [
  {path: '' , component: InicioComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'miembros' , component: MiembrosListaComponent , resolve: {usuarios: MiembrosResolver}},
      {path: 'miembros/:id' , component: MiembrosDetalleComponent , resolve: {usuario: MiembrosDetalleResolver}},
      {path: 'editar' , component: EditarMiembroComponent, resolve: {usuario: MiembrosEditarResolve},
       canDeactivate: [GuardarCambiosGuard] },
      {path: 'mensajes' , component: MensajesComponent},
      {path: 'listas' , component: ListasComponent , resolve: {usuarios: ListasResolver}}
    ]
  },
  {path: '**' , redirectTo: 'home' , pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
