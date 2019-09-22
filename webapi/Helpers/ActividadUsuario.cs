using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using webapi.Data;

namespace webapi.Helpers
{
    public class ActividadUsuario : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            var usuarioId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<ICitasAppRepositorio>();
            var usuario = await repo.ObtenerUsuario(usuarioId);
            usuario.UltimaConexion = DateTime.Now;
            await repo.GuardarTodo();
        }
    }
}