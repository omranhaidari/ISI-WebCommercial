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
            if (!ModelState.IsValid)
            {
                // Si le client modifié n'est pas valide, on affiche un message d'erreur à l'utilisateur
                return View(unC);
            }
                
            try
            {
                // utilisation possible de Request
               //  String s= Request["Societe"];

                Clientel.updateClient(unC);
                ViewBag.Title = "Modifier";

                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Ajouter()
        {
            try
            {
                ViewBag.Title = "Ajouter un client";
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Ajouter(Clientel unC)
        {
            /*List<String> mesNumeros;
            int num;

            // Test pour savoir quel ID on peut donner au client créé
            try
            {
                mesNumeros = Clientel.LectureNoClient();
                if(int.TryParse(mesNumeros.Last(), out num))
                {
                    num++;
                }
            }*/

            unC.NoClient = "000269";

            try
            {
                // utilisation possible de Request
                //  String s= Request["Societe"];

                Clientel.insertClient(unC);
                ViewBag.Title = "Ajouter un client";

                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return View(unC);
            }
        }
    }
}
