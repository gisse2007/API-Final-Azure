using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;

namespace ClientesAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes
                .Include(c => c.Reservas)
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<Cliente?> GetByEmailAsync(string email)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Cliente>> GetActiveAsync()
        {
            return await _context.Clientes
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            cliente.FechaCreacion = DateTime.Now;

            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(int id, Cliente cliente)
        {
            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = cliente.Nombre;
            existing.Email = cliente.Email;
            existing.Telefono = cliente.Telefono;
            existing.Direccion = cliente.Direccion;
            existing.Activo = cliente.Activo;
            existing.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            cliente.Activo = false;
            cliente.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.ClienteId == id);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            return await _context.Clientes
                .AnyAsync(c => c.Email == email && (!excludeId.HasValue || c.ClienteId != excludeId.Value));
        }
    }
}
