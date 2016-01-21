using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Educacional.Pagamento.Gateway.v2.Controllers
{
    public class SecurityController : Controller
    {
        [HttpGet]
        public PartialViewResult JavascriptEnabled()
        {
            return PartialView("_Default");
        }
    }
}