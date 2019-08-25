using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Data
{
    public interface IAuthRepository
    {
         Task<Usuario> Registro(Usuario usuario, string password);
         Task<Usuario> Login(string nombre , string password);

         Task<bool> ExisteUsuario(string nombre);
    }
}