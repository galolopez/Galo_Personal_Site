using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Galo_Personal_Site.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your main page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public new ActionResult Profile()
        {
            ViewBag.Message = "Your profile page.";

            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Your portfolio page.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your blog page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}