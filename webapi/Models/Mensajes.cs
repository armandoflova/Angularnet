using System;

namespace webapi.Models
{
    public class Mensajes
    {
        public int Id { get; set; }
        public int RemitenteId { get; set; }
        public virtual Usuario Remitente { get; set; }
        public int DestinatarioId { get; set; }
        public virtual Usuario Destinatario { get; set; }
        public string Contenido { get; set; }
        public bool EstaLeido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool RemitenteElimina { get; set; }
        public bool DestinatarioElimina { get; set; }
    }
}