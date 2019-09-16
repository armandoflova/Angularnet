using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable> ObtenerUsarios()
        {
            var usuarios =await _context.Usuarios.Include(F => F.Fotos).ToListAsync();
            return usuarios;
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
    }
}