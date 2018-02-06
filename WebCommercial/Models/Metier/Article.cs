using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using WebCommercial.Models.MesExceptions;
using WebCommercial.Models.Persistance.WebApplication1.Models.Persistance;

namespace WebCommercial.Models.Metier
{
    public class Article
    {
        //Definition des attributs
        private int noArticle;
        private String libelle;
        private int qte;
        private String villeArt;
        private float prix;
        private String interr;
        private List<Article> articles;


        //Definition des properties

        [Display(Name = "N° Article")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoArticle
        {
            get { return noArticle; }
            set { noArticle = value; }
        }

        [Display(Name = "Libellé Article")]
        [Required(ErrorMessage = "Le libellé de l'article doit être saisi")]
        public String Libelle
        {
            get { return libelle; }
            set { libelle = value; }
        }

        [Display(Name = "Quantité")]
        [Required(ErrorMessage = "La quantité doit être valide")]
        public int Quantite
        {
            get { return qte; }
            set { qte = value; }
        }

        [Display(Name = "Ville Article")]
        [Required(ErrorMessage = "La ville de l'article doit être saisie")]
        public String VilleArticle
        {
            get { return villeArt; }
            set { villeArt = value; }
        }

        [Display(Name = "Prix Article")]
        [Required(ErrorMessage = "Le prix de l'article doit être valide")]
        public float Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        [Display(Name = "Interrompu")]
        [Required(ErrorMessage = "Erreur de saisie")]
        public String Interrompu
        {
            get { return interr; }
            set { interr = value; }
        }

        [Display(Name = "Composants")]
        [Required(ErrorMessage = "Erreur de saisie")]
        public List<Article> Composants
        {
            get { return articles; }
            set { articles = value; }
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public Article()
        {
            noArticle = 0;
            libelle = "";
            qte = 0;
            villeArt = "";
            prix = 0;
            interr = "";
            articles = new List<Article>();
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public Article(int no, string lib, int qute, string ville, float price, string inte, List<Article> arts)
        {
            noArticle = no;
            libelle = lib;
            qte = qute;
            villeArt = ville;
            prix = price;
            if(inte.Length != 1)
            { // Si le nom n'est pas valide, il est remplacé par "F"
                interr = "F";
            } else
            {
                interr = inte;
            }
            articles = arts;
        }

        public Article(int no, string lib, int qute, string ville, float price, string inte)
        {
            noArticle = no;
            libelle = lib;
            qte = qute;
            villeArt = ville;
            prix = price;
            if (inte.Length != 1)
            { // Si le nom n'est pas valide, il est remplacé par "F"
                interr = "F";
            }
            else
            {
                interr = inte;
            }
            articles = new List<Article>();
        }

        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="noArt">N° de l'utilisateur à lire</param>
        public static Article getArticle(int noArt)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un article.", "Article.RechercheUnArticle()");
            try
            {

                mysql = "SELECT LIB_ARTICLE, QTE_DISPO,";
                mysql += "VILLE_ART, PRIX_ART, INTERROMPU ";
                mysql += "FROM Articles WHERE NO_ARTICLE = " + noArt;
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Article article = new Article();
                    DataRow dataRow = dt.Rows[0];
                    article.noArticle = noArt;
                    article.libelle = dataRow[0].ToString();
                    article.qte = int.Parse(dataRow[1].ToString());
                    article.villeArt = dataRow[2].ToString();
                    article.prix = float.Parse(dataRow[3].ToString());
                    article.interr = dataRow[4].ToString();

                    return article;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static List<Article> getComposantsArticle(int noArt)
        {

            String mysql;
            DataTable dt;
            Article article;
            List<Article> articles = new List<Article>();
            Serreurs er = new Serreurs("Erreur sur recherche des composants d'un article.", "Article.RechercheUnArticle()");
            try
            {

                mysql = "SELECT NO_COMPOSANT, QTE_COMPOSANT ";
                mysql += "FROM Compose WHERE NO_COMPOSE = " + noArt;
                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    article = getArticle(int.Parse(dataRow[0].ToString()));

                    ((List<Article>)articles).Add(article);
                }

                return articles;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static IEnumerable<Article> getArticles()
        {
            IEnumerable<Article> articles = new List<Article>();
            DataTable dt;
            Article article;
            Serreurs er = new Serreurs("Erreur sur lecture des articles.", "ArticlesList.getArticles()");
            try
            {
                String mysql = "SELECT NO_ARTICLE, LIB_ARTICLE, QTE_DISPO," +
                               "VILLE_ART, PRIX_ART, INTERROMPU FROM Articles ORDER BY NO_ARTICLE";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    article = new Article();
                    article.noArticle = int.Parse(dataRow[0].ToString());
                    article.libelle = dataRow[1].ToString();
                    article.qte = int.Parse(dataRow[2].ToString());
                    article.villeArt = dataRow[3].ToString();
                    article.prix = float.Parse(dataRow[4].ToString());
                    article.interr = dataRow[5].ToString();

                    ((List<Article>)articles).Add(article);
                }

                return articles;
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        /// <summary>
        /// mise à jour d'un vendeur sur son ID
        /// </summary>
        /// <param name="unArt">Vendeur à mettre à jour</param>
        public static void updateArticle(Article unArt)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un article.", "Article.update()");
            String requete = "UPDATE Articles SET " +
                                  "LIB_ARTICLE = '" + unArt.libelle + "'" +
                                  ", QTE_DISPO = '" + unArt.qte + "'" +
                                  ", VILLE_ART = '" + unArt.villeArt + "'" + "'" +
                                  ", PRIX_ART = '" + unArt.prix + "'" +
                                  ", INTERROMPU = '" + unArt.interr + "'" +
                                  " WHERE NO_ARTICLE = " + unArt.noArticle;
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static void insertArticle(Article unArt)
        {
            Serreurs er = new Serreurs("Erreur sur la création d'un article.", "Article.insert()");
            String requete = "INSERT INTO Articles (LIB_ARTICLE, QTE_DISPO, VILLE_ART, PRIX_ART, INTERROMPU) VALUES " +
                                    "('" + unArt.libelle + "'" +
                                    ",'" + unArt.qte + "'" +
                                    ",'" + unArt.villeArt + "'" +
                                    ",'" + unArt.prix + "'" +
                                    ",'" + unArt.interr + "')";
            try
            {
                DBInterface.Insertion_Donnees(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }
    }
}