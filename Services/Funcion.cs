using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services
{
    public class FuncionService
    {
        private readonly CineDbContext _context;

        public FuncionService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Funcion>> GetFuncionesAsync()
        {
            return await _context.Funciones.Where(c => c.swt).ToListAsync();
        }

        public async Task<Funcion> GetFuncionByIdAsync(int id)
        {
            return await _context.Funciones.FindAsync(id);
        }

        public async Task AddFuncionAsync(Funcion funcion)
        {
            _context.Funciones.Add(funcion);
            await _context.SaveChangesAsync();
        }

        public async Task<Funcion> UpdateFuncionAsync(Funcion funcion)
        {
            var existingFuncion = await GetFuncionByIdAsync(funcion.Id);
            if (existingFuncion == null)
            {
                throw new KeyNotFoundException($"Funcion with ID {funcion.Id} not found.");
            }
            // Update properties as needed
            existingFuncion.IdSala = funcion.IdSala;
            existingFuncion.IdPelicula = funcion.IdPelicula;
            existingFuncion.Fecha = funcion.Fecha;
            existingFuncion.HoraInicio = funcion.HoraInicio;
            existingFuncion.HoraFin = funcion.HoraFin;
            existingFuncion.PrecioEntrada = funcion.PrecioEntrada;
            existingFuncion.swt = funcion.swt;
            existingFuncion.AsientosDisponibles = funcion.AsientosDisponibles;
            existingFuncion.Estado = funcion.Estado;
            existingFuncion.usuario_modificacion = funcion.usuario_modificacion;
            existingFuncion.fecha_modificacion = DateTime.Now;
            _context.Funciones.Update(funcion);
            await _context.SaveChangesAsync();
            return existingFuncion;
        }

        public async Task<Funcion> DeleteFuncionAsync(int id)
        {
            var funcion = await GetFuncionByIdAsync(id);
            if (funcion != null)
            {
                funcion.swt = false;
                _context.Funciones.Update(funcion);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Funcion with ID {id} not found.");
            }
            return funcion;
        }
    }
}