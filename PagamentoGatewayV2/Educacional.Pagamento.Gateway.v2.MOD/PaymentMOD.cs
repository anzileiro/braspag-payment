
namespace Educacional.Pagamento.Gateway.v2.MOD
{
    public sealed class PaymentMOD
    {
        public PaymentMOD(CardMOD card, string type, string provider, string installments, decimal amount, string description = null)
        {
            this.Card = card;
            this.Type = type;
            this.Provider = provider;
            this.Installments = installments;
            this.Amount = amount;
            this.Description = description;
            this.Customer = new CustomerMOD();
        }

        public CardMOD Card { get; set; }
        public CustomerMOD Customer { get; set; }
        public string Installments { get; set; }
        public string Type { get; set; }
        public string Provider { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

    }
}
