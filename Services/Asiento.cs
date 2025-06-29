// using BackendCine.Entities.Models;
// using Microsoft.EntityFrameworkCore;
// namespace BackendCine.Services
// {

//     public class AsientoService
//     {
//         private readonly CineDbContext _context;

//         public AsientoService(CineDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<List<Asiento>> GetAsientosAsync()
//         {
//             return await _context.Asientos.Where(c => c.swt).ToListAsync();
//         }

//         public async Task<Asiento> GetAsientoByIdAsync(int id)
//         {
//             return await _context.Asientos.FindAsync(id);
//         }

//         public async Task AddAsientoAsync(Asiento Asiento)
//         {
//             _context.Asientos.Add(Asiento);
//             await _context.SaveChangesAsync();
//         }

//         public async Task<Asiento> UpdateAsientoAsync(Asiento Asiento)
//         {
//             var existingAsiento = await GetAsientoByIdAsync(Asiento.Id);
//             if (existingAsiento == null)
//             {
//                 throw new KeyNotFoundException($"Asiento with ID {Asiento.Id} not found.");
//             }
//             // Update properties as needed
//             existingAsiento.swt = Asiento.swt;
//             existingAsiento.IdSala = Asiento.IdSala;
//             existingAsiento.Fila = Asiento.Fila;
//             existingAsiento.Columna = Asiento.Columna;
//             existingAsiento.Estado = Asiento.Estado;
//             existingAsiento.usuario_modificacion = Asiento.usuario_modificacion;
//             existingAsiento.fecha_modificacion = DateTime.Now;
//             _context.Asientos.Update(existingAsiento);
//             await _context.SaveChangesAsync();
//             return existingAsiento;
//         }

//         public async Task<Asiento> DeleteAsientoAsync(int id)
//         {
//             var Asiento = await GetAsientoByIdAsync(id);
//             if (Asiento != null)
//             {
//                 Asiento.swt = false;
//                 _context.Asientos.Update(Asiento);
//                 await _context.SaveChangesAsync();
//             }
//             else
//             {
//                 throw new KeyNotFoundException($"Asiento with ID {id} not found.");
//             }
//             return Asiento;
//         }


//     }
// }