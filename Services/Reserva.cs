using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services
{
    public class ReservaService
    {
        private readonly CineDbContext _context;

        public ReservaService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> GetReservasAsync()
        {
            return await _context.Reservas.Where(c => c.swt).ToListAsync();
        }

        public async Task<Reserva> GetReservaByIdAsync(int id)
        {
            return await _context.Reservas.FindAsync(id);
        }

        public async Task AddReservaAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<Reserva> UpdateReservaAsync(Reserva reserva)
        {
            var existingReserva = await GetReservaByIdAsync(reserva.Id);
            if (existingReserva == null)
            {
                throw new KeyNotFoundException($"Reserva with ID {reserva.Id} not found.");
            }
            existingReserva.CodigoReserva = reserva.CodigoReserva;
            existingReserva.IdCliente = reserva.IdCliente;
            existingReserva.IdFuncion = reserva.IdFuncion;
            existingReserva.FechaReserva = reserva.FechaReserva;
            existingReserva.CantidadAsientos = reserva.CantidadAsientos;
            existingReserva.PrecioTotal = reserva.PrecioTotal;
            existingReserva.Estado = reserva.Estado;
            existingReserva.FechaExpiracion = reserva.FechaExpiracion;
            existingReserva.swt = reserva.swt;
            existingReserva.usuario_modificacion = reserva.usuario_modificacion;
            existingReserva.fecha_modificacion = DateTime.Now;
            _context.Reservas.Update(existingReserva);
            await _context.SaveChangesAsync();
            return existingReserva;
        }

        public async Task<Reserva> DeleteReservaAsync(int id)
        {
            var reserva = await GetReservaByIdAsync(id);
            if (reserva != null)
            {
                reserva.swt = false;
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();  
            }
            return reserva;
        }

        public async Task<ListarReserva> CreateReservaAsync(CreateReserva createReserva)
        {
            var codigoReserva = $"RSV-{createReserva.IdCliente}-{createReserva.IdFuncion}-{DateTime.Now:yyyyMMddHHmmssfff}";
            var existingFuncion = await _context.Funciones.FindAsync(createReserva.IdFuncion);
            if (existingFuncion == null)
            {
                throw new KeyNotFoundException($"Funcion with ID {createReserva.IdFuncion} not found.");
            }
            var reserva = new Reserva
            {
                CodigoReserva = codigoReserva,
                IdCliente = createReserva.IdCliente,
                IdFuncion = createReserva.IdFuncion,
                FechaReserva = DateOnly.FromDateTime(DateTime.Now),
                CantidadAsientos = createReserva.CantidadAsientos,
                PrecioTotal = createReserva.PrecioTotal,
                FechaExpiracion = existingFuncion.Fecha.AddDays(1), // Assuming expiration is 1 day after the function date
                Estado = "Habilitada",
                swt = true,
                usuario_creacion = 1,
                fecha_creacion = DateTime.Now
            };
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            // Update the function's available seats
            existingFuncion.AsientosDisponibles -= createReserva.CantidadAsientos;
            if (existingFuncion.AsientosDisponibles < 0)
            {
                throw new InvalidOperationException("Not enough available seats for this function.");
            }
            _context.Funciones.Update(existingFuncion);
            // Create ReservaXAsientos entries for the reserved seats
            for (int i = 0; i < createReserva.CantidadAsientos; i++)
            {
                var reservaAsiento = new ReservaXAsientos
                {
                    IdReserva = reserva.Id,
                    FechaReserva = DateOnly.FromDateTime(DateTime.Now),
                    Estado = "Reservado",
                    swt = true,
                    usuario_creacion = 1,
                    fecha_creacion = DateTime.Now
                };
                _context.ReservasXAsientos.Add(reservaAsiento);
            }
            // Save changes to both Reserva and Funcion
            await _context.SaveChangesAsync();
            // Return the created reservation as a ListarReserva DTO
            var funcionActual = await _context.Funciones
                .Include(f => f.Sala)
                .Include(f => f.Pelicula)
                .FirstOrDefaultAsync(f => f.Id == createReserva.IdFuncion);
            return new ListarReserva
            {
                Id = reserva.Id,
                CodigoReserva = reserva.CodigoReserva,
                FechaReserva = reserva.FechaReserva.ToDateTime(new TimeOnly(0, 0)),
                Cliente = await _context.Clientes.FindAsync(reserva.IdCliente),
                Funcion = funcionActual,
                CantidadAsientos = reserva.CantidadAsientos,
                PrecioTotal = reserva.PrecioTotal
            };
        }

    }
}