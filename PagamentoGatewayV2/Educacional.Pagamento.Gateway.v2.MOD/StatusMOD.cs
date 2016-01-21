using System.Net;

namespace Educacional.Pagamento.Gateway.v2.MOD
{
    public sealed class StatusMOD
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}
