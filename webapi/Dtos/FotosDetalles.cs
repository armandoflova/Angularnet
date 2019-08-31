using System;

namespace webapi.Dtos
{
    public class FotosDetalles
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAgreaga { get; set; }
        public bool EsPrincipal {get; set;}

    }
}