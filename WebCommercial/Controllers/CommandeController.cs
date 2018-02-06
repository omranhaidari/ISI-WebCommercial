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

        [Authorize]
        public ActionResult Modifier(int id)
        {
            try
            {
                Commande uneCom = Commande.getCommande(id);
                ViewBag.Title = "Modifier commande N°" + id;

                List<SelectListItem> items = new List<SelectListItem>();
                foreach (String no in Vendeur.LectureNoVendeurs())
                {
                    if(int.Parse(no) == uneCom.NoVendeur)
                    {
                        items.Add(new SelectListItem { Text = "Vendeur N°"+no, Value = no , Selected = true });
                    } else
                    {
                        items.Add(new SelectListItem { Text = "Vendeur N°"+no, Value = no });
                    }
                }
                ViewBag.NoVendeurs = items;

                items = new List<SelectListItem>();
                foreach (String no in Clientel.LectureNoClients())
                {
                    if (int.Parse(no) == uneCom.NoClient)
                    {
                        items.Add(new SelectListItem { Text = "Client N°" + no, Value = no, Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = "Client N°" + no, Value = no });
                    }
                }
                ViewBag.NoClients = items;

                items = new List<SelectListItem>();
                foreach (Article art in Article.getArticles())
                {
                    items.Add(new SelectListItem { Text = art.NoArticle + "/;/" + art.Libelle + "/;/" + art.Prix, Value = art.NoArticle.ToString() });
                }
                ViewBag.Articles = items;

                return View(uneCom);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public ActionResult AjoutArticle(int id, int noart, int quantite, string livree)  
        {
            try
            {
                Article art = new Article();
                art.NoArticle = noart;
                Commande.addArticleInCommande(id, new ArticleCommande(art, quantite, livree));

                return Json("{true}");
            }
            catch (MonException e)
            {
                return null;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Modifier(Commande uneCom)
        {
            /*if (!ModelState.IsValid)
            {
                // Si le client modifié n'est pas valide, on affiche un message d'erreur à l'utilisateur
                return View(uneCom);
            }*/

            try
            {
                Commande.updateCommande(uneCom);

                return RedirectToAction("Modifier", uneCom.NoCommande);
            }
            catch (MonException e)
            {
                return HttpNotFound();
            }
        }
    }
}