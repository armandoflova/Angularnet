export interface Paginacion {
    paginaActaul: number;
    itemsPorPagina: number;
    itemsTotal: number;
    paginaTotal: number;
}

export class ResultadoPagina<T> {
    resultado: T;
    paginacion: Paginacion;
}
