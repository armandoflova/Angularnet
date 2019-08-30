import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { MiembrosListaComponent } from './Components/miembros-lista/miembros-lista.component';
import { MensajesComponent } from './Components/mensajes/mensajes.component';
import { ListasComponent } from './Components/listas/listas.component';
import { AuthGuard } from './Guard/auth.guard';


const routes: Routes = [
  {path: '' , component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'miembros' , component: MiembrosListaComponent},
      {path: 'mensajes' , component: MensajesComponent},
      {path: 'listas' , component: ListasComponent}
    ]
  },
  {path: '**' , redirectTo: 'home' , pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
