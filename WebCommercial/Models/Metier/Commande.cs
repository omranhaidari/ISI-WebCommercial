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
    public class Commande
    {
        //Definition des attributs
        private int noCommande;
        private int noVendeur;
        private int noClient;
        private DateTime dateCde;
        private String facture;
        private List<Article> articles;


        //Definition des properties

        [Display(Name = "N° Commande")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoCommande
        {
            get { return noCommande; }
            set { noCommande = value; }
        }

        [Display(Name = "N° Vendeur")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoVendeur
        {
            get { return noVendeur; }
            set { noVendeur = value; }
        }

        [Display(Name = "N° Client")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoClient
        {
            get { return noClient; }
            set { noClient = value; }
        }

        [Display(Name = "Date de la commande")]
        [Required(ErrorMessage = "La date doit être valide")]
        public DateTime DateCommande
        {
            get { return dateCde; }
            set { dateCde = value; }
        }

        [Display(Name = "Facture")]
        [Required(ErrorMessage = "Erreur de saisie")]
        public String Facture
        {
            get { return facture; }
            set { facture = value; }
        }

        [Display(Name = "Articles")]
        [Required(ErrorMessage = "Erreur de saisie")]
        public List<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public Commande()
        {
            noCommande = 0;
            noVendeur = 0;
            noClient = 0;
            dateCde = new DateTime();;
            facture = "";
            articles = new List<Article>();
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public Commande(int no, int vend, int cli, DateTime date, String fact, List<Article> arts)
        {
            noCommande = no;
            noVendeur = vend;
            noClient = cli;
            dateCde = date;
            facture = fact;
            articles = arts;
        }

        public Commande(int no, int vend, int cli, DateTime date, String fact)
            : this(no, vend, cli, date, fact, new List<Article>())
        {
            
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
                mysql += "FROM Article WHERE NO_ARTICLE = " + noArt;
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

        public static IEnumerable<Article> getArticles()
        {
            IEnumerable<Article> articles = new List<Article>();
            DataTable dt;
            Article article;
            Serreurs er = new Serreurs("Erreur sur lecture des articles.", "ArticlesList.getArticles()");
            try
            {
                String mysql = "SELECT NO_ARTICLE, LIB_ARTICLE, QTE_DISPO," +
                               "VILLE_ART, PRIX_ART, INTERROMPU FROM Article ORDER BY NO_ARTICLE";

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
            String requete = "UPDATE Article SET " +
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
            String requete = "INSERT INTO Article (LIB_ARTICLE, QTE_DISPO, VILLE_ART, PRIX_ART, INTERROMPU) VALUES " +
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