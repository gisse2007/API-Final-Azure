using ClientesAPI.Models;

namespace ClientesAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario?> LoginAsync(string email, string password);
        Task<Usuario> RegisterAsync(Usuario usuario, string password);
        Task<bool> UserExistsAsync(string email);
        string CreateToken(Usuario usuario);
    }
}
