using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RESTfulAPI.Data;

namespace RESTfulAPI.Entities
{
    public class ProdutoVenda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        // Para o carrinho, associar ao usuário:
        public string? UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser? Usuario { get; set; }

        public int? VendaId { get; set; } // Opcional para carrinho ou venda
        [ForeignKey("VendaId")]
        public Venda? Venda { get; set; }

        [Required]
        public int Quantidade { get; set; } // Quantidade do produto no carrinho

        [Required]
        public decimal PrecoUnitario { get; set; } // Preço unitário
    }
}
