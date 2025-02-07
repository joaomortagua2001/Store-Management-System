using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories;

public class ProdutoVendaRepository : IProdutoVendaRepository
{
    private readonly ApplicationDbContext _context;

    public ProdutoVendaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AdicionarItemCarrinho(ProdutoVenda item)
    {
        try
        {
            // Verificar se o item já existe no carrinho do usuário
            var itemExistente = await _context.ProdutosVendidos
                .FirstOrDefaultAsync(pv => pv.ProdutoId == item.ProdutoId && pv.UsuarioId == item.UsuarioId && pv.VendaId == null);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += item.Quantidade;
            }
            else
            {
                item.VendaId = null; // Garantir que é um carrinho (não vinculado a uma venda ainda)
                await _context.ProdutosVendidos.AddAsync(item);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<ProdutoVenda>> GetItensCarrinho(string usuarioId)
    {
        return await _context.ProdutosVendidos
            .Include(pv => pv.Produto)
            .Where(pv => pv.UsuarioId == usuarioId && pv.VendaId == null)
            .ToListAsync();
    }

    public async Task<bool> AtualizarQuantidadeItem(int id, int novaQuantidade)
    {
        var item = await _context.ProdutosVendidos.FirstOrDefaultAsync(pv => pv.Id == id && pv.VendaId == null);

        if (item == null)
            return false;

        item.Quantidade = novaQuantidade;
        _context.ProdutosVendidos.Update(item);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoverItemCarrinho(int id)
    {
        var item = await _context.ProdutosVendidos.FirstOrDefaultAsync(pv => pv.Id == id && pv.VendaId == null);

        if (item == null)
            return false;

        _context.ProdutosVendidos.Remove(item);
        await _context.SaveChangesAsync();

        return true;
    }
}
