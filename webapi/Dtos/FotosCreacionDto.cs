using System;
using Microsoft.AspNetCore.Http;

namespace webapi.Dtos
{
    public class FotosCreacionDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAgregada { get; set; }
        public string PublicId { get; set; }

          public FotosCreacionDto()
          {
              FechaAgregada = DateTime.Now;
          }
    }
}