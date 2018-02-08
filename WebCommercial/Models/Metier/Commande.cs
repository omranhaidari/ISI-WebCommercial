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
        private List<ArticleCommande> articles;


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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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
        public List<ArticleCommande> Articles
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
            articles = new List<ArticleCommande>();
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public Commande(int no, int vend, int cli, DateTime date, String fact, List<ArticleCommande> arts)
        {
            noCommande = no;
            noVendeur = vend;
            noClient = cli;
            dateCde = date;
            facture = fact;
            articles = arts;
        }

        public Commande(int no, int vend, int cli, DateTime date, String fact)
            : this(no, vend, cli, date, fact, new List<ArticleCommande>())
        {
            
        }

        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="noCom">N° de l'utilisateur à lire</param>
        public static Commande getCommande(int noCom)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un article.", "Article.RechercheUnArticle()");
            try
            {

                mysql = "SELECT NO_VENDEUR,";
                mysql += "NO_CLIENT, DATE_CDE, FACTURE ";
                mysql += "FROM Commandes WHERE NO_COMMAND = " + noCom;
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Commande commande = new Commande();
                    DataRow dataRow = dt.Rows[0];
                    commande.noCommande = noCom;
                    commande.noVendeur = int.Parse(dataRow[0].ToString());
                    commande.noClient = int.Parse(dataRow[1].ToString());
                    commande.dateCde = DateTime.Parse(dataRow[2].ToString());
                    commande.facture = dataRow[3].ToString();
                    commande.articles = getArticlesFromCommande(noCom);

                    return commande;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static List<ArticleCommande> getArticlesFromCommande(int noCom)
        {
            List<ArticleCommande> articles = new List<ArticleCommande>();
            DataTable dt;
            ArticleCommande article;
            Serreurs er = new Serreurs("Erreur sur lecture des articles de la commande.", "ArticlesList.getArticles()");
            try
            {
                String mysql = "SELECT NO_ARTICLE, QTE_CDEE, LIVREE FROM Detail_cde " +
                               "WHERE NO_COMMAND = " + noCom + " ORDER BY NO_ARTICLE";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    article = new ArticleCommande();
                    article.QuantiteCommandee = int.Parse(dataRow[1].ToString());
                    article.Livree = dataRow[2].ToString();
                    article.Article = Article.getArticle(int.Parse(dataRow[0].ToString()));

                    ((List<ArticleCommande>)articles).Add(article);
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

        public static void addArticleInCommande(int id, ArticleCommande art)
        {
            Serreurs er = new Serreurs("Erreur sur l'insertion d'un article dans une commande.", "Commande.insertArticle()");
            String requete = "INSERT INTO Detail_cde (NO_COMMAND, NO_ARTICLE, QTE_CDEE, LIVREE) VALUES " +
                                    "('" + id + "'" +
                                    ",'" + art.Article.NoArticle + "'" +
                                    ",'" + art.QuantiteCommandee + "'" +
                                    ",'" + art.Livree + "')";
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

        public static void deleteArticleInCommande(int id, int noArt)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commande.update()");
            String requete = "DELETE FROM Detail_cde WHERE NO_COMMAND = " + id + " and NO_ARTICLE = " + noArt;
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

        public static void updateArticleInCommande(int id, ArticleCommande art)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commande.update()");
            String requete = "UPDATE Detail_cde SET " +
                                  " QTE_CDEE = " + art.QuantiteCommandee +
                                  ", LIVREE = '" + art.Livree + "'" +
                                  " WHERE NO_COMMAND = " + id + " and NO_ARTICLE = " + art.Article.NoArticle;
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

        public static IEnumerable<Commande> getCommandes()
        {
            IEnumerable<Commande> commandes = new List<Commande>();
            DataTable dt;
            Commande commande;
            Serreurs er = new Serreurs("Erreur sur lecture des articles.", "ArticlesList.getArticles()");
            try
            {
                String mysql = "SELECT NO_COMMAND, NO_VENDEUR, NO_CLIENT," +
                               "DATE_CDE, FACTURE FROM Commandes ORDER BY NO_COMMAND";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    commande = new Commande();
                    commande.noCommande = int.Parse(dataRow[0].ToString());
                    commande.noVendeur = int.Parse(dataRow[1].ToString());
                    commande.noClient = int.Parse(dataRow[2].ToString());
                    commande.dateCde = DateTime.Parse(dataRow[3].ToString());
                    commande.facture = dataRow[4].ToString();
                    commande.articles = getArticlesFromCommande(commande.noCommande);

                    ((List<Commande>)commandes).Add(commande);
                }

                return commandes;
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
        /// <param name="uneCom">Vendeur à mettre à jour</param>
        public static void updateCommande(Commande uneCom)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'une commande.", "Commande.update()");
            String requete = "UPDATE Commandes SET " +
                                  "NO_VENDEUR = " + uneCom.noVendeur +
                                  ", NO_CLIENT = " + uneCom.noClient +
                                  ", DATE_CDE = '" + uneCom.dateCde.ToString("yyyy-MM-dd") + "'" +
                                  ", FACTURE = '" + uneCom.facture + "'" +
                                  " WHERE NO_COMMAND = " + uneCom.noCommande;
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

            // TODO Ajouter la modification/insertion/suppression éventuelle des articles qu'elle contient

        }

        public static int insertCommande(Commande uneCom)
        {
            Serreurs er = new Serreurs("Erreur sur la création d'une commande.", "Commande.insert()");
            String requete = "INSERT INTO Commandes (NO_VENDEUR, NO_CLIENT, DATE_CDE, FACTURE) VALUES " +
                                    "(" + uneCom.noVendeur +
                                    "," + uneCom.noClient +
                                    ",'" + uneCom.dateCde.ToString("yyyy-MM-dd") + "'" +
                                    ",'" + uneCom.facture + "')";
            try
            {
                return DBInterface.Insertion_Donnees(requete, true);
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