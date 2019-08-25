using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _context { get; }
        public AuthRepository(DataContext context)
        {
           _context = context;

        }
        public async Task<bool> ExisteUsuario(string nombre)
        {
           if ( await _context.Usuarios.AnyAsync( x => x.Nombre == nombre))
            return true;
        return false;
        }

        public async  Task<Usuario> Login(string nombre, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == nombre);
            if(usuario == null)
                return null;
            if (!verificarPasswoordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
             return null;

            return usuario;        
        }

        private bool verificarPasswoordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using ( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
           {
              var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
              for (int i =0 ; i< ComputeHash.Length; i++)
              {
                if(ComputeHash[i] != passwordHash[i]) return false;
              }
              return true;
           
            }
           
        }

        public async Task<Usuario> Registro(Usuario usuario, string password)
        {
            byte[] passwordHash, PasswordSalt;

            CrearPasswordHash(password, out passwordHash, out PasswordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = PasswordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}