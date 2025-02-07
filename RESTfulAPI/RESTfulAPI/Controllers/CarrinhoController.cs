using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhoController : ControllerBase
{
    private readonly IProdutoVendaRepository _produtoVendaRepository;

    public CarrinhoController(IProdutoVendaRepository produtoVendaRepository)
    {
        _produtoVendaRepository = produtoVendaRepository;
    }

    // Adicionar item ao carrinho
    [HttpPost]
    public async Task<IActionResult> AdicionarItemCarrinho([FromBody] ProdutoVenda item)
    {
        if (item == null || item.Quantidade <= 0)
            return BadRequest("Dados inválidos para o item do carrinho.");

        var sucesso = await _produtoVendaRepository.AdicionarItemCarrinho(item);
        if (!sucesso)
            return StatusCode(500, "Erro ao adicionar item no carrinho.");

        return Ok();
    }

    // Listar itens do carrinho por usuário
    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetItensCarrinho(string usuarioId)
    {
        var itens = await _produtoVendaRepository.GetItensCarrinho(usuarioId);
        if (itens == null || !itens.Any())
            return NotFound("Nenhum item encontrado no carrinho.");

        return Ok(itens);
    }

    // Atualizar quantidade de um item no carrinho
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarQuantidadeItem(int id, [FromBody] int novaQuantidade)
    {
        if (novaQuantidade <= 0)
            return BadRequest("A quantidade deve ser maior que zero.");

        var sucesso = await _produtoVendaRepository.AtualizarQuantidadeItem(id, novaQuantidade);
        if (!sucesso)
            return NotFound($"Item com ID {id} não encontrado.");

        return NoContent();
    }

    // Remover item do carrinho
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverItemCarrinho(int id)
    {
        var sucesso = await _produtoVendaRepository.RemoverItemCarrinho(id);
        if (!sucesso)
            return NotFound($"Item com ID {id} não encontrado.");

        return NoContent();
    }
}
