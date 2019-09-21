using System;
using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class UsuarioRegistro
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4 , ErrorMessage = "La contraseña debe tener entre 4 y 8 caracteres")]
        public string password { get; set; }
        [Required]
        public string Genero { get; set; }

        [Required]      
        public DateTime FechaNacimiento { get; set; }
         public DateTime UltimaConexion { get; set; }
        [Required]
         public DateTime Creado { get; set; }
        public string  Alias { get; set; }
        [Required]
       
        public string  City { get; set; }
        [Required]
        public string Pais { get; set; }
        public UsuarioRegistro()
        {
            UltimaConexion = DateTime.Now;
            Creado = DateTime.Now;
        }
    }
}