namespace webapi.Helpers
{
    public class MensajeParams
    {
        
        private const int MaximoPaginas = 50;
        public int NumeroPaginas { get; set; } = 1;
        private int tamanoPagina = 10;
        public int TamanoPagina
        {
            get { return tamanoPagina; }
            set { tamanoPagina  = (value > MaximoPaginas) ? MaximoPaginas : value; }
        }
        public int UsuarioId { get; set; }
        public string TipoContenido { get; set; } = "noLeido";
    }
}