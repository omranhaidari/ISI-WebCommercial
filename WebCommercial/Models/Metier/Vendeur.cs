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
        /// <param name="numCli">N° de l'utilisateur à lire</param>
        public static Vendeur getVendeur(int numVen)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un vendeur.", "Vendeur.RechercheUnVendeur()");
            try
            {

                mysql = "SELECT SOCIETE, NOM_CL, PRENOM_CL,";
                mysql += "ADRESSE_CL, VILLE_CL, CODE_POST_CL ";
                mysql += "FROM Vendeur WHERE NO_CLIENT = " + numCli;
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Vendeur client = new Vendeur();
                    DataRow dataRow = dt.Rows[0];
                    client.NoClient = numCli;
                    client.NomCl = dataRow[1].ToString();
                    client.Societe = dataRow[0].ToString();
                    client.PrenomCl = dataRow[2].ToString();
                    client.AdresseCl = dataRow[3].ToString();
                    client.VilleCl = dataRow[4].ToString();
                    client.CodePostCl = dataRow[5].ToString();

                    return client;
                }
                else
                    return null;
            }
            catch (MySqlException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }

        }

        public static IEnumerable<Vendeur> getClients()
        {
            IEnumerable<Vendeur> clients = new List<Vendeur>();
            DataTable dt;
            Vendeur client;
            Serreurs er = new Serreurs("Erreur sur lecture des clients.", "ClientsList.getClients()");
            try
            {
                String mysql = "SELECT SOCIETE, NOM_CL, PRENOM_CL, ADRESSE_CL, VILLE_CL, CODE_POST_CL, " +
                               "NO_CLIENT FROM Vendeur ORDER BY NO_CLIENT";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    client = new Vendeur();
                    client.NoClient = int.Parse(dataRow[6].ToString());
                    client.NomCl = dataRow[1].ToString();
                    client.Societe = dataRow[0].ToString();
                    client.PrenomCl = dataRow[2].ToString();
                    client.AdresseCl = dataRow[3].ToString();
                    client.VilleCl = dataRow[4].ToString();
                    client.CodePostCl = dataRow[5].ToString();

                    ((List<Vendeur>)clients).Add(client);
                }

                return clients;
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
        /// mise à jour d'un client sur son ID
        /// </summary>
        /// <param name="numCli">N° de l'utilisateur à lire</param>
        public static void updateClient(Vendeur unCli)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un client.", "Client.update()");
            String requete = "UPDATE Vendeur SET " +
                                  "SOCIETE = '" + unCli.Societe + "'" +
                                  ", NOM_CL = '" + unCli.NomCl + "'" +
                                  ", PRENOM_CL = '" + unCli.PrenomCl + "'" +
                                  ", ADRESSE_CL = '" + unCli.AdresseCl + "'" +
                                   ", VILLE_CL = '" + unCli.VilleCl + "'" +
                                   ", CODE_POST_CL = '" + unCli.CodePostCl + "'" +
                                   " WHERE NO_CLIENT = " + unCli.NoClient;
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

        public static void insertClient(Vendeur unCli)
        {
            Serreurs er = new Serreurs("Erreur sur la création d'un client.", "Client.insert()");
            String requete = "INSERT INTO Vendeur (no_client, societe, nom_cl, prenom_cl, adresse_cl, ville_cl, code_post_cl) VALUES " +
                                    "('" + unCli.NoClient + "'" +
                                    ",'" + unCli.Societe + "'" +
                                    ",'" + unCli.NomCl + "'" +
                                    ",'" + unCli.PrenomCl + "'" +
                                    ",'" + unCli.AdresseCl + "'" +
                                    ",'" + unCli.VilleCl + "'" +
                                    ",'" + unCli.CodePostCl + "')";
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