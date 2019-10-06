using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace webapi.Helpers
{
    public static class Extensions
    {
        public static void AplicationError(this HttpResponse response, string mensaje){
            response.Headers.Add("Aplicacion-Error", mensaje);
            response.Headers.Add("Access-Control-Expose-Headers", "Aplicacion-Error");
            response.Headers.Add("Access-Control-Expose-Headers", "*");
        }

        public static void AgregarPaginacion(this HttpResponse response, int paginaActual, int itemsXPagina, int totalItems, int totalpaginas)
        {
            var encabezadoPagina = new paginaEncabezado(paginaActual, itemsXPagina, totalItems, totalpaginas);
            var camelcaseFormater =new JsonSerializerSettings();
            camelcaseFormater.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Paginacion", JsonConvert.SerializeObject(encabezadoPagina, camelcaseFormater));
            response.Headers.Add("Access-Control-Expose-Headers", "Paginacion");
            
        }
        public static int CalcularEdad(this DateTime fecha){
            var edad = DateTime.Today.Year - fecha.Year;
            if (DateTime.Today.Year > fecha.Year){
                edad --;
            }
            return edad;
        }
    }
}