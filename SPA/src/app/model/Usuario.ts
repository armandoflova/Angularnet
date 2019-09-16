import { Foto } from './Foto';

export interface Usuario  {
    id: number;
    nombre: string;
    genero: string;
    edad: number;
    alias: string;
    creado: Date;
     ultimaConexion: Date;
     introduccion?: string;
     buscarPor?: string;
     intereses?: string;
      city: string;
     pais: string;
     url: string;
     fotos: Foto[];
}
