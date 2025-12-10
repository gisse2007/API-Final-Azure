using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Interfaces;
using ClientesAPI.Models;

namespace ClientesAPI.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET ALL
        // ============================================================
        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync();
        }

        // ============================================================
        // GET BY ID
        // ============================================================
        public async Task<Reserva?> GetByIdAsync(int id)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        // ============================================================
        // GET BY CLIENTE ID
        // ============================================================
        public async Task<IEnumerable<Reserva>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Reservas
                .AsNoTracking()
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .OrderByDescending(r => r.FechaReserva)
                .ToListAsync();
        }

        // ============================================================
        // CREATE
        // ============================================================
        public async Task<Reserva> CreateAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        // ============================================================
        // UPDATE
        // ============================================================
        public async Task<Reserva?> UpdateAsync(int id, Reserva updated)
        {
            var existing = await _context.Reservas.FindAsync(id);
            if (existing == null) return null;

            // Campos principales
            existing.FechaEntrada = updated.FechaEntrada;
            existing.FechaSalida = updated.FechaSalida;
            existing.Habitacion = updated.Habitacion;
            existing.CantidadPersonas = updated.CantidadPersonas;

            // FKs y estado
            existing.ClienteId = updated.ClienteId;
            existing.ServicioId = updated.ServicioId;
            existing.Estado = updated.Estado;
            existing.Descripcion = updated.Descripcion;

            await _context.SaveChangesAsync();
            return existing;
        }

        // ============================================================
        // DELETE
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

