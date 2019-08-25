using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config= config;
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
            var usuario = await _repo.Login(usuariologin.nombre.ToLower(), usuariologin.password);
            if (usuario == null){
                return Unauthorized();
            }
            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier , usuario.Id.ToString()),
             new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config.GetSection("AppSettings:token").Value));

            var creds = new SigningCredentials( key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor  = new SecurityTokenDescriptor
            {
                Subject = new  ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenhandler.WriteToken(token)
            });


        }
    }
}