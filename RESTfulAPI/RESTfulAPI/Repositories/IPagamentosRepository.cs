using RESTfulAPI.Entities;

public interface IPagamentoRepository
{
    Task<Pagamento?> ObterPagamentoPorIdAsync(int id);
    Task AtualizarPagamentoAsync(Pagamento pagamento);
    Task<Pagamento?> ObterPagamentoPorVendaIdAsync(int vendaId);
    Task CriarPagamentoAsync(Pagamento pagamento);

}
