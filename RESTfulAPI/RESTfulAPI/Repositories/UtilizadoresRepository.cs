using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public class UtilizadoresRepository : IUtilizadoresRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UtilizadoresRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExisteUtilizadorAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser?> ObterUtilizadorPorEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> CriarUtilizadorAsync(ApplicationUser utilizador, string password)
        {
            var result = await _userManager.CreateAsync(utilizador, password);
            return result.Succeeded;
        }

        public async Task<bool> AdicionarRoleAsync(ApplicationUser utilizador, string role)
        {
            var result = await _userManager.AddToRoleAsync(utilizador, role);
            return result.Succeeded;
        }
    }
}
