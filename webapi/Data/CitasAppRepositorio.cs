using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Helpers;
using webapi.Models;

namespace webapi.Data
{
    public class CitasAppRepositorio : ICitasAppRepositorio
    {
        public DataContext _context { get; }
        public CitasAppRepositorio(DataContext context)
        {
            _context = context;

        }
        public void Agregar<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Eliminar<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }

        public async Task<bool> GuardarTodo()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            var usuario =await _context.Usuarios.Include(F => F.Fotos).FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<Foto> ObtenerFoto(int id)
        {
            var foto = await _context.Fotos.FirstOrDefaultAsync(f => f.Id == id);
            return foto;
        }

        public async Task<Foto> ObtenerFotoActual(int idUsario)
        {
            var fotoActual = await _context.Fotos.Where(u => u.UsuarioID == idUsario).FirstOrDefaultAsync(f => f.EsPrincipal);
            return fotoActual;
        }

        public async Task<ListaPagina<Usuario>> ObtenerUsuarios(ParametrosUsuarios parametosUsuario)
        {
            var usuarios = _context.Usuarios.Include(f => f.Fotos).OrderByDescending(u => u.UltimaConexion).AsQueryable();
             usuarios = usuarios.Where(u => u.Id != parametosUsuario.Id);
             usuarios = usuarios.Where(u => u.Genero == parametosUsuario.Genero);
             if(parametosUsuario.Likers)
             {
                var usuarioLikers = await obtenerUsuarioLikes(parametosUsuario.Id, parametosUsuario.Likers);
                usuarios = usuarios.Where(u => usuarioLikers.Contains(u.Id));
             } 
             if(parametosUsuario.Likees) 
             {
                var usuarioLikees = await obtenerUsuarioLikes(parametosUsuario.Id, parametosUsuario.Likers);
                usuarios = usuarios.Where(u => usuarioLikees.Contains(u.Id));
             }
            if(parametosUsuario.MinEdad != 18 || parametosUsuario.MaxEdad != 99) {
                var minDob = DateTime.Today.AddYears(-parametosUsuario.MaxEdad -1);
                var maxDob = DateTime.Today.AddYears(-parametosUsuario.MinEdad);

                usuarios = usuarios.Where(u => u.FechaNacimiento >= minDob && u.FechaNacimiento <= maxDob);
            }

            if(!string.IsNullOrEmpty(parametosUsuario.ordenarPor))
            {
                switch (parametosUsuario.ordenarPor)
                {
                    case "Creado":
                    usuarios = usuarios.OrderByDescending(u => u.Creado);
                    break;
                    default:
                    usuarios = usuarios.OrderByDescending(u => u.UltimaConexion);
                    break;
                }
            }
            return await ListaPagina<Usuario>.CrearAsync(usuarios ,parametosUsuario.NumeroPaginas, parametosUsuario.TamanoPagina);
        }

        public async Task<Like> ObtenerLike(int idUsuario, int recipienteId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.LikerId == idUsuario && u.LikeeId == recipienteId);
        }

        private async Task<IEnumerable<int>> obtenerUsuarioLikes(int id, bool likers)
        {
            var usuario = await _context.Usuarios
            .Include( u => u.Likers).
            Include(u => u.Likees).FirstOrDefaultAsync(u => u.Id == id);

            if( likers)
            {
                return usuario.Likers.Where(u => u.LikeeId == id).Select(i => i.LikerId);
            }
            else
            {
                return usuario.Likees.Where(u => u.LikerId == id).Select(i => i.LikeeId);
            }
        }

        public async Task<Mensajes> ObtenerMensaje(int id)
        {
           var mensaje = await _context.Mensajes.FirstOrDefaultAsync(m => m.Id == id);
           return mensaje;
        }

        public async Task<ListaPagina<Mensajes>> ObtenerMensajesPorUsuario(MensajeParams mensajeParams)
        {
            var mensajes = _context.Mensajes.Include(u =>u.Remitente)
                            .ThenInclude(f => f.Fotos)
                            .Include(u =>u.Remitente).ThenInclude(f => f.Fotos)
                            .Include(u => u.Destinatario).ThenInclude(f => f.Fotos).AsQueryable();
            switch(mensajeParams.TipoContenido)
                {
                    case "Inbox":
                        mensajes = mensajes.Where(u => u.RemitenteId == mensajeParams.UsuarioId && u.RemitenteElimina == false);
                    break;
                    case "Outbox":
                        mensajes = mensajes.Where(u => u.DestinatarioId == mensajeParams.UsuarioId && u.DestinatarioElimina == false);
                    break;
                    default:
                     mensajes = mensajes.Where(u => u.RemitenteId == mensajeParams.UsuarioId &&  u.RemitenteElimina == false && u.EstaLeido == false);
                    break;
                }
            mensajes = mensajes.OrderByDescending(m => m.FechaEnvio);

            return await ListaPagina<Mensajes>.CrearAsync(mensajes , mensajeParams.NumeroPaginas, mensajeParams.TamanoPagina);
        }

        public async Task<IEnumerable<Mensajes>> ObtenerMensajesLeido(int usuarioId, int remitenteId)
        {
            var mensajes =await _context.Mensajes.Include(u =>u.Remitente)
                            .ThenInclude(f => f.Fotos)
                            .Include(u =>u.Remitente).ThenInclude(f => f.Fotos).
                            Where(m => m.RemitenteId == usuarioId && m.RemitenteElimina == false && m.DestinatarioId == remitenteId 
                            || m.DestinatarioId ==usuarioId && m.DestinatarioElimina == false && m.RemitenteId == remitenteId)
                            .OrderByDescending(m => m.FechaEnvio).ToListAsync();
            return mensajes;
        }
    }
}