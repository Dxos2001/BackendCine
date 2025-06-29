using System;
using BackendCine.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendCine.Services;

public class General
{
    private readonly CineDbContext _context;
    public General(CineDbContext context)
    {
        _context = context;
    }
    public async Task ExpirarReservasVencidas()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var reservasVencidas = await _context.Reservas.Where(r => r.swt && r.FechaExpiracion < today)
        .Include(r => r.Funcion)
        .ToListAsync();
        foreach (var reserva in reservasVencidas)
        {
            reserva.Estado = "Vencida";
            reserva.swt = false; // Mark as inactive
            _context.Reservas.Update(reserva);
        }
        await _context.SaveChangesAsync();
    }

  
}
