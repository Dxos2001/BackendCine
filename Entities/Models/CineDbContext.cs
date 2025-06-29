using System;
using System.Collections.Generic;
using Company.TestProject1;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BackendCine.Entities.Models;

public partial class CineDbContext : DbContext
{
    public CineDbContext()
    {
    }

    public CineDbContext(DbContextOptions<CineDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; } = null!;
    public virtual DbSet<Sala> Salas { get; set; } = null!;
    public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
    public virtual DbSet<Genero> Generos { get; set; } = null!;
    public virtual DbSet<GeneroXPelicula> GenerosXPeliculas { get; set; } = null!;
    public virtual DbSet<Funcion> Funciones { get; set; } = null!;
    public virtual DbSet<Reserva> Reservas { get; set; } = null!;
    public virtual DbSet<ReservaXAsientos> ReservasXAsientos { get; set; } = null!;
    public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
