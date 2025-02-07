using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace GestaoLojaV1.Entities
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Limite arbitrário para o nome da categoria
        public string Nome { get; set; } = string.Empty;

        public int? Ordem { get; set; } // Ordem no frontend

        public string? UrlImagem { get; set; }
        public byte[]? Imagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Chave estrangeira para subcategoria
        public int? CategoriaPaiId { get; set; }


        [ForeignKey(nameof(CategoriaPaiId))]
        public Categoria? CategoriaPai { get; set; }

        // Propriedade de navegação para as subcategorias
        public ICollection<Categoria>? Subcategorias { get; set; } = new List<Categoria>();

    }
}
