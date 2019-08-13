using Microsoft.EntityFrameworkCore;


namespace webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
        
        public DbSet<Valores>  Valores {get;set;}  
    }
}