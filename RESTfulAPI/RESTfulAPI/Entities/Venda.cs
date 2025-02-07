using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RESTfulAPI.Data;

namespace RESTfulAPI.Entities
{
    public class Venda
    {
        public int Id { get; set; }

        [Required]
        public string UsuarioId { get; set; } // ID do usuário (relacionamento com a tabela de usuários do Identity)

        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser Usuario { get; set; } // Relacionamento com o ApplicationUser

        [Required]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pendente"; // Ex.: "Confirmado", "Rejeitado", "Pendente"

        public decimal Total { get; set; } // Valor total da venda

        public ICollection<ProdutoVenda> ProdutosVenda { get; set; } // Produtos associados a esta venda
    }
}
