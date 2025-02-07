using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace RESTfulAPI.Entities
{
    public class Utilizador  : IdentityUser
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
        public string? Nome { get; set; }

        [StringLength(100, ErrorMessage = "O apelido não pode exceder 100 caracteres")]
        public string? Apelido { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [StringLength(150, ErrorMessage = "O email não pode exceder 150 caracteres")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória")]
        [StringLength(100, ErrorMessage = "A password não pode exceder 100 caracteres")]
        public string? Password { get; set; }

        [ValidarNif(ErrorMessage = "NIF inválido")]
        public long? NIF { get; set; }

        [StringLength(200, ErrorMessage = "A rua não pode exceder 200 caracteres")]
        public string? Rua { get; set; }

        [StringLength(100, ErrorMessage = "A localidade principal não pode exceder 100 caracteres")]
        public string? Localidade1 { get; set; }

        [StringLength(100, ErrorMessage = "A localidade secundária não pode exceder 100 caracteres")]
        public string? Localidade2 { get; set; }

        [StringLength(50, ErrorMessage = "O país não pode exceder 50 caracteres")]
        public string? Pais { get; set; }

        [StringLength(100, ErrorMessage = "A URL da imagem não pode exceder 100 caracteres")]
        public string? UrlImagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public byte[]? Fotografia { get; set; }

        public class ValidarNif : ValidationAttribute
        {
            public override bool IsValid(object? value)
            {
                if (value is long nif)
                {
                    // Validação simplificada do NIF
                    return nif >= 100000000 && nif <= 999999999;
                }
                return false;
            }
        }
    }
}