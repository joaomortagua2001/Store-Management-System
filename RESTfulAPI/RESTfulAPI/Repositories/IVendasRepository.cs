using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public interface IVendasRepository
    {
        Task<IEnumerable<Venda>> ObterTodasVendasAsync();
        Task<Venda?> ObterVendaPorIdAsync(int id);
        Task<IEnumerable<Venda>> ObterVendasPorUsuarioAsync(string usuarioId);
        Task AdicionarVendaAsync(Venda venda);
        Task AtualizarStatusVendaAsync(int id, string status);
    }
}
