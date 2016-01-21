using System.Configuration;

namespace Educacional.Pagamento.Gateway.v2.MOD
{
    public struct UtilityMOD
    {
        public static string API_BRASPAG_URL = ConfigurationManager.AppSettings["api_braspag_url_address"].ToString();
        
        public static string API_BRASPAG_MERCHANT_ID = ConfigurationManager.AppSettings["api_braspag_merchant_id"].ToString();
        
        public static string API_BRASPAG_MERCHANT_KEY = ConfigurationManager.AppSettings["api_braspag_merchant_key"].ToString();
        
        public static string API_BRASPAG_PROVIDER_TYPE = ConfigurationManager.AppSettings["api_braspag_provider_type"].ToString();
        
        public static string INTERNAL_USERS_EMAILS = ConfigurationManager.AppSettings["internal_users_emails"].ToString();

        public static string INTERNAL_USER_LOGIN = ConfigurationManager.AppSettings["internal_email_login"].ToString();

        public static string INTERNAL_USER_PASSWORD = ConfigurationManager.AppSettings["internal_email_password"].ToString();
    }
}
