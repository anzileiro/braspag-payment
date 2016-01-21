using Educacional.Pagamento.Gateway.v2.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using Educacional.Pagamento.Gateway.v2.BLL;
using System.Threading.Tasks;
using Educacional.Pagamento.Gateway.v2.MOD;
using System.Net;

namespace Educacional.Pagamento.Gateway.v2.Controllers
{
    public class PaymentController : Controller
    {
        private const int TOTAL_YEARS = 10;
        private const int TOTAL_MONTHS = 12;
        private readonly PaymentBLL _paymentBLL;

        public PaymentController()
        {
            this._paymentBLL = new PaymentBLL();
        }

        [NonAction]
        private PaymentViewModel PreparePaymentForViewModel()
        {
            List<SelectListItem> years = new List<SelectListItem>();

            for (int i = DateTime.Now.Year; i < DateTime.Now.AddYears(TOTAL_YEARS).Year; i++)
            {
                years.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = false
                });
            }

            List<SelectListItem> months = new List<SelectListItem> 
            {
                new SelectListItem { Text = "Janeiro (01)", Value = "01", Selected = false },
                new SelectListItem { Text = "Fevereiro (02)", Value = "02", Selected = false },
                new SelectListItem { Text = "Março (03)", Value = "03", Selected = false },
                new SelectListItem { Text = "Abril (04)", Value = "04", Selected = false },
                new SelectListItem { Text = "Maio (05)", Value = "05", Selected = false },
                new SelectListItem { Text = "Junho (06)", Value = "06", Selected = false },
                new SelectListItem { Text = "Julho (07)", Value = "07", Selected = false },
                new SelectListItem { Text = "Agosto (08)", Value = "08", Selected = false },
                new SelectListItem { Text = "Setembro (09)", Value = "09", Selected = false },
                new SelectListItem { Text = "Outubro (10)", Value = "10", Selected = false },
                new SelectListItem { Text = "Novembro (11)", Value = "11", Selected = false },
                new SelectListItem { Text = "Dezembro (12)", Value = "12", Selected = false }
            };

            List<SelectListItem> flags = new List<SelectListItem> 
            {
                new SelectListItem { Text = "Visa", Value = "Visa", Selected = false },
                new SelectListItem { Text = "Master", Value = "Master", Selected = false },
                new SelectListItem { Text = "Amex", Value = "Amex", Selected = false },
                new SelectListItem { Text = "Elo", Value = "Elo", Selected = false },
                new SelectListItem { Text = "Aura", Value = "Aura", Selected = false },
                new SelectListItem { Text = "Jcb", Value = "Jcb", Selected = false },
                new SelectListItem { Text = "Diners", Value = "Diners", Selected = false },
                new SelectListItem { Text = "Discover", Value = "Discover", Selected = false }
            };

            return new PaymentViewModel
            {
                ExpirationMonths = months,
                ExpirationYears = years,
                CardFlags = flags
            };
        }

        private PaymentMOD ConfigurationFromPayment(PaymentViewModel paymentModel)
        {
            CardMOD cardMOD = new CardMOD
            {
                Code = paymentModel.CardSecurityCode,
                Expiration = string.Format("{0}/{1}", paymentModel.CardExpirationMonth, paymentModel.CardExpirationYear),
                Flag = paymentModel.CardFlag,
                Name = paymentModel.NameOnCard,
                Number = paymentModel.CardNumber
            };

            return new PaymentMOD(cardMOD, TypeMOD.CREDIT_CARD, UtilityMOD.API_BRASPAG_PROVIDER_TYPE, InstallmentsMOD.ONE, paymentModel.Amount);
        }

        [HttpGet]
        public ViewResult Home()
        {
            return View(PreparePaymentForViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Pay(PaymentViewModel paymentModel)
        {
            if (paymentModel != null)
            {
                if (ModelState.IsValid)
                {
                    ResponseMOD response = await this._paymentBLL.PayWithBraspagAsync(ConfigurationFromPayment(paymentModel));

                    if (response.Status.Code == HttpStatusCode.OK)
                    {
                        return PartialView("_Success", "Thank you! Your payment has been successfully made.");
                    }
                }
            }

            return RedirectToAction("Home", paymentModel);
        }

        [HttpGet]
        public PartialViewResult Success(string message = null)
        {
            return PartialView("_Success");
        }

        [HttpGet]
        public PartialViewResult Error(string message = null)
        {
            return PartialView("_Error");
        }

    }
}