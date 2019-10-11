using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Dtos;
using webapi.Helpers;
using webapi.Models;

namespace webapi.Controllers
{
    [ServiceFilter(typeof(ActividadUsuario))]
    [Authorize]
    [Route("api/usuario/{usuarioId}/[controller]")]
    [ApiController]
    public class MensajesController : ControllerBase
    {
        public ICitasAppRepositorio _repo { get; }
        public IMapper _mapper { get; }
        public MensajesController(ICitasAppRepositorio repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }
        [HttpGet("{id}" , Name = "obtenerMensaje")]

        public async Task<IActionResult> obtenerMensaje(int usuarioId, int id)
        {
             if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var mensaje = await _repo.ObtenerMensaje(id);

            if(mensaje == null)
                return NotFound();
            
            return Ok(mensaje);
        }
        [HttpGet]
        public async Task<IActionResult> obtenerMensajesXUsuario(int usuarioId,[FromQuery] MensajeParams mensajeParams)
        {
            if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            mensajeParams.UsuarioId = usuarioId;

             var mensajesRepo = await _repo.ObtenerMensajesPorUsuario(mensajeParams);

             var mensajes = _mapper.Map<IEnumerable<MensajeReturn>>(mensajesRepo);

             Response.AgregarPaginacion(mensajesRepo.PaginaActual, mensajesRepo.TamanoPagina, mensajesRepo.Total, mensajesRepo.TotalPaginas);

             return Ok(mensajes);
        }
        [HttpGet("chat/{destinatarioId}")]

        public async Task<IActionResult> obtenerChat(int usuarioId , int destinatarioId)
        {
             if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var mensajesRepo = await _repo.ObtenerMensajesLeido(usuarioId , destinatarioId);

            var mensajes = _mapper.Map<IEnumerable<MensajeReturn>>(mensajesRepo);

            return Ok(mensajes);
        }

        [HttpPost]

        public async Task<IActionResult> enviarMensaje(int usuarioId ,MensajeCreado mensajeCreado)
        {
            var remitente = await _repo.ObtenerUsuario(usuarioId);
             if (remitente.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                 
            mensajeCreado.RemitenteId = usuarioId;

            var destinatario = await _repo.ObtenerUsuario(mensajeCreado.DestinatarioId);

            if(destinatario == null)
                return BadRequest("no se encontro destinatario");
            
            var mensaje = _mapper.Map<Mensajes>(mensajeCreado);
            
            _repo.Agregar(mensaje);

           if(await _repo.GuardarTodo())
            {
                var mensajeReturn = _mapper.Map<MensajeReturn>(mensaje);
                 return CreatedAtRoute("obtenerMensaje", new {id = mensaje.Id}, mensajeReturn);
            }
               
            throw new Exception("No se pudo guardar mensaje");
        }

     [HttpPost("{id}")]

    public async Task<IActionResult> EliminarMensaje(int id, int usuarioId)
    {
          if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();
        
        var mensajeRepo= await _repo.ObtenerMensaje(id);

        if(mensajeRepo.RemitenteId == usuarioId)
            mensajeRepo.RemitenteElimina = true;
        if(mensajeRepo.DestinatarioId == usuarioId)
            mensajeRepo.DestinatarioElimina = true;
        if(mensajeRepo.RemitenteElimina && mensajeRepo.DestinatarioElimina)
            _repo.Eliminar(mensajeRepo);
        
        if(await _repo.GuardarTodo())
            return NoContent();
        
        throw new Exception("No se pudo eliminar el mensaje");
    }

    [HttpPost("Leido/{id}")]
    public async Task<IActionResult> Leido(int usuarioId,int id)
    {
        
          if (usuarioId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();
        
         var mensajeRepo = await _repo.ObtenerMensaje(id);

         if(mensajeRepo.RemitenteId != usuarioId)
            return Unauthorized();
        
        mensajeRepo.EstaLeido = true;
        mensajeRepo.FechaLectura = DateTime.Now;

       await _repo.GuardarTodo();
        return NoContent();
    }
    }

    
}