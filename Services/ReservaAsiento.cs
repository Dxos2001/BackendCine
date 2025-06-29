using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services
{
    public class ReservaAsientoService
    {
        private readonly CineDbContext _context;

        public ReservaAsientoService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservaXAsientos>> GetReservasAsientosAsync()
        {
            return await _context.ReservasXAsientos.Where(c => c.swt).ToListAsync();
        }

        public async Task<ReservaXAsientos> GetReservaAsientoByIdAsync(int id)
        {
            return await _context.ReservasXAsientos.FindAsync(id);
        }

        public async Task AddReservaAsientoAsync(ReservaXAsientos reservaAsiento)
        {
            _context.ReservasXAsientos.Add(reservaAsiento);
            await _context.SaveChangesAsync();
        }

        public async Task<ReservaXAsientos> UpdateReservaAsientoAsync(ReservaXAsientos reservaAsiento)
        {
            var existingReservaAsiento = await GetReservaAsientoByIdAsync(reservaAsiento.Id);
            if (existingReservaAsiento == null)
            {
                throw new KeyNotFoundException($"ReservaXAsientos with ID {reservaAsiento.Id} not found.");
            }
            existingReservaAsiento.IdReserva = reservaAsiento.IdReserva;
            existingReservaAsiento.FechaReserva = reservaAsiento.FechaReserva;
            existingReservaAsiento.Estado = reservaAsiento.Estado;
            existingReservaAsiento.swt = reservaAsiento.swt;
            existingReservaAsiento.usuario_modificacion = reservaAsiento.usuario_modificacion;
            existingReservaAsiento.fecha_modificacion = DateTime.Now;
            _context.ReservasXAsientos.Update(existingReservaAsiento);
            await _context.SaveChangesAsync();
            return existingReservaAsiento;
        }

        public async Task<ReservaXAsientos> DeleteReservaAsientoAsync(int id)
        {
            var reservaAsiento = await GetReservaAsientoByIdAsync(id);
            if (reservaAsiento != null)
            {
                reservaAsiento.swt = false;
                _context.ReservasXAsientos.Update(reservaAsiento);
                await _context.SaveChangesAsync();
            }
            return reservaAsiento;
        }
    }
}