namespace RESTfulAPI.DTOs
{
    public class VendaDetalhadaDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public List<ProdutoVendaDetalhadaDTO> ProdutosVenda { get; set; } = new List<ProdutoVendaDetalhadaDTO>();
    }

    public class ProdutoVendaDetalhadaDTO
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
}
