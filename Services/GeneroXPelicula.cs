using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services
{
    public class GeneroXPeliculaService
    {
        private readonly CineDbContext _context;

        public GeneroXPeliculaService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<GeneroXPelicula>> GetGenerosXPeliculasAsync()
        {
            return await _context.GenerosXPeliculas.Where(c => c.swt).ToListAsync();
        }

        public async Task<GeneroXPelicula> GetGeneroXPeliculaByIdAsync(int id)
        {
            return await _context.GenerosXPeliculas.FindAsync(id);
        }

        public async Task AddGeneroXPeliculaAsync(GeneroXPelicula generoXPelicula)
        {
            _context.GenerosXPeliculas.Add(generoXPelicula);
            await _context.SaveChangesAsync();
        }

        public async Task<GeneroXPelicula> UpdateGeneroXPeliculaAsync(GeneroXPelicula generoXPelicula)
        {
            var existingGeneroXPelicula = await GetGeneroXPeliculaByIdAsync(generoXPelicula.Id);
            if (existingGeneroXPelicula == null)
            {
                throw new KeyNotFoundException($"GeneroXPelicula with ID {generoXPelicula.Id} not found.");
            }
            existingGeneroXPelicula.IdPelicula = generoXPelicula.IdPelicula;
            existingGeneroXPelicula.IdGenero = generoXPelicula.IdGenero;
            existingGeneroXPelicula.swt = generoXPelicula.swt;
            existingGeneroXPelicula.usuario_modificacion = generoXPelicula.usuario_modificacion;
            existingGeneroXPelicula.fecha_modificacion = DateTime.Now;
            _context.GenerosXPeliculas.Update(existingGeneroXPelicula);
            await _context.SaveChangesAsync();
            return existingGeneroXPelicula;
        }

        public async Task<GeneroXPelicula> DeleteGeneroXPeliculaAsync(int id)
        {
            var generoXPelicula = await GetGeneroXPeliculaByIdAsync(id);
            if (generoXPelicula != null)
            {
                generoXPelicula.swt = false;
                _context.GenerosXPeliculas.Update(generoXPelicula);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"GeneroXPelicula with ID {id} not found.");
            }
            return generoXPelicula;
        }
    }
}