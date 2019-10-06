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

namespace webapi.Controllers
{
    [ServiceFilter(typeof(ActividadUsuario))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public ICitasAppRepositorio _repo { get; }
        private readonly IMapper _mapper;
        public UsuarioController(ICitasAppRepositorio repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerUsuarios([FromQuery]ParametrosUsuarios parametrosUsuarios)
        {
            var idUsuarioActual = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usuarioActual = await _repo.ObtenerUsuario(idUsuarioActual);
            parametrosUsuarios.Id = idUsuarioActual;
            if (string.IsNullOrEmpty(parametrosUsuarios.Genero)){
                parametrosUsuarios.Genero = usuarioActual.Genero == "Masculino" ? "Femenino" : "Masculino";
            }
            var usuarios = await _repo.ObtenerUsuarios(parametrosUsuarios);
            var usariosReturn = _mapper.Map<IEnumerable<UsuarioLista>>(usuarios);
            Response.AgregarPaginacion(usuarios.PaginaActual, usuarios.TamanoPagina,usuarios.Total, usuarios.TotalPaginas);
            return Ok(usariosReturn);
        }
        [HttpGet("{id}" , Name = "obtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _repo.ObtenerUsuario(id);
            var usuarioReturn = _mapper.Map<UsuarioDetalles>(usuario);
            return Ok(usuarioReturn);
        }
        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateUser(int id, UsuarioEditar UsuarioEditar)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
 
            var userFromRepo = await _repo.ObtenerUsuario(id);
            _mapper.Map(UsuarioEditar , userFromRepo);
 
            if (await _repo.GuardarTodo())
                return NoContent();
 
            throw new Exception($"Updating user {id} failed on save");
        }
    }
}