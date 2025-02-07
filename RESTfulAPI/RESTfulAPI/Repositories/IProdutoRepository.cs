﻿using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories
{
    public interface IProdutoRepository
    {
        
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Produto>> ObterProdutosPromocaoAsync();
        Task<IEnumerable<Produto>> ObterProdutosMaisVendidosAsync();
        Task<Produto> ObterDetalheProdutoAsync(int id);
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto?> ObterProdutoDestaqueAsync();
        Task<Produto?> ObterProdutoPorIdAsync(int produtoId);

    }
}
