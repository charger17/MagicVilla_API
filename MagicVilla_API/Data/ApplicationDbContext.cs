﻿using MagicVilla_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext: IdentityDbContext<UsuarioAplicacion>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioAplicacion> UsuariosAplicacion { get; set; }

        public DbSet<Villa> Villas { get; set; }

        public DbSet<NumeroVilla> NumeroVillas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
            {
                Id = 1,
                Nombre = "Villa Real",
                Detalle = "Detalle de la villa...",
                ImagenUrl = "",
                Ocupantes = 5,
                MetrosCuadrados = 50,
                Tarifa = 200,
                Amenidad = "",
                FechaCreacion = DateTime.Now,
                FechaActualiazcion = DateTime.Now,
            }, new Villa
            {
                Id = 2,
                Nombre = "Premium vista a la piscina",
                Detalle = "Detalle de la villa...",
                ImagenUrl = "",
                Ocupantes = 4,
                MetrosCuadrados = 40,
                Tarifa = 150,
                Amenidad = "",
                FechaCreacion = DateTime.Now,
                FechaActualiazcion = DateTime.Now,
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
