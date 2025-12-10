using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;

namespace ClientesAPI.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servicio>> GetAllAsync()
        {
            return await _context.Servicios
                .Where(s => s.Activo)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }

        public async Task<Servicio?> GetByIdAsync(int id)
        {
            return await _context.Servicios
                .Include(s => s.Reservas)
                .FirstOrDefaultAsync(s => s.ServicioId == id);
        }

        public async Task<Servicio> CreateAsync(Servicio servicio)
        {
            servicio.FechaCreacion = DateTime.Now;

            await _context.Servicios.AddAsync(servicio);
            await _context.SaveChangesAsync();

            return servicio;
        }

        public async Task<Servicio?> UpdateAsync(int id, Servicio updated)
        {
            var existing = await _context.Servicios.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = updated.Nombre;
            existing.Descripcion = updated.Descripcion;
            existing.Precio = updated.Precio;
            existing.DuracionMinutos = updated.DuracionMinutos;
            existing.Activo = updated.Activo;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null) return false;

            servicio.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
