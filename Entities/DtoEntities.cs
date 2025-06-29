using System;
using BackendCine.Entities.Models;

namespace BackendCine.Entities{}

public class LoginParam
{
    public string NomUsuario { get; set; }
    public string Password { get; set; }
}
public class PeliculaXCartelera
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopsis { get; set; }
    public int Duracion { get; set; }
    public string Img { get; set; }
    public List<GeneroXPelicula> Generos { get; set; }
    public List<FuncionResumen> ProximasFunciones { get; set; }
}

public class FuncionResumen
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public string SalaNombre { get; set; }
    public decimal PrecioEntrada { get; set; }
    public int AsientosDisponibles { get; set; }

}
public class FuncionDetalleDto
{
    public int Id { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public Sala Sala { get; set; }
    public int AsientosDisponibles { get; set; }
    public int AsientosOcupados { get; set; }
    public decimal PrecioEntrada { get; set; }
}

public class PeliculaXSala
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopsis { get; set; }
    public int Duracion { get; set; }
    public string Img { get; set; }
    public List<Genero> Generos { get; set; }
    public List<Funcion> Funciones { get; set; }
}

public class CreateReserva
{
    public int IdCliente { get; set; }
    public int IdFuncion { get; set; }
    public int CantidadAsientos { get; set; }
    public decimal PrecioTotal { get; set; }
}
public class ListarReserva
{
    public int Id { get; set; }
    public string CodigoReserva { get; set; }
    public DateTime FechaReserva { get; set; }
    public Cliente Cliente { get; set; }
    public Funcion Funcion { get; set; }
    public int CantidadAsientos { get; set; }
    public decimal PrecioTotal { get; set; }
}

public class ListarPeliculasParam
{
    public string? titulo { get; set; }
    public int? idGenero { get; set; }
    public int? idSala { get; set; }
}