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
    public class Vendeur
    {
        //Definition des attributs
        private int noVendeur;
        private int noChefEquipe;
        private String nomVen;
        private String prenomVen;
        private DateTime dateEmbauche;
        private String villeVen;
        private float salaire;
        private float comm;


        //Definition des properties

        [Display(Name = "N° Vendeur")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoVendeur
        {
            get { return noVendeur; }
            set { noVendeur = value; }
        }

        [Display(Name = "N° Chef d'équipe")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoChef
        {
            get { return noChefEquipe; }
            set { noChefEquipe = value; }
        }

        [Display(Name = "Nom Vendeur")]
        [Required(ErrorMessage = "Le nom du vendeur doit être saisi")]
        public String NomVendeur
        {
            get { return nomVen; }
            set { nomVen = value; }
        }

        [Display(Name = "Prénom Vendeur")]
        [Required(ErrorMessage = "Le prénom du client doit être saisi")]
        public String PrenomVendeur
        {
            get { return prenomVen; }
            set { prenomVen = value; }
        }

        [Display(Name = "Date d'embauche Vendeur")]
        [Required(ErrorMessage = "La date doit être valide")]
        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
            set { dateEmbauche = value; }
        }

        [Display(Name = "Ville Vendeur")]
        [Required(ErrorMessage = "La ville du client doit être saisie")]
        public String VilleVendeur
        {
            get { return villeVen; }
            set { villeVen = value; }
        }

        [Display(Name = "Salaire Vendeur")]
        [Required(ErrorMessage = "Le montant doit être valide")]
        public float Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }

        [Display(Name = "Commission Vendeur")]
        [Required(ErrorMessage = "Le montant doit être valide")]
        public float Commission
        {
            get { return comm; }
            set { comm = value; }
        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public Vendeur()
        {
            noVendeur = 0;
            noChefEquipe = 0;
            nomVen = "";
            prenomVen = "";
            dateEmbauche = new DateTime();
            villeVen = "";
            salaire = 0;
            comm = 0;
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public Vendeur(int no, int no_chef, string nom, string prenom, DateTime embauche, string ville, float sal, float com)
        {
            noVendeur = no;
            noChefEquipe = no_chef;
            nomVen = nom;
            prenomVen = prenom;
            dateEmbauche = embauche;
            villeVen = ville;
            salaire = sal;
            comm = com;
        }

        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="numVen">N° de l'utilisateur à lire</param>
        public static Vendeur getVendeur(int numVen)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un vendeur.", "Vendeur.RechercheUnVendeur()");
            try
            {

                mysql = "SELECT NO_VEND_CHEF_EQ, NOM_VEND, PRENOM_VEND,";
                mysql += "DATE_EMBAU, VILLE_VEND, SALAIRE_VEND, COMMISSION ";
                mysql += "FROM Vendeur WHERE NO_VENDEUR = " + numVen;
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Vendeur vendeur = new Vendeur();
                    DataRow dataRow = dt.Rows[0];
                    vendeur.NoVendeur = numVen;
                    vendeur.NoChef = int.Parse(dataRow[0].ToString());
                    vendeur.NomVendeur = dataRow[1].ToString();
                    vendeur.PrenomVendeur = dataRow[2].ToString();
                    vendeur.DateEmbauche = DateTime.Parse(dataRow[3].ToString());
                    vendeur.VilleVendeur = dataRow[4].ToString();
                    vendeur.Salaire = float.Parse(dataRow[5].ToString());
                    vendeur.Commission = float.Parse(dataRow[6].ToString());

                    return vendeur;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static List<String> LectureNoVendeurs()
        {
            List<String> mesNumeros = new List<String>();
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture du vendeur.", "Vendeur.LectureNoVendeur()");
            try
            {

                String mysql = "SELECT DISTINCT NO_VENDEUR FROM Vendeur ORDER BY NO_VENDEUR";
                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    mesNumeros.Add((dataRow[0]).ToString());
                }

                return mesNumeros;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static Vendeur authentifier(String login, String pwd)
        {
            // Pour simplifier le débug, on authentifie un vendeur avec son simple ID (sans mot de passe)
            return getVendeur(int.Parse(login));
        }

        public static IEnumerable<Vendeur> getVendeurs()
        {
            IEnumerable<Vendeur> vendeurs = new List<Vendeur>();
            DataTable dt;
            Vendeur vendeur;
            Serreurs er = new Serreurs("Erreur sur lecture des vendeurs.", "VendeursList.getVendeurs()");
            try
            {
                String mysql = "SELECT NO_VENDEUR,NO_VEND_CHEF_EQ, NOM_VEND, PRENOM_VEND,DATE_EMBAU," +
                               "VILLE_VEND,SALAIRE_VEND,COMMISSION FROM Vendeur ORDER BY NO_VENDEUR";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    vendeur = new Vendeur();
                    vendeur.NoVendeur = int.Parse(dataRow[0].ToString());
                    vendeur.NoChef = int.Parse(dataRow[1].ToString());
                    vendeur.NomVendeur = dataRow[2].ToString();
                    vendeur.PrenomVendeur = dataRow[3].ToString();
                    vendeur.DateEmbauche = DateTime.Parse(dataRow[4].ToString());
                    vendeur.VilleVendeur = dataRow[5].ToString();
                    vendeur.Salaire = float.Parse(dataRow[6].ToString());
                    vendeur.Commission = float.Parse(dataRow[7].ToString());

                    ((List<Vendeur>)vendeurs).Add(vendeur);
                }

                return vendeurs;
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
        /// <param name="unVend">Vendeur à mettre à jour</param>
        public static void updateVendeur(Vendeur unVend)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un vendeur.", "Vendeur.update()");
            String requete = "UPDATE Vendeur SET " +
                                  "NO_VEND_CHEF_EQ = '" + unVend.NoChef + "'" +
                                  ", NOM_VEND = '" + unVend.NomVendeur + "'" +
                                  ", PRENOM_VEND = '" + unVend.PrenomVendeur + "'" +
                                  ", DATE_EMBAU = '" + unVend.DateEmbauche.ToString("yyyy/MM/dd") + "'" +
                                  ", VILLE_VEND = '" + unVend.VilleVendeur + "'" +
                                  ", SALAIRE_VEND = '" + unVend.Salaire + "'" +
                                  ", COMMISSION = '" + unVend.Commission + "'" +
                                  " WHERE NO_VENDEUR = " + unVend.NoVendeur;
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

        public static void insertVendeur(Vendeur unVend)
        {
            Serreurs er = new Serreurs("Erreur sur la création d'un vendeur.", "Vendeur.insert()");
            String requete = "INSERT INTO Vendeur (NO_VEND_CHEF_EQ, NOM_VEND, PRENOM_VEND, DATE_EMBAU, VILLE_VEND, SALAIRE_VEND, COMMISSION) VALUES " +
                                    "('" + unVend.NoChef + "'" +
                                    ",'" + unVend.NomVendeur + "'" +
                                    ",'" + unVend.PrenomVendeur + "'" +
                                    ",'" + unVend.DateEmbauche.ToString("yyyy/MM/dd") + "'" +
                                    ",'" + unVend.VilleVendeur + "'" +
                                    ",'" + unVend.Salaire + "'" +
                                    ",'" + unVend.Commission + "')";
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