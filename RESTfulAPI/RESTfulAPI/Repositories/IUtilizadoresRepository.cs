using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public interface IUtilizadoresRepository
    {
        Task<bool> ExisteUtilizadorAsync(string email);
        Task<ApplicationUser?> ObterUtilizadorPorEmailAsync(string email);
        Task<bool> CriarUtilizadorAsync(ApplicationUser utilizador, string password);
        Task<bool> AdicionarRoleAsync(ApplicationUser utilizador, string role);
    }
}
