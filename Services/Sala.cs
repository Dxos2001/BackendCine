using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace BackendCine.Services
{
    public class SalaService
    {
        private readonly CineDbContext _context;

        public SalaService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sala>> GetSalasAsync()
        {
            return await _context.Salas.Where(c => c.swt).ToListAsync();
        }

        public async Task<Sala> GetSalaByIdAsync(int id)
        {
            return await _context.Salas.FindAsync(id);
        }

        public async Task AddSalaAsync(Sala Sala)
        {
            _context.Salas.Add(Sala);
            await _context.SaveChangesAsync();
        }

        public async Task<Sala> UpdateSalaAsync(Sala Sala)
        {
            var existingSala = await GetSalaByIdAsync(Sala.Id);
            if (existingSala == null)
            {
                throw new KeyNotFoundException($"Sala with ID {Sala.Id} not found.");
            }
            existingSala.Nombre = Sala.Nombre;
            existingSala.Capacidad = Sala.Capacidad;
            existingSala.Filas = Sala.Filas;
            existingSala.Columnas = Sala.Columnas;
            existingSala.swt = Sala.swt;
            existingSala.usuario_modificacion = Sala.usuario_modificacion;
            existingSala.fecha_modificacion = DateTime.Now;
            _context.Salas.Update(existingSala);
            await _context.SaveChangesAsync();
            return existingSala;
        }

        public async Task<Sala> DeleteSalaAsync(int id)
        {
            var Sala = await GetSalaByIdAsync(id);
            if (Sala != null)
            {
                Sala.swt = false;
                _context.Salas.Update(Sala);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Sala with ID {id} not found.");
            }
            return Sala;
        }
    }
}