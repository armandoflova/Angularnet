using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using webapi.Data;
using webapi.Dtos;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthRepository _repo { get; }
        public IConfiguration _config { get; }
        public IMapper _mapper { get; }
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;

        }

        [HttpPost("Registro")]

        public async Task<IActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {

            usuarioRegistro.nombre = usuarioRegistro.nombre.ToLower();

            if (await _repo.ExisteUsuario(usuarioRegistro.nombre))
                return BadRequest("Ya existe nombre de Usuario");

            var usuario = new Usuario
            {
                Nombre = usuarioRegistro.nombre
            };

            var crearUsuario = await _repo.Registro(usuario, usuarioRegistro.password);

            return StatusCode(201);

        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(UsuarioLogin usuariologin)
        {
            var usuariorepo = await _repo.Login(usuariologin.nombre.ToLower(), usuariologin.password);
            if (usuariorepo == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , usuariorepo.Id.ToString()),
                new Claim(ClaimTypes.Name, usuariorepo.Nombre)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config.GetSection("AppSettings:token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);
            var usuario = _mapper.Map<UsuarioLista>(usuariorepo);
            return Ok(new
            {
                token = tokenhandler.WriteToken(token), 
                usuario
            });
        }
    }
}