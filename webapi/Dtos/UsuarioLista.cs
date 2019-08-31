using System;

namespace webapi.Dtos
{
    public class UsuarioLista
    {
         public int Id { get; set; }
        public string  Nombre { get; set; }
        public string Genero { get; set; }

        public int Edad { get; set; }
        public string  Alias { get; set; }
        public DateTime Creado { get; set; }
        public DateTime UltimaConexion { get; set; }
        public string  City { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
    }
}