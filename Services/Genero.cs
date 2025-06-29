using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace BackendCine.Services
{

    public class GeneroService
    {
        private readonly CineDbContext _context;

        public GeneroService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await _context.Generos.Where(c => c.swt).ToListAsync();
        }

        public async Task<Genero> GetGeneroByIdAsync(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task AddGeneroAsync(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
        }

        public async Task<Genero> UpdateGeneroAsync(Genero genero)
        {
            var existingGenero = await GetGeneroByIdAsync(genero.Id);
            if (existingGenero == null)
            {
                throw new KeyNotFoundException($"Genero with ID {genero.Id} not found.");
            }
            // Update properties as needed
            existingGenero.Nombre = genero.Nombre;
            existingGenero.usuario_modificacion = genero.usuario_modificacion;
            existingGenero.fecha_modificacion = DateTime.Now;
            _context.Generos.Update(existingGenero);
            await _context.SaveChangesAsync();
            return existingGenero;
        }

        public async Task<Genero> DeleteGeneroAsync(int id)
        {
            var genero = await GetGeneroByIdAsync(id);
            if (genero != null)
            {
                genero.swt = false;
                _context.Generos.Update(genero);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Genero with ID {id} not found.");
            }
            return genero;
        }
    }
}