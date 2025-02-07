using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;

public class PagamentoRepository : IPagamentoRepository
{
    private readonly ApplicationDbContext _dbcontext;

    public PagamentoRepository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    // Obter um pagamento pelo ID
    public async Task<Pagamento?> ObterPagamentoPorIdAsync(int id)
    {
        return await _dbcontext.Pagamentos
            .Include(p => p.Venda) // Inclui a venda associada, se necessário
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Atualizar o status de um pagamento
    public async Task AtualizarPagamentoAsync(Pagamento pagamento)
    {
        var pagamentoExistente = await _dbcontext.Pagamentos.FindAsync(pagamento.Id);
        if (pagamentoExistente == null)
        {
            throw new Exception($"Pagamento com ID {pagamento.Id} não encontrado.");
        }

        pagamentoExistente.Status = pagamento.Status;
        pagamentoExistente.DataPagamento = pagamento.DataPagamento;
        pagamentoExistente.ValorPago = pagamento.ValorPago;

        await _dbcontext.SaveChangesAsync();
    }

    public async Task<Pagamento?> ObterPagamentoPorVendaIdAsync(int vendaId)
    {
        return await _dbcontext.Pagamentos
            .FirstOrDefaultAsync(p => p.VendaId == vendaId);
    }

    public async Task CriarPagamentoAsync(Pagamento pagamento)
    {
        _dbcontext.Pagamentos.Add(pagamento);
        await _dbcontext.SaveChangesAsync();
    }

}
