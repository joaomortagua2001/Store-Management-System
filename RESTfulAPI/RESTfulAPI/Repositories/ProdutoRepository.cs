using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public ProdutoRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId)
        {
            return await _dbcontext.Produtos
                .Where(p => p.CategoriaId == categoriaId)
                .Where(x => x.Imagem.Length > 0)
                .Include(p => p.modoentrega)
                .Include(p => p.categoria)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync()
        {
            return await _dbcontext.Produtos
                .Where(p => p.Promocao == true)
                .Where(x => x.Imagem.Length > 0)
                .Include(p => p.modoentrega)
                .Include(p => p.categoria)
                .OrderBy(p => p.categoria.Ordem)
                .ThenBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Produto?> ObterProdutoPorIdAsync(int produtoId)
        {
            return await _dbcontext.Produtos
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync()
        {
            return await _dbcontext.Produtos
                .Where(p => p.MaisVendido)
                .Where(x => x.Imagem.Length > 0)
                .Include(p => p.modoentrega)
                .Include(p => p.categoria)
                .OrderBy(p => p.categoria.Ordem)
                .ThenBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            return await _dbcontext.Produtos
                .Where(x => x.Imagem.Length > 0)
                .Include(p => p.modoentrega)
                .Include(p => p.categoria)
                .OrderBy(p => p.categoria.Ordem)
                .ThenBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Produto> ObterDetalheProdutoAsync(int id)
        {
            return await _dbcontext.Produtos
                .Where(p => p.Id == id)
                .Where(x => x.Imagem.Length > 0)
                .Include(p => p.modoentrega)
                .Include(p => p.categoria)
                .FirstOrDefaultAsync();
        }


        public async Task<Produto?> ObterProdutoDestaqueAsync()
        {
            return await _dbcontext.Produtos
                .Where(p => p.Disponivel)
                .OrderBy(p => Guid.NewGuid()) // Seleção aleatória
                .FirstOrDefaultAsync();
        }

    }
}
