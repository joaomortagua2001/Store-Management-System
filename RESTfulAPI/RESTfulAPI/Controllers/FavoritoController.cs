using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories;

namespace RESTfulAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritosController : ControllerBase
{
    private readonly IFavoritoRepository _favoritoRepository;

    public FavoritosController(IFavoritoRepository favoritoRepository)
    {
        _favoritoRepository = favoritoRepository;
    }

    // Obter todos os favoritos de um utilizador
    [HttpGet("{utilizadorId}")]
    public async Task<IActionResult> GetFavoritos(string utilizadorId)
    {
        if (string.IsNullOrWhiteSpace(utilizadorId))
            return BadRequest("O ID do utilizador é obrigatório.");

        var favoritos = await _favoritoRepository.GetFavoritos(utilizadorId);

        if (favoritos == null || !favoritos.Any())
            return NotFound("Nenhum favorito encontrado para este utilizador.");

        return Ok(favoritos);
    }

    // Adicionar um produto aos favoritos
    [HttpPost("{produtoId}")]
    public async Task<IActionResult> AdicionarFavorito(int produtoId, [FromQuery] string utilizadorId)
    {
        if (produtoId <= 0)
            return BadRequest("O ID do produto é inválido.");

        if (string.IsNullOrWhiteSpace(utilizadorId))
            return BadRequest("O ID do utilizador é obrigatório.");

        var sucesso = await _favoritoRepository.AdicionarFavorito(produtoId, utilizadorId);
        if (!sucesso)
            return BadRequest("Erro ao adicionar o produto aos favoritos.");

        return Ok("Produto adicionado aos favoritos.");
    }

    // Remover um produto dos favoritos
    [HttpDelete("{produtoId}")]
    public async Task<IActionResult> RemoverFavorito(int produtoId, [FromQuery] string utilizadorId)
    {
        if (produtoId <= 0)
            return BadRequest("O ID do produto é inválido.");

        if (string.IsNullOrWhiteSpace(utilizadorId))
            return BadRequest("O ID do utilizador é obrigatório.");

        var sucesso = await _favoritoRepository.RemoverFavorito(produtoId, utilizadorId);
        if (!sucesso)
            return NotFound("Produto não encontrado nos favoritos.");

        return Ok("Produto removido dos favoritos.");
    }
}
