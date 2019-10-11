using System;

namespace webapi.Dtos
{
    public class MensajeReturn
    {
        public int Id { get; set; }
        public int RemitenteId { get; set; }
        public string RemitenteNombre { get; set; }
        public string RemitenteUrl { get; set; }
        public int DestinatarioId { get; set; }
        public string DestinatarioNombre { get; set; }
        public string DestinatarioUrl { get; set; }
        public string Contenido { get; set; }
        public bool EstaLeido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public DateTime FechaEnvio { get; set; }
        
    }
}