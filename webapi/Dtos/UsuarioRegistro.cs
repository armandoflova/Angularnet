using System.ComponentModel.DataAnnotations;

namespace webapi.Dtos
{
    public class UsuarioRegistro
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4 , ErrorMessage = "La contraseña debe tener entre 4 y 8 caracteres")]
        public string  password { get; set; }
    }
}