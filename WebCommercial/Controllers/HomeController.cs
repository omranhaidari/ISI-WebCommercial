using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCommercial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Accueil";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "À propos";
            ViewBag.Message = "Page en construction";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";
            ViewBag.Message = "Page de contact";

            return View();
        }

        public ActionResult Accueil(string id = "Christian")
        {
            ViewBag.Title = "Accueil";
            if (string.IsNullOrWhiteSpace(id))
                return View("Error");
            else
            {
                ViewBag.Nom = id;
                return View();
            }
        }
    }
}