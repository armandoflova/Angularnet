export interface Mensaje {
    id: number;
    remitenteId: number;
    remintenteNombre: string;
    remitenteUrl: string;
    destinatarioId: string;
    destinatarioNombre: string;
    destinatarioUrl: string;
    contenido: string;
    estaLeido: boolean;
    fechaLectura: Date;
    fechaEnvio?: Date;
}
