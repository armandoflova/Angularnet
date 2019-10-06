import { BrowserModule, HAMMER_GESTURE_CONFIG, HammerGestureConfig } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './Components/nav/nav.component';
import {HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { InicioComponent } from './Components/inicio/inicio.component';
import { RegistroComponent } from './Components/registro/registro.component';
import { ErrorInterceptorProvider } from './Servicios/auth.interceptor';
import { BsDropdownModule, TabsModule, PaginationModule, ButtonsModule} from 'ngx-bootstrap';
import { MiembrosListaComponent } from './Components/miembros/miembros-lista/miembros-lista.component';
import { ListasComponent } from './Components/listas/listas.component';
import { MensajesComponent } from './Components/mensajes/mensajes.component';
import { TarjetaMiembrosComponent } from './Components/miembros/tarjeta-miembros/tarjeta-miembros.component';
import { MiembrosDetalleComponent } from './Components/miembros/miembros-detalle/miembros-detalle.component';
import { MiembrosDetalleResolver } from './Resolver/miembros-detalle.Resolver';
import { MiembrosResolver } from './Resolver/miembros.resolver';
import { EditarMiembroComponent } from './Components/miembros/editar-miembro/editar-miembro.component';
import { MiembrosEditarResolve } from './Resolver/miembros-editar.resolver';
import {MatTableModule, MatPaginatorModule, MatSortModule, MatFormFieldModule,
   MatInputModule, MatDatepickerModule, MatNativeDateModule} from '@angular/material';
import { FotoEditorComponent } from './Components/miembros/foto-editor/foto-editor.component';
import { FileUploadModule } from 'ng2-file-upload';
import {TimeAgoPipe} from 'time-ago-pipe';
import { registerLocaleData } from '@angular/common';
import locales from '@angular/common/locales/es-PE';
registerLocaleData(locales);


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
    EditarMiembroComponent,
    FotoEditorComponent,
    TimeAgoPipe
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    NgxGalleryModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatPaginatorModule,
    FileUploadModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
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
    { provide: LOCALE_ID, useValue: 'es-PE' },
    ErrorInterceptorProvider,
    MiembrosDetalleResolver,
    MiembrosResolver,
     MiembrosEditarResolve,
    { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
