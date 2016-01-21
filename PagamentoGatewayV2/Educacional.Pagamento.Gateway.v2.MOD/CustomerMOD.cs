using System;
using System.Configuration;

namespace Educacional.Pagamento.Gateway.v2.MOD
{
    public sealed class CustomerMOD
    {
        public CustomerMOD()
        {
            this.Id = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            this.Name = ConfigurationManager.AppSettings["api_braspag_customer_name"].ToString();
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
