using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestaoLojaV1.Entities
{
    public class ProdutoVenda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        [Required]
        public int VendaId { get; set; }

        [ForeignKey("VendaId")]
        public Venda Venda { get; set; }

        [Required]
        public int Quantidade { get; set; } // Quantidade comprada do produto

        [Required]
        public decimal PrecoUnitario { get; set; } // Preço unitário no momento da venda
    }
}
