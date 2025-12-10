using ClientesAPI.Models;

namespace ClientesAPI.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva?> GetByIdAsync(int id);
        Task<IEnumerable<Reserva>> GetByClienteIdAsync(int clienteId);
        Task<Reserva> CreateAsync(Reserva reserva);
        Task<Reserva?> UpdateAsync(int id, Reserva updated);
        Task<bool> DeleteAsync(int id);
    }
}
