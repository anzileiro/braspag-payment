using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace Educacional.Pagamento.Gateway.v2.BLL
{
    using MOD;

    public sealed class PaymentBLL
    {
        public async Task<ResponseMOD> PayWithBraspagAsync(PaymentMOD payment)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                SetAuthForBraspag(httpClient);

                return await SendPaymentToBraspagAsync(httpClient, payment);
            }
        }

        private async Task<ResponseMOD> SendPaymentToBraspagAsync(HttpClient httpClient, PaymentMOD payment)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(UtilityMOD.API_BRASPAG_URL, SetJsonForBraspag(payment));

                ResponseMOD response = await ValidateResponseFromBraspagAsync(httpResponseMessage);

                return response;
            }
            catch (Exception exception)
            {
                return SetCurrentException(exception);
            }
        }

        private ResponseMOD SetCurrentException(Exception exception)
        {
            return new ResponseMOD
            {
                Object = null,
                Status = new StatusMOD
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = exception.Message
                },
                Exception = new ExceptionMOD
                {
                    Message = exception.Message,
                    Object = exception.InnerException
                }
            };
        }

        private async Task<ResponseMOD> ValidateResponseFromBraspagAsync(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage != null)
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    if (httpResponseMessage.Content != null)
                    {
                        return new ResponseMOD
                        {
                            Exception = null,
                            Status = new StatusMOD
                            {
                                Code = httpResponseMessage.StatusCode,
                                Message = httpResponseMessage.ReasonPhrase
                            },
                            Object = await httpResponseMessage.Content.ReadAsAsync<BraspagResponseMOD>()
                        };
                    }
                    else
                    {
                        return new ResponseMOD
                        {
                            Exception = null,
                            Object = null,
                            Status = new StatusMOD
                            {
                                Code = httpResponseMessage.StatusCode,
                                Message = httpResponseMessage.ReasonPhrase
                            }
                        };
                    }
                }
                else
                {
                    return new ResponseMOD
                    {
                        Exception = null,
                        Object = null,
                        Status = new StatusMOD 
                        {
                            Code = httpResponseMessage.StatusCode,
                            Message = httpResponseMessage.ReasonPhrase
                        }
                    };
                }
            }

            return new ResponseMOD
            {
                Exception = null,
                Object = null,
                Status = new StatusMOD 
                {
                    Code = HttpStatusCode.BadRequest,
                    Message = "Não foi possível efetuar o pagamento."
                }
            };
        }

        private void SetAuthForBraspag(HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Add("MerchantId", UtilityMOD.API_BRASPAG_MERCHANT_ID);
            httpClient.DefaultRequestHeaders.Add("MerchantKey", UtilityMOD.API_BRASPAG_MERCHANT_KEY);
        }

        private object SetJsonForBraspag(PaymentMOD payment)
        {
            return new
            {
                MerchantOrderId = payment.Customer.Id,
                Customer = new
                {
                    Name = payment.Customer.Name
                },
                Payment = new
                {
                    Capture = true,
                    Type = payment.Type,
                    Amount = payment.Amount,
                    Provider = payment.Provider,
                    Installments = payment.Installments,
                    CreditCard = new
                    {
                        CardNumber = payment.Card.Number,
                        Holder = payment.Card.Name,
                        ExpirationDate = payment.Card.Expiration,
                        SecurityCode = payment.Card.Code,
                        Brand = payment.Card.Flag
                    }
                }
            };
        }

    }
}


//{
//                    MerchantOrderId = this._cliente.Id,
//                    Customer = new
//                    {
//                        Name = this._cliente.Nome
//                    },
//                    Payment = new
//                    {
//                        Capture = true,
//                        Type = Tipo.CARTAO_CREDITO,
//                        Amount = this._cobranca.Valor,
//                        Provider = Providers.BRASPAG_PROVIDER,
//                        Installments = this._cobranca.Parcelas,
//                        CreditCard = new
//                        {
//                            CardNumber = this._cartao.Numero,
//                            Holder = this._cartao.Nome,
//                            ExpirationDate = this._cartao.Validade,
//                            SecurityCode = this._cartao.Chave,
//                            Brand = this._cartao.Bandeira
//                        }
//                    }
//};


// using (this._httpClient = new HttpClient())
//            {
//                Autenticar();

//                HttpResponseMessage httpResponseMessage = await this._httpClient.PostAsJsonAsync(Urls.BRASPAG_V2_SALES, MontarJson());

//                var resposta = new Resposta<string>
//                {
//                    Mensagem = "Transação efetuada com sucesso.",
//                    Status = httpResponseMessage.StatusCode,
//                    Objeto = await httpResponseMessage.Content.ReadAsStringAsync()
//                };

//                return resposta;
//            }