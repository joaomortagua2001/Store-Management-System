using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories;

[ApiController]
[Route("api/[controller]")]
public class PagamentosController : ControllerBase
{
    private readonly IPagamentoRepository _pagamentoRepository;
    private readonly IVendasRepository _vendasRepository;

    public PagamentosController(IPagamentoRepository pagamentoRepository, IVendasRepository vendasRepository)
    {
        _pagamentoRepository = pagamentoRepository;
        _vendasRepository = vendasRepository;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> SimularPagamento(int id)
    {
        // Localiza o pagamento pelo ID
        var pagamento = await _pagamentoRepository.ObterPagamentoPorIdAsync(id);
        if (pagamento == null)
            return NotFound($"Pagamento com ID {id} não encontrado.");

        // Verifica se já está pago
        if (pagamento.Status == "Pago")
            return BadRequest("O pagamento já foi concluído.");

        // Altera o status para "Pago"
        pagamento.Status = "Pago";
        pagamento.DataPagamento = DateTime.Now;

        // Salva as alterações
        await _pagamentoRepository.AtualizarPagamentoAsync(pagamento);

        // Retorna sucesso
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPagamento(int id)
    {
        // Localiza o pagamento pelo ID
        var pagamento = await _pagamentoRepository.ObterPagamentoPorIdAsync(id);
        if (pagamento == null)
            return NotFound($"Pagamento com ID {id} não encontrado.");

        // Retorna os detalhes do pagamento
        return Ok(new
        {
            Id = pagamento.Id,
            VendaId = pagamento.VendaId,
            ValorPago = pagamento.ValorPago,
            DataPagamento = pagamento.DataPagamento,
            Status = pagamento.Status,
            Metodo = pagamento.Metodo
        });
    }

    [HttpPut("venda/{vendaId}/pagar")]
    public async Task<IActionResult> SimularPagamentoPorVenda(int vendaId, [FromBody] string metodo)
    {
        // Verificar se a venda existe
        var venda = await _vendasRepository.ObterVendaPorIdAsync(vendaId);
        if (venda == null)
            return NotFound($"Venda com ID {vendaId} não encontrada.");

        // Verificar se já existe um pagamento associado à venda
        var pagamentoExistente = await _pagamentoRepository.ObterPagamentoPorVendaIdAsync(vendaId);
        if (pagamentoExistente != null && pagamentoExistente.Status == "Pago")
            return BadRequest("O pagamento para esta venda já foi concluído.");

        // Criar ou atualizar o pagamento
        var pagamento = pagamentoExistente ?? new Pagamento
        {
            VendaId = vendaId,
            ValorPago = venda.Total,
            DataPagamento = DateTime.Now,
            Status = "Pago",
            Metodo = metodo
        };

        if (pagamentoExistente == null)
            await _pagamentoRepository.CriarPagamentoAsync(pagamento);
        else
            await _pagamentoRepository.AtualizarPagamentoAsync(pagamento);

        return NoContent();
    }
}
