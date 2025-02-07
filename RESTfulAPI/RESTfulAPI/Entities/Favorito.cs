using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using RESTfulAPI.Data;

namespace RESTfulAPI.Entities

{
    public class Favorito
    {
        public int Id { get; set; }
        [Required]
        public string UtilizadorId { get; set; }
        [ForeignKey("UtilizadorId")]
        public virtual ApplicationUser Utilizador { get; set; }

        [Required]
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        public bool Efavorito { get; set; } = true;
    }
}
