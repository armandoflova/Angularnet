using Microsoft.AspNetCore.Http;

namespace webapi.Helpers
{
    public static class Extensions
    {
        public static void AplicationError(this HttpResponse respuesta, string mensaje){
            respuesta.Headers.Add("Aplicacion-Error", mensaje);
            respuesta.Headers.Add("Acces-Control-Expose-Headers", "Aplicacion-Error");
            respuesta.Headers.Add("Acces-Control-Allow-origin", "*");
        }
    }
}