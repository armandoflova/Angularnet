namespace webapi.Helpers
{
    public class ParametrosUsuarios
    {
        private const int MaximoPaginas = 50;
        public int NumeroPaginas { get; set; } = 1;
        private int tamanoPagina = 10;
        public int TamanoPagina
        {
            get { return tamanoPagina; }
            set { tamanoPagina  = (value > MaximoPaginas) ? MaximoPaginas : value; }
        }
        
        public int Id { get; set; }
        public string Genero { get; set; }
        public int MinEdad { get; set; } = 18;
        public int MaxEdad { get; set; } = 99;
        public string ordenarPor { get; set; }
        public bool Likees { get; set; } = false;
        public bool Likers { get; set; } = false;
    }
}