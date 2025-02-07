namespace GestaoLojaV1.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Metodo { get; set; } // Cartão, PayPal, etc.
        public string Status { get; set; } = "Pendente";
    }
}
