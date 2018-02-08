using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommercial.Models.Metier;
using WebCommercial.Models.MesExceptions;

namespace WebApplication1.Controllers
{
	public class ArticleController : Controller
	{
		// GET: Article
		public ActionResult Index()
		{
			IEnumerable<Article> articles = null;

			try
			{
				articles = Article.getArticles();
			}
			catch (MonException e)
			{
				ModelState.AddModelError("Erreur", "Erreur lors de la récupération des articles : " + e.Message);
				ViewBag.Message = "Liste des articles";
				return View("Error");
			}

			ViewBag.Title = "Liste des articles";
			return View(articles);
		}

		[Authorize]
		// GET: Commande/Edit/5
		public ActionResult Modifier(int id)
		{
			try
			{
				Article unAr = Article.getArticle(id);
				ViewBag.Title = "Modifier";
				return View(unAr);
			}
			catch (MonException e)
			{
				return HttpNotFound();
			}
		}

		[Authorize]
		[HttpPost]
		public ActionResult Modifier(Article unA)
		{
			if (!ModelState.IsValid)
			{
				// Si l'article modifié n'est pas valide, on affiche un message d'erreur à l'utilisateur
				return View(unA);
			}

			try
			{
				Article.updateArticle(unA);
				ViewBag.Title = "Modifier";

				return RedirectToAction("Index");
			}
			catch (MonException e)
			{
				return HttpNotFound();
			}
		}

		[Authorize]
		public ActionResult Ajouter()
		{
			try
			{
				ViewBag.Title = "Ajouter un article";
				return View();
			}
			catch (MonException e)
			{
				return HttpNotFound();
			}
		}

		[Authorize]
		[HttpPost]
		public ActionResult Ajouter(Article unA)
		{
			try
			{
				Article.insertArticle(unA);
				ViewBag.Title = "Ajouter un article";

				return RedirectToAction("Index");
			}
			catch (MonException e)
			{
				return View(unA);
			}
		}
	}
}
