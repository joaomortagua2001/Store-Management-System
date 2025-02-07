using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.DTOs;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendasController : ControllerBase
{
    private readonly IVendasRepository _vendaRepository;
    private readonly IProdutoRepository _produtoRepository;

    public VendasController(IVendasRepository vendaRepository, IProdutoRepository produtoRepository)
    {
        _vendaRepository = vendaRepository;
        _produtoRepository = produtoRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CriarVenda([FromBody] CriarVendaDTO criarVendaDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (criarVendaDTO.ProdutosVenda == null || !criarVendaDTO.ProdutosVenda.Any())
            return BadRequest("A venda deve conter pelo menos um produto.");

        var venda = new Venda
        {
            UsuarioId = criarVendaDTO.UsuarioId,
            Data = DateTime.Now,
            Status = "Pendente",
            ProdutosVenda = new List<ProdutoVenda>()
        };

        decimal total = 0;

        foreach (var item in criarVendaDTO.ProdutosVenda)
        {
            var produto = await _produtoRepository.ObterProdutoPorIdAsync(item.ProdutoId);
            if (produto == null)
                return BadRequest($"Produto com ID {item.ProdutoId} não encontrado.");

            venda.ProdutosVenda.Add(new ProdutoVenda
            {
                ProdutoId = item.ProdutoId,
                Quantidade = item.Quantidade,
                PrecoUnitario = produto.Preco
            });

            total += item.Quantidade * produto.Preco;
        }

        venda.Total = total;

        await _vendaRepository.AdicionarVendaAsync(venda);

        return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVenda(int id)
    {
        var venda = await _vendaRepository.ObterVendaPorIdAsync(id);
        if (venda == null)
            return NotFound($"Venda com ID {id} não encontrada.");

        var vendaDetalhadaDTO = new VendaDetalhadaDTO
        {
            Id = venda.Id,
            UsuarioId = venda.UsuarioId,
            Data = venda.Data,
            Status = venda.Status,
            Total = venda.Total,
            ProdutosVenda = venda.ProdutosVenda.Select(pv => new ProdutoVendaDetalhadaDTO
            {
                ProdutoId = pv.ProdutoId,
                Nome = pv.Produto.Nome,
                Quantidade = pv.Quantidade,
                PrecoUnitario = pv.PrecoUnitario
            }).ToList()
        };

        return Ok(vendaDetalhadaDTO);
    }
}