using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CategoriaRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await _dbcontext.Categorias.ToListAsync();
        }
    }
}
