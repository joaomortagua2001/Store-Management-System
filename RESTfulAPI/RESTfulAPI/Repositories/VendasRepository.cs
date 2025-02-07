using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public class VendasRepository : IVendasRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public VendasRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // Obter todas as vendas com os produtos associados
        public async Task<IEnumerable<Venda>> ObterTodasVendasAsync()
        {
            return await _dbcontext.Vendas
                .Include(v => v.ProdutosVenda)
                .ThenInclude(pv => pv.Produto)
                .ToListAsync();
        }

        // Obter venda específica pelo ID com os produtos associados
        public async Task<Venda?> ObterVendaPorIdAsync(int id)
        {
            return await _dbcontext.Vendas
                .Include(v => v.ProdutosVenda)
                .ThenInclude(pv => pv.Produto)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Obter todas as vendas de um usuário específico
        public async Task<IEnumerable<Venda>> ObterVendasPorUsuarioAsync(string usuarioId)
        {
            return await _dbcontext.Vendas
                .Where(v => v.UsuarioId == usuarioId)
                .Include(v => v.ProdutosVenda)
                .ThenInclude(pv => pv.Produto)
                .ToListAsync();
        }

        // Adicionar uma nova venda ao banco de dados
        public async Task AdicionarVendaAsync(Venda venda)
        {
            // Adiciona a venda e os produtos associados
            _dbcontext.Vendas.Add(venda);
            await _dbcontext.SaveChangesAsync();
        }

        // Atualizar o status de uma venda
        public async Task AtualizarStatusVendaAsync(int id, string status)
        {
            var venda = await _dbcontext.Vendas.FindAsync(id);
            if (venda != null)
            {
                venda.Status = status;
                await _dbcontext.SaveChangesAsync();
            }
        }
    }
}
