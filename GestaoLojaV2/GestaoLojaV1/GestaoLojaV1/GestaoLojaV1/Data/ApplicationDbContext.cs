using GestaoLojaV1.Data;
using GestaoLojaV1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoLojaV1.Data // Faltava abrir o bloco namespace corretamente
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ModoEntrega> ModosEntrega { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ProdutoVenda> ProdutosVendidos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração adicional para ProdutoVenda
            modelBuilder.Entity<ProdutoVenda>()
                .HasKey(pv => pv.Id);

            modelBuilder.Entity<ProdutoVenda>()
                .HasOne(pv => pv.Venda)
                .WithMany(v => v.ProdutosVenda)
                .HasForeignKey(pv => pv.VendaId);

            modelBuilder.Entity<ProdutoVenda>()
                .HasOne(pv => pv.Produto)
                .WithMany()
                .HasForeignKey(pv => pv.ProdutoId);

            modelBuilder.Entity<Categoria>()
       .HasOne(c => c.CategoriaPai)
       .WithMany(c => c.Subcategorias)
       .HasForeignKey(c => c.CategoriaPaiId)
       .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
