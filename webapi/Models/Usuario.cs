namespace webapi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string  Nombre { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}