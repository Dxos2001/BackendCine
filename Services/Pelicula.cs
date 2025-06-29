using Microsoft.EntityFrameworkCore;

namespace BackendCine.Entities.Models
{
    public class PeliculaService
    {
        private readonly CineDbContext _context;

        public PeliculaService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            return await _context.Peliculas.Where(p => p.swt).ToListAsync();
        }

        public async Task<Pelicula> GetPeliculaByIdAsync(int id)
        {
            return await _context.Peliculas.FindAsync(id);
        }

        public async Task AddPeliculaAsync(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
        }

        public async Task<Pelicula> UpdatePeliculaAsync(Pelicula pelicula)
        {
            var existingPelicula = await GetPeliculaByIdAsync(pelicula.Id);
            if (existingPelicula == null)
            {
                throw new KeyNotFoundException($"Pelicula with ID {pelicula.Id} not found.");
            }
            // Update properties as needed
            existingPelicula.Titulo = pelicula.Titulo;
            existingPelicula.Sinopsis = pelicula.Sinopsis;
            existingPelicula.Duracion = pelicula.Duracion;
            existingPelicula.Img = pelicula.Img;
            existingPelicula.swt = pelicula.swt;
            existingPelicula.usuario_modificacion = pelicula.usuario_modificacion;
            existingPelicula.fecha_modificacion = DateTime.Now;
            _context.Peliculas.Update(existingPelicula);
            await _context.SaveChangesAsync();
            return existingPelicula;
        }

        public async Task<Pelicula> DeletePeliculaAsync(int id)
        {
            var pelicula = await GetPeliculaByIdAsync(id);
            if (pelicula != null)
            {
                pelicula.swt = false;
                _context.Peliculas.Update(pelicula);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Pelicula with ID {id} not found.");
            }
            return pelicula;
        }
    }
}