
namespace Educacional.Pagamento.Gateway.v2.MOD
{
    public sealed class ResponseMOD
    {
        public StatusMOD Status { get; set; }
        public ExceptionMOD Exception { get; set; }
        public BraspagResponseMOD Object { get; set; }
    }
}
