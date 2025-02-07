using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories;
public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategorias();
}
