using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories;

public interface IFavoritoRepository
{
    Task<IEnumerable<Favorito>> GetFavoritos(string utilizadorId);
    Task<bool> AdicionarFavorito(int produtoId, string utilizadorId);
    Task<bool> RemoverFavorito(int produtoId, string utilizadorId);
}
