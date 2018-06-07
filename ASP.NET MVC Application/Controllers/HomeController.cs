using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_Application.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de OSDE-VL.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contáctese con los desarrolladores.";

            return View();
        }
    }
}