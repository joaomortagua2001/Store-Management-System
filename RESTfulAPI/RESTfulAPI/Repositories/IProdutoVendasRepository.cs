using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories;

public interface IProdutoVendaRepository
{
    Task<bool> AdicionarItemCarrinho(ProdutoVenda item);
    Task<IEnumerable<ProdutoVenda>> GetItensCarrinho(string usuarioId);
    Task<bool> AtualizarQuantidadeItem(int id, int novaQuantidade);
    Task<bool> RemoverItemCarrinho(int id);
}
