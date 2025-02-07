namespace RESTfulAPI.DTOs
{
    public class CriarVendaDTO
    {
        public string UsuarioId { get; set; }
        public List<ProdutoVendaDTO> ProdutosVenda { get; set; } = new List<ProdutoVendaDTO>();
    }

    public class ProdutoVendaDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
