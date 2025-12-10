using ClientesAPI.Models;

namespace ClientesAPI.Interfaces
{
    public interface IServicioRepository
    {
        Task<IEnumerable<Servicio>> GetAllAsync();
        Task<Servicio> GetByIdAsync(int id);
        Task<Servicio> CreateAsync(Servicio servicio);
        Task<Servicio> UpdateAsync(int id, Servicio servicio);
        Task<bool> DeleteAsync(int id);
    }
}
