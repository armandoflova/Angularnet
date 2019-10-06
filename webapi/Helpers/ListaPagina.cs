using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace webapi.Helpers
{
    public class ListaPagina<T> : List<T> 
    {
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; }   
        public int Total { get; set; }

        public ListaPagina(List<T> items, int contador, int numeropaginas, int tamanopagina)
        {
           Total = contador;
          TamanoPagina = tamanopagina;
          PaginaActual = numeropaginas;
          TotalPaginas = (int)Math.Ceiling(contador/(double)tamanopagina);
          this.AddRange(items);
        }

        public static async Task<ListaPagina<T>> CrearAsync(IQueryable<T> fuente, int numeropagina,int tamanopagina)
        {
            var contador = await fuente.CountAsync();
            var items = await fuente.Skip((numeropagina-1) * tamanopagina).Take(tamanopagina).ToListAsync();
            return new ListaPagina<T>(items , contador, numeropagina, tamanopagina); 
        }
    }
}