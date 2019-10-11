using System;

namespace webapi.Dtos
{
    public class MensajeCreado
    {
        public int RemitenteId { get; set; }
        public int DestinatarioId { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Contenido { get; set; }

        public MensajeCreado()
        {
            this.FechaEnvio = DateTime.Now;
        }
    }
}