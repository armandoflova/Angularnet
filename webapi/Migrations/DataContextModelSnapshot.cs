﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data;

namespace api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("webapi.Data.Valores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nombre");

                    b.HasKey("Id");

                    b.ToTable("Valores");
                });

            modelBuilder.Entity("webapi.Models.Foto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<bool>("EsPrincipal");

                    b.Property<DateTime>("FechaAgreaga");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UsuarioID");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Fotos");
                });

            modelBuilder.Entity("webapi.Models.Like", b =>
                {
                    b.Property<int>("LikerId");

                    b.Property<int>("LikeeId");

                    b.HasKey("LikerId", "LikeeId");

                    b.HasIndex("LikeeId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("webapi.Models.Mensajes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contenido");

                    b.Property<bool>("DestinatarioElimina");

                    b.Property<int>("DestinatarioId");

                    b.Property<bool>("EstaLeido");

                    b.Property<DateTime>("FechaEnvio");

                    b.Property<DateTime?>("FechaLectura");

                    b.Property<bool>("RemitenteElimina");

                    b.Property<int>("RemitenteId");

                    b.HasKey("Id");

                    b.HasIndex("DestinatarioId");

                    b.HasIndex("RemitenteId");

                    b.ToTable("Mensajes");
                });

            modelBuilder.Entity("webapi.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias");

                    b.Property<string>("BuscarPor");

                    b.Property<string>("City");

                    b.Property<DateTime>("Creado");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Genero");

                    b.Property<string>("Intereses");

                    b.Property<string>("Introduccion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Pais");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<DateTime>("UltimaConexion");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("webapi.Models.Foto", b =>
                {
                    b.HasOne("webapi.Models.Usuario", "Usuario")
                        .WithMany("Fotos")
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("webapi.Models.Like", b =>
                {
                    b.HasOne("webapi.Models.Usuario", "Likee")
                        .WithMany("Likers")
                        .HasForeignKey("LikeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("webapi.Models.Usuario", "Liker")
                        .WithMany("Likees")
                        .HasForeignKey("LikerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("webapi.Models.Mensajes", b =>
                {
                    b.HasOne("webapi.Models.Usuario", "Destinatario")
                        .WithMany("MensajesRecibidos")
                        .HasForeignKey("DestinatarioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("webapi.Models.Usuario", "Remitente")
                        .WithMany("MensajesEnviados")
                        .HasForeignKey("RemitenteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
