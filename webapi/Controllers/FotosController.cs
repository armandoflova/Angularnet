using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using webapi.Data;
using webapi.Dtos;
using webapi.Helpers;
using webapi.Models;

namespace webapi.Controllers
{
    [Authorize]
    [Route("api/usuarios/{usuarioId}/fotos")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        public ICitasAppRepositorio _repo { get; }
        public IMapper _mapper { get; }
        public IOptions<CloudinarySettings> _CloudinaryConfig { get; }

        private Cloudinary _cloudinary;
        public FotosController(ICitasAppRepositorio repo, IMapper mapper, IOptions<CloudinarySettings> CloudinaryConfig)
        {
            _CloudinaryConfig = CloudinaryConfig;
            _mapper = mapper;
            _repo = repo;
            
            Account acc = new Account
            (
                _CloudinaryConfig.Value.CloudName, 
                _CloudinaryConfig.Value.ApiKey,
                _CloudinaryConfig.Value.ApiSecret
            );

            _cloudinary  = new Cloudinary(acc);
        }
        [HttpGet("{id}" , Name = "ObtenerFoto")]
        public async Task<IActionResult> ObtenerFoto(int id) {
            var fotorepo = await _repo.ObtenerFoto(id);
            var foto = _mapper.Map<fotoReturnRepo>(fotorepo);
            return Ok(foto);
        }
        
        [HttpPost]
        public async Task<IActionResult> AgregarFoto(int UsuarioID,[FromForm]FotosCreacionDto fotosCreacionDto){
            if ( UsuarioID !=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
          var usuario = await _repo.ObtenerUsuario(UsuarioID);
          var archivo = fotosCreacionDto.File;
          var uploadResult = new ImageUploadResult();

          if(archivo.Length > 0) {
              using ( var stream = archivo.OpenReadStream())
              {
                  var uploadParamas = new ImageUploadParams()
                  {
                      File = new FileDescription(archivo.Name, stream),
                      Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                  };
                uploadResult = _cloudinary.Upload(uploadParamas);  
              }
          }

          fotosCreacionDto.Url = uploadResult.Uri.ToString();
          fotosCreacionDto.PublicId = uploadResult.PublicId;

          var foto = _mapper.Map<Foto>(fotosCreacionDto);

          if (!usuario.Fotos.Any(u => u.EsPrincipal))
            foto.EsPrincipal = true;

         usuario.Fotos.Add(foto);

         if(await _repo.GuardarTodo()){
             var fotoReturn = _mapper.Map<fotoReturnRepo>(foto);
             return CreatedAtRoute("ObtenerFoto" , new fotoReturnRepo{ Id = foto.Id}, fotoReturn);
         }

         return BadRequest("no se pudo cargar la foto");
        }
    [HttpPost("{id}/esPrincipal")]
    public async Task<IActionResult> setPrincipal(int UsuarioID, int id){
          if ( UsuarioID !=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
         var usuario =await _repo.ObtenerUsuario(UsuarioID);
    if (!usuario.Fotos.Any(f => f.Id == id))
            return Unauthorized();
    var FotoRepo = await _repo.ObtenerFoto(id);

    if (FotoRepo.EsPrincipal)
        return BadRequest("Esta foto ya es Principal");
    
    var fotoActual =await _repo.ObtenerFotoActual(UsuarioID);
        fotoActual.EsPrincipal = false;
        FotoRepo.EsPrincipal = true;

        if (await _repo.GuardarTodo())
            return NoContent();
    return BadRequest("no se pudo establecer como foto principal");

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> eliminarFoto(int UsuarioID, int id){
     if ( UsuarioID !=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
    var usuario =await _repo.ObtenerUsuario(UsuarioID);
    if (!usuario.Fotos.Any(f => f.Id == id))
            return Unauthorized();
    var FotoRepo = await _repo.ObtenerFoto(id);

    if (FotoRepo.EsPrincipal)
        return BadRequest("Esta foto Es Principal no se puede Eliminar");
    
    if (FotoRepo.PublicId != null)
    {
         var paramEliminar = new DeletionParams(FotoRepo.PublicId);

        var eliminarFoto = _cloudinary.Destroy(paramEliminar);

        if (eliminarFoto.Result == "ok")
                _repo.Eliminar(FotoRepo);
         if( await _repo.GuardarTodo())
              return Ok();
    }
    if (FotoRepo.PublicId == null){
        _repo.Eliminar(FotoRepo);
        if( await _repo.GuardarTodo())
              return Ok();
        }
   
    return BadRequest("ocurrio un error no se pudo eliminar foto");
    }
    }
}