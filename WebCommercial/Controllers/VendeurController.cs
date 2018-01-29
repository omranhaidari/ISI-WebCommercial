using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebCommercial.Models.MesExceptions;
using WebCommercial.Models.Metier;

namespace WebCommercial.Controllers
{
    public class VendeurController : Controller
    {
        // GET: Vendeur
        public ActionResult Index()
        {
            IEnumerable<Vendeur> vendeurs = null;

            try
            {
                vendeurs = Vendeur.getVendeurs();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des vendeurs : " + e.Message);
                ViewBag.Message = "Liste des vendeurs";
                return View("Error");
            }

            ViewBag.Title = "Liste des vendeurs";
            return View(vendeurs);
        }

        // GET: Vendeur/Details/5
        public ActionResult FicheVendeur(int id)
        {
            try
            {
                Vendeur unVend = Vendeur.getVendeur(id);
                ViewBag.Title = "Vendeur";
                return View(unVend);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Connexion()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("FicheVendeur", 0);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Connexion(string login, string pwd, string returnUrl)
        {
            Vendeur vendeur = new Vendeur();
            if(!string.IsNullOrEmpty(login))
            {
                vendeur = Vendeur.authentifier(login, pwd);
                if (vendeur != null)
                {
                    FormsAuthentication.SetAuthCookie(vendeur.NoVendeur.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("FicheVendeur", "Vendeur", new { id = vendeur.NoVendeur });
                }
                ModelState.AddModelError("Login", "Identifiant et/ou mot de passe incorrect(s)");
            }
            return View(vendeur);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();

            return Redirect("/");
        }

        // GET: Vendeur/Create
        public ActionResult Ajouter()
        {
            try
            {
                ViewBag.Title = "Ajouter un vendeur";
                return View();
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        // POST: Vendeur/Create
        [HttpPost]
        public ActionResult Ajouter(Vendeur unVend)
        {
            try
            {
                Vendeur.insertVendeur(unVend);
                ViewBag.Title = "Ajouter un vendeur";

                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return View(unVend);
            }
        }

        // GET: Vendeur/Edit/5
        public ActionResult Modifier(int id)
        {
            try
            {
                Vendeur unVend = Vendeur.getVendeur(id);
                ViewBag.Title = "Modifier";
                return View(unVend);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        // POST: Vendeur/Edit/5
        [HttpPost]
        public ActionResult Modifier(Vendeur unVend)
        {
            if (!ModelState.IsValid)
            {
                // Si le client modifié n'est pas valide, on affiche un message d'erreur à l'utilisateur
                return View(unVend);
            }

            try
            {
                // utilisation possible de Request
                //  String s= Request["Societe"];

                Vendeur.updateVendeur(unVend);
                ViewBag.Title = "Modifier";

                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        // GET: Vendeur/Delete/5
        /*public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vendeur/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
