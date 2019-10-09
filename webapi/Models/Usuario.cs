using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string  Nombre { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Genero { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string  Alias { get; set; }
        public DateTime Creado { get; set; }
        public DateTime UltimaConexion { get; set; }
        public string Introduccion { get; set; }
        public string BuscarPor { get; set; }
        public string Intereses { get; set; }
        public string  City { get; set; }
        public string Pais { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }

    }
}