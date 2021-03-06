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
            CreateMap<UsuarioEditar , Usuario>();
            CreateMap<Foto, fotoReturnRepo>();
            CreateMap<FotosCreacionDto, Foto>();
            CreateMap<UsuarioRegistro, Usuario>();
            CreateMap<MensajeCreado, Mensajes>().ReverseMap();
            CreateMap<Mensajes, MensajeReturn>()
                .ForMember(r => r.RemitenteUrl, opt => opt.MapFrom(u => u.Remitente.Fotos.FirstOrDefault(f => f.EsPrincipal).Url))
                .ForMember(d => d.DestinatarioUrl, opt => opt.MapFrom(u => u.Destinatario.Fotos.FirstOrDefault(f => f.EsPrincipal).Url));
                
        }
    }
}