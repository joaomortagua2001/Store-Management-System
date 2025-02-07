using Microsoft.AspNetCore.Identity;
using GestaoLojaV1.Entities;
using System;

namespace GestaoLojaV1.Data
{
    public static class Inicializacao
    {
        public static async Task CriaDadosIniciais(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            // Adicionar default Roles
            string[] roles = { "Admin", "Gestor", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityRole roleRole = new IdentityRole(role);
                    await roleManager.CreateAsync(roleRole);
                }
            }

            // Adicionar default User - Admin
            var defaultUser = await userManager.FindByEmailAsync("admin@localhost.com");
            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser
                {
                    UserName = "admin@localhost.com",
                    Email = "admin@localhost.com",
                    Nome = "Administrador",
                    Apelido = "Local",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                await userManager.CreateAsync(defaultUser, "Is3C..00");
                await userManager.AddToRoleAsync(defaultUser, "Admin");
            }

            // Adicionar Categorias
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(
                    new Categoria { Nome = "Roupas", Ordem = 1 },
                    new Categoria { Nome = "Calçados", Ordem = 2 },
                    new Categoria { Nome = "Acessórios", Ordem = 3 }
                );
                await context.SaveChangesAsync(); // Salva as categorias no banco
            }

            // Recuperar IDs das categorias
            var categoriaRoupas = context.Categorias.FirstOrDefault(c => c.Nome == "Roupas");
            var categoriaCalcados = context.Categorias.FirstOrDefault(c => c.Nome == "Calçados");

            // Adicionar Produtos
            if (!context.Produtos.Any())
            {
                context.Produtos.AddRange(
                    new Produto
                    {
                        Nome = "Camiseta Básica",
                        Detalhe = "Camiseta 100% algodão",
                        Preco = 19.99M,
                        Promocao = false,
                        MaisVendido = true,
                        EmStock = 100,
                        Disponivel = true,
                        CategoriaId = categoriaRoupas.Id,
                        Imagem = Array.Empty<byte>() // Valor padrão para evitar o erro
                    },
                    new Produto
                    {
                        Nome = "Tênis Esportivo",
                        Detalhe = "Tênis para corrida",
                        Preco = 89.99M,
                        Promocao = true,
                        MaisVendido = true,
                        EmStock = 50,
                        Disponivel = true,
                        CategoriaId = categoriaCalcados.Id,
                        Imagem = Array.Empty<byte>() // Valor padrão para evitar o erro
                    }
                );
                await context.SaveChangesAsync(); // Salva os produtos no banco
            }

            // Recuperar IDs dos produtos
            var produto1 = context.Produtos.FirstOrDefault(p => p.Nome == "Camiseta Básica");
            var produto2 = context.Produtos.FirstOrDefault(p => p.Nome == "Tênis Esportivo");

            // Adicionar Vendas
            if (!context.Vendas.Any())
            {
                var venda = new Venda
                {
                    UsuarioId = defaultUser.Id,
                    Data = DateTime.Now,
                    Status = "Confirmado",
                    Total = 119.98M,
                    ProdutosVenda = new List<ProdutoVenda>
                    {
                        new ProdutoVenda { ProdutoId = produto1.Id, Quantidade = 2, PrecoUnitario = 19.99M },
                        new ProdutoVenda { ProdutoId = produto2.Id, Quantidade = 1, PrecoUnitario = 89.99M }
                    }
                };

                context.Vendas.Add(venda);
                await context.SaveChangesAsync(); // Salva a venda e gera o ID
            }

            // Adicionar Pagamentos
            if (!context.Pagamentos.Any())
            {
                var vendaExistente = context.Vendas.FirstOrDefault();
                if (vendaExistente != null)
                {
                    context.Pagamentos.Add(new Pagamento
                    {
                        VendaId = vendaExistente.Id, // Referencia a venda salva anteriormente
                        ValorPago = 119.98M,
                        DataPagamento = DateTime.Now,
                        Metodo = "Cartão"
                    });
                    await context.SaveChangesAsync(); // Salva o pagamento
                }
            }
        }
    }
}
