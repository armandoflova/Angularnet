namespace webapi.Helpers
{
    public class paginaEncabezado
    {
        public int PaginaActaul { get; set; }
        public int ItemsPorPagina { get; set; }
        public int ItemsTotal { get; set; }
        public int PaginaTotal { get; set; }
        public paginaEncabezado(int paginaActual, int itemsporPagina, int itemsTotal, int paginaTotal)
        {
            this.PaginaActaul = paginaActual;
            this.ItemsPorPagina = itemsporPagina;
            this.ItemsTotal = itemsTotal;
            this.PaginaTotal = paginaTotal;
        }
    }
}