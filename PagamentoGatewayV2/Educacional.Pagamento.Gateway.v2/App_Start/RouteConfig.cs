using System.Web.Mvc;
using System.Web.Routing;

namespace Educacional.Pagamento.Gateway.v2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "payment_pay",
                url: "payment/pay",
                defaults: new { controller = "payment", action = "pay" }
            );

            routes.MapRoute(
                name: "javascript_enabled",
                url: "javascript/enabled",
                defaults: new { controller = "security", action = "javascriptenabled" }
            );

            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "payment", action = "home", id = UrlParameter.Optional }
            );
        }
    }
}
