using ClientesAPI.Models;

namespace ClientesAPI.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<Cliente?> GetByEmailAsync(string email);
        Task<IEnumerable<Cliente>> GetActiveAsync();
        Task<Cliente> CreateAsync(Cliente cliente);
        Task<Cliente?> UpdateAsync(int id, Cliente cliente);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    }
}