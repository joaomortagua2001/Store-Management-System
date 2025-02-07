using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly ApplicationDbContext _context;

    public FavoritoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Obter todos os favoritos de um utilizador
    public async Task<IEnumerable<Favorito>> GetFavoritos(string utilizadorId)
    {
        return await _context.Favoritos
            .Include(f => f.Produto) // Inclui os detalhes do produto relacionado
            .Where(f => f.UtilizadorId == utilizadorId)
            .ToListAsync();
    }

    // Adicionar um produto aos favoritos
    public async Task<bool> AdicionarFavorito(int produtoId, string utilizadorId)
    {
        if (string.IsNullOrWhiteSpace(utilizadorId))
            throw new ArgumentException("O identificador do utilizador é obrigatório.", nameof(utilizadorId));

        // Verifica se o favorito já existe
        var favoritoExistente = await _context.Favoritos
            .FirstOrDefaultAsync(f => f.ProdutoId == produtoId && f.UtilizadorId == utilizadorId);

        if (favoritoExistente != null)
            return true; // Já está nos favoritos

        var novoFavorito = new Favorito
        {
            ProdutoId = produtoId,
            UtilizadorId = utilizadorId,
            Efavorito = true
        };

        await _context.Favoritos.AddAsync(novoFavorito);
        await _context.SaveChangesAsync();

        return true;
    }

    // Remover um produto dos favoritos
    public async Task<bool> RemoverFavorito(int produtoId, string utilizadorId)
    {
        if (string.IsNullOrWhiteSpace(utilizadorId))
            throw new ArgumentException("O identificador do utilizador é obrigatório.", nameof(utilizadorId));

        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(f => f.ProdutoId == produtoId && f.UtilizadorId == utilizadorId);

        if (favorito == null)
            return false; // Não encontrado

        _context.Favoritos.Remove(favorito);
        await _context.SaveChangesAsync();

        return true;
    }
}
