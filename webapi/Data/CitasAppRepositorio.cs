using System;
using System.Collections;
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
    }
}