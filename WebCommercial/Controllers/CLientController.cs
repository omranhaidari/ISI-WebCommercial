using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.Metier;
using WebCommercial.Models.MesExceptions;

namespace WebApplication1.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<Clientel> clients = null;

            try
            {
                clients = Clientel.getClients();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des clients : " + e.Message);
                ViewBag.Message = "Liste des clients";
                return View("Error");
            }

            ViewBag.Title = "Liste des clients";
            return View(clients);
        }

        // GET: Commande/Edit/5
        public ActionResult Modifier(string id)
        {
            try
            {
                Clientel unCl = Clientel.getClient(id);
                ViewBag.Title = "Modifier";
                return View(unCl);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Clientel unC)
        {
            try
            {
                // utilisation possible de Request
               //  String s= Request["Societe"];

                Clientel.updateClient(unC);
                ViewBag.Title = "Modifier";
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
    }
}
