using System;

namespace webapi.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAgreaga { get; set; }
        public bool EsPrincipal {get; set;}
        public string PublicId { get; set; }

        public virtual Usuario Usuario {get; set;}
        public int UsuarioID { get; set; }
    }
}