using System;
using System.Collections.Generic;
using webapi.Models;

namespace webapi.Dtos
{
    public class UsuarioDetalles
    {
         public int Id { get; set; }
        public string  Nombre { get; set; }
       
        public string Genero { get; set; }

        public int Edad { get; set; }
        public string  Alias { get; set; }
        public DateTime Creado { get; set; }
        public DateTime UltimaConexion { get; set; }
        public string Introduccion { get; set; }
        public string BuscarPor { get; set; }
        public string Intereses { get; set; }
        public string  City { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
        public ICollection<FotosDetalles> Fotos { get; set; }
    }
}