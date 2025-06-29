using System;
using System.Security.Cryptography;
using BackendCine.Entities;
using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services;

public class CarteleraService
{
    private readonly CineDbContext _context;
    public CarteleraService(CineDbContext context)
    {
        _context = context;
    }
    public async Task<List<PeliculaXSala>> GetPeliculaXSala(int? idPelicula)
    {
        using (var cn = new CineDbContext())
        {
            var query = from p in cn.Peliculas
                        where p.swt && (p.Id == idPelicula || idPelicula == null || idPelicula == 0)
                        orderby p.Titulo
                        select new PeliculaXSala
                        {
                            Id = p.Id,
                            Titulo = p.Titulo,
                            Sinopsis = p.Sinopsis,
                            Duracion = p.Duracion,
                            Img = p.Img,
                            Generos = cn.GenerosXPeliculas
                                .Where(gp => gp.IdPelicula == p.Id && gp.swt && (p.Id == idPelicula || idPelicula == null || idPelicula == 0))
                                .Select(gp => gp.Genero)
                                .ToList(),
                            Funciones = cn.Funciones
                                .Where(func => func.IdPelicula == p.Id && func.swt)
                                .Select(func => new Funcion
                                {
                                    Id = func.Id,
                                    Fecha = func.Fecha,
                                    HoraInicio = func.HoraInicio,
                                    HoraFin = func.HoraFin,
                                    IdSala = func.IdSala,
                                    Sala = func.Sala,
                                    Pelicula = func.Pelicula,
                                    PrecioEntrada = func.PrecioEntrada,
                                    AsientosDisponibles = func.AsientosDisponibles,
                                    Estado = func.Estado
                                })
                                .ToList()

                        };
            return await query.ToListAsync();
        }
    }

    public async Task<List<PeliculaXSala>> GetPeliculaByParam(ListarPeliculasParam? param)
    {
        using (var cn = new CineDbContext())
        {
            var query = from p in cn.Peliculas
                        where p.swt &&
                            (
                                // Mostrar todo si todos los parámetros son null
                                (param == null || 
                                (param.titulo == null && param.idGenero == null && param.idSala == null))

                                // Filtros individuales si algún parámetro tiene valor
                                || (!string.IsNullOrEmpty(param.titulo) &&
                                    EF.Functions.Like(p.Titulo, $"%{param.titulo}%"))
                                || (param.idGenero != null &&
                                    cn.GenerosXPeliculas.Any(gp =>
                                        gp.IdPelicula == p.Id &&
                                        gp.swt &&
                                        gp.IdGenero == param.idGenero))
                                || (param.idSala != null &&
                                    cn.Funciones.Any(f =>
                                        f.IdPelicula == p.Id &&
                                        f.swt &&
                                        f.IdSala == param.idSala))
                            )
                        orderby p.Titulo
                        select new PeliculaXSala
                        {
                            Id       = p.Id,
                            Titulo   = p.Titulo,
                            Sinopsis = p.Sinopsis,
                            Duracion = p.Duracion,
                            Img      = p.Img,

                            Generos = cn.GenerosXPeliculas
                                .Where(gp => gp.IdPelicula == p.Id &&
                                            gp.swt &&
                                            (param.idGenero == null || gp.IdGenero == param.idGenero))
                                .Select(gp => gp.Genero)
                                .ToList(),

                            Funciones = cn.Funciones
                                .Where(func => func.IdPelicula == p.Id &&
                                            func.swt &&
                                            (param.idSala == null || func.IdSala == param.idSala))
                                .Select(func => new Funcion
                                {
                                    Id                  = func.Id,
                                    Fecha               = func.Fecha,
                                    HoraInicio          = func.HoraInicio,
                                    HoraFin             = func.HoraFin,
                                    IdSala              = func.IdSala,
                                    Sala                = func.Sala,
                                    Pelicula            = func.Pelicula,
                                    PrecioEntrada       = func.PrecioEntrada,
                                    AsientosDisponibles = func.AsientosDisponibles,
                                    Estado              = func.Estado
                                })
                                .ToList()
                        };

            return await query.ToListAsync();
        }
    }
}

