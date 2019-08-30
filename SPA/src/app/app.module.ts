import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './Components/nav/nav.component';
import {HttpClientModule } from '@angular/common/http';
import { InicioComponent } from './Components/inicio/inicio.component';
import { RegistroComponent } from './Components/registro/registro.component';
import { ErrorInterceptorProvider } from './Servicios/auth.interceptor';
import { BsDropdownModule } from 'ngx-bootstrap';
import { HomeComponent } from './Components/home/home.component';
import { MiembrosListaComponent } from './Components/miembros-lista/miembros-lista.component';
import { ListasComponent } from './Components/listas/listas.component';
import { MensajesComponent } from './Components/mensajes/mensajes.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    InicioComponent,
    RegistroComponent,
    HomeComponent,
    MiembrosListaComponent,
    ListasComponent,
    MensajesComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BsDropdownModule.forRoot()
    ],
  providers: [
    ErrorInterceptorProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
