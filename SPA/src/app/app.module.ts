import { BrowserModule, HAMMER_GESTURE_CONFIG, HammerGestureConfig } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './Components/nav/nav.component';
import {HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { InicioComponent } from './Components/inicio/inicio.component';
import { RegistroComponent } from './Components/registro/registro.component';
import { ErrorInterceptorProvider } from './Servicios/auth.interceptor';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { MiembrosListaComponent } from './Components/miembros/miembros-lista/miembros-lista.component';
import { ListasComponent } from './Components/listas/listas.component';
import { MensajesComponent } from './Components/mensajes/mensajes.component';
import { TarjetaMiembrosComponent } from './Components/miembros/tarjeta-miembros/tarjeta-miembros.component';
import { MiembrosDetalleComponent } from './Components/miembros/miembros-detalle/miembros-detalle.component';
import { MiembrosDetalleResolver } from './Resolver/miembros-detalle.Resolver';
import { MiembrosResolver } from './Resolver/miembros.resolver';
import { EditarMiembroComponent } from './Components/miembros/editar-miembro/editar-miembro.component';
import { MiembrosEditarResolve } from './Resolver/miembros-editar.resolver';


export function tokenGetter() {
  return localStorage.getItem('token');
}
export class CustomHammerConfig extends HammerGestureConfig {
  overrides = {
      pinch: { enable: false },
      rotate: { enable: false }
  };
}
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    InicioComponent,
    RegistroComponent,
    MiembrosListaComponent,
    ListasComponent,
    MensajesComponent,
    TarjetaMiembrosComponent,
    MiembrosDetalleComponent,
    EditarMiembroComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgxGalleryModule,
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    JwtModule.forRoot({
      config: {
        // tslint:disable-next-line: object-literal-shorthand
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/Auth']
      }
    })
    ],
  providers: [
    ErrorInterceptorProvider,
    MiembrosDetalleResolver,
    MiembrosResolver,
     MiembrosEditarResolve,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
