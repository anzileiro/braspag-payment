using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educacional.Pagamento.Gateway.v2.Models
{
    public sealed class PaymentViewModel
    {
        [Required(ErrorMessage = "Número do Cartão Obrigatório.")]
        [StringLength(19, ErrorMessage = "Número do Cartão Inválido.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Nome no Cartão Obrigatório.")]
        [StringLength(30, ErrorMessage = "Nome no Cartão Inválido.")]
        public string NameOnCard { get; set; }

        [Required(ErrorMessage = "Mês de Validade Obrigatório.")]
        public int CardExpirationMonth { get; set; }

        [Required(ErrorMessage = "Ano de Validade Obrigatório.")]
        public int CardExpirationYear { get; set; }

        [Required(ErrorMessage = "Código de Segurança Obrigatório.")]
        [StringLength(4, ErrorMessage = "Código de Segurança Inválido.")]
        public string CardSecurityCode { get; set; }

        [Required(ErrorMessage = "Bandeira do Cartão Obrigatório.")]
        public string CardFlag { get; set; }

        public List<SelectListItem> ExpirationMonths { get; set; }
        public List<SelectListItem> ExpirationYears { get; set; }
        public List<SelectListItem> CardFlags { get; set; }

        public decimal Amount { get; set; }
    }
}