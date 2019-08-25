using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
        
        public DbSet<Valores>  Valores {get;set;}  
        public DbSet<Usuario> Usuarios {get; set;}
    }
}