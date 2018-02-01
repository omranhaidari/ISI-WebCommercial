using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.MesExceptions;
using WebCommercial.Models.Metier;

namespace WebCommercial.Controllers
{
    public class CommandeController : Controller
    {
        // GET: Commande
        public ActionResult Index()
        {
            IEnumerable<Commande> commandes = null;

            try
            {
                commandes = Commande.getCommandes();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des commandes : " + e.Message);
                ViewBag.Message = "Liste des commandes";
                return View("Error");
            }

            ViewBag.Title = "Liste des commandes";
            return View(commandes);
        }

        public ActionResult Modifier(int id)
        {
            try
            {
                Commande uneCom = Commande.getCommande(id);
                ViewBag.Title = "Modifier commande N°" + id;
                return View(uneCom);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Modifier(Commande uneCom)
        {
            if (!ModelState.IsValid)
            {
                // Si le client modifié n'est pas valide, on affiche un message d'erreur à l'utilisateur
                return View(uneCom);
            }

            try
            {
                // utilisation possible de Request
                //  String s= Request["Societe"];

                Commande.updateCommande(uneCom);

                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
    }
}