using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories;

//using RESTfulAPI.Repositories;

namespace RESTfulAPI.Controllers;

    [Route("api/[Controller]")]
    [ApiController]

    //[Authorize]

    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]

    [HttpGet]
    public async Task<IActionResult> GetProdutos(string tipoProduto, int? categoriaId = null)
    {
        IEnumerable<Produto> produtos;

        if (tipoProduto == "categoria" && categoriaId != null)
        {
            produtos = await _produtoRepository.ObterProdutosPorCategoriaAsync(categoriaId.Value);
        }
        else if (tipoProduto == "detalhe" && categoriaId != null)
        {
            Produto produto = await _produtoRepository.ObterDetalheProdutoAsync(categoriaId.Value);
            return Ok(produto);
        }
        else if (tipoProduto == "promocao")
        {
            produtos = await _produtoRepository.ObterProdutosPromocaoAsync();
        }
        else if (tipoProduto == "maisvendido")
        {
            produtos = await _produtoRepository.ObterProdutosMaisVendidosAsync();
        }
        else if (tipoProduto == "todos")
        {
            produtos = await _produtoRepository.ObterTodosProdutosAsync();
        }
        else
        {
            return BadRequest("Tipo de produto inválido.");
        }

        // Certifique-se de que estamos retornando uma lista simples
        return Ok(produtos);
    }




    [HttpGet("{id}")]

        public async Task<IActionResult> GetDetalheProduto(int id)
        {
            Produto produto = await _produtoRepository.ObterDetalheProdutoAsync(id);

            if (produto is null)
            {
                return NotFound($"Produto com id = {id} nao encontrado");
            }
            return Ok(produto);
        }

    [HttpGet("destaque")]
    public async Task<IActionResult> GetProdutoDestaque()
    {
        var produto = await _produtoRepository.ObterProdutoDestaqueAsync();
        if (produto == null)
            return NotFound("Nenhum produto disponível como destaque.");
        return Ok(produto);
    }

}



