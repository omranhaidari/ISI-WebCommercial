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
    public class ArticleCommande
    {
        //Definition des attributs
        private int qteCdee;
        private String livree;
        private Article article;


        //Definition des properties

        [Display(Name = "Quantité commandée")]
        [Required(ErrorMessage = "La quantité doit être valide")]
        public int QuantiteCommandee
        {
            get { return qteCdee; }
            set { qteCdee = value; }
        }

        [Display(Name = "Livrée")]
        [Required(ErrorMessage = "L'état de la livraison doit être renseigné")]
        public String Livree
        {
            get { return livree; }
            set { livree = value; }
        }

        [Display(Name = "Article")]
        [Required(ErrorMessage = "Article requis")]
        public Article Article
        {
            get { return article; }
            set { article = value; }
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public ArticleCommande()
        {
            qteCdee = 0;
            livree = "F";
            article = new Article();
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public ArticleCommande(Article art, int qteCd, String livr)
        {
            article = art;
            qteCdee = qteCd;
            if(livr.Length != 1)
            { // Si le nom n'est pas valide, il est remplacé par "F"
                livree = "F";
            } else
            {
                livree = livr;
            }
        }

        /// <summary>
        /// mise à jour d'un vendeur sur son ID
        /// </summary>
        /// <param name="unArt">Vendeur à mettre à jour</param>
    /*    public static void updateArticleCommande(ArticleCommande unArt)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un article.", "Article.update()");
            String requete = "UPDATE article SET " +
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
            String requete = "INSERT INTO article (LIB_ARTICLE, QTE_DISPO, VILLE_ART, PRIX_ART, INTERROMPU) VALUES " +
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
        }*/
    }
}