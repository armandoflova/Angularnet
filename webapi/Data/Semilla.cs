using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using webapi.Models;

namespace webapi.Data
{
    public class Semilla
    {
        public static void SemillaUsuario(DataContext context)
        {
            if(!context.Usuarios.Any()){
                var usuarioData = System.IO.File.ReadAllText("Data/Usuario.Generados.json");
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(usuarioData);

                foreach( var usuario in usuarios){
                    byte[] passwordHash , passwordSalt;
                     CrearPasswordHash("password", out passwordHash, out passwordSalt);

                     usuario.PasswordHash = passwordHash;
                     usuario.PasswordSalt = passwordSalt;
                     usuario.Nombre = usuario.Nombre.ToLower();
                     context.Add(usuario);
                }

                context.SaveChanges();
            }
        }

          private static void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}