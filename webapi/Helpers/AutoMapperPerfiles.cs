using System.Linq;
using AutoMapper;
using webapi.Dtos;
using webapi.Models;

namespace webapi.Helpers
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles()
        {
            CreateMap<Usuario , UsuarioLista>()
            .ForMember(dest => dest.Url , opt => opt.MapFrom(src => 
            src.Fotos.FirstOrDefault(f => f.EsPrincipal).Url))
            .ForMember(dest => dest.Edad , opt => opt.MapFrom( src => 
            src.FechaNacimiento.CalcularEdad()));
            CreateMap<Usuario , UsuarioDetalles>()
            .ForMember(dest => dest.Url , opt => opt.MapFrom(src => 
            src.Fotos.FirstOrDefault(f => f.EsPrincipal).Url))
            .ForMember(dest => dest.Edad , opt => opt.MapFrom( src => 
            src.FechaNacimiento.CalcularEdad())); 
            CreateMap<Foto , FotosDetalles>();
        }
    }
}