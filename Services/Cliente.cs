using Microsoft.EntityFrameworkCore;

namespace BackendCine.Entities.Models
{
    public class ClienteService
    {
        private readonly CineDbContext _context;

        public ClienteService(CineDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.Where(c => c.swt).ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            var existingCliente = await GetClienteByIdAsync(cliente.Id);
            if (existingCliente == null)
            {
                throw new KeyNotFoundException($"Cliente with ID {cliente.Id} not found.");
            }
            // Update properties as needed
            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Apellido = cliente.Apellido;
            existingCliente.Documento = cliente.Documento;
            existingCliente.FechaNacimiento = cliente.FechaNacimiento;
            existingCliente.Genero = cliente.Genero;
            existingCliente.Estado = cliente.Estado;
            existingCliente.swt = cliente.swt;
            existingCliente.usuario_modificacion = cliente.usuario_modificacion;
            existingCliente.fecha_modificacion = DateTime.Now;
            _context.Clientes.Update(existingCliente);
            await _context.SaveChangesAsync();
            return existingCliente;
        }

        public async Task<Cliente> DeleteClienteAsync(int id)
        {
            var cliente = await GetClienteByIdAsync(id);
            if (cliente != null)
            {
                cliente.swt = false;
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Cliente with ID {id} not found.");
            }
            return cliente;
        }
    }
}