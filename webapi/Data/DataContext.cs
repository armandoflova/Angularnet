using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){}
        
        public DbSet<Valores>  Valores {get;set;}  
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Foto> Fotos {get; set;}
        public DbSet<Like> Likes { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});

            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)   
                .HasForeignKey( u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)   
                .HasForeignKey( u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Mensajes>()
                .HasOne(u => u.Remitente)
                .WithMany(u => u.MensajesEnviados)
                .OnDelete(DeleteBehavior.Restrict);

             builder.Entity<Mensajes>()
                .HasOne(u => u.Destinatario)
                .WithMany(u => u.MensajesRecibidos)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}