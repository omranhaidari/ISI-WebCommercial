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
    public class Clientel
    {
        //Definition des attributs
        private int noClient;
        private String societe;
        private String nomCl;
        private String prenomCl;
        private String adresseCl;
        private String villeCl;
        private String codePostCl;


        //Definition des properties

        [Display(Name = "Société")]
        [Required(ErrorMessage = "Le nom de la société doit être saisi")]
        public String Societe
        {
            get { return societe; }
            set { societe = value; }
        }

        [Display(Name = "Nom Client")]
        [Required(ErrorMessage = "Le nom du client doit être saisi")]
        public String NomCl
        {
            get { return nomCl; }
            set { nomCl = value; }
        }

        [Display(Name = "N° Client")]
        [Required(ErrorMessage = "L'identifiant doit être valide")]
        public int NoClient
        {
            get { return noClient; }
            set { noClient = value; }
        }

        [Display(Name = "Prénom Client")]
        [Required(ErrorMessage = "Le prénom du client doit être saisi")]
        public String PrenomCl
        {
            get { return prenomCl; }
            set { prenomCl = value; }
        }

        [Display(Name = "Adresse Client")]
        [Required(ErrorMessage = "L'adresse du client doit être saisie")]
        public String AdresseCl
        {
            get { return adresseCl; }
            set { adresseCl = value; }
        }

        [Display(Name = "Ville Client")]
        [Required(ErrorMessage = "La ville du client doit être saisie")]
        public String VilleCl
        {
            get { return villeCl; }
            set { villeCl = value; }
        }

        [Display(Name = "Code Postal Client")]
        [Required(ErrorMessage = "Le code postal du client doit être saisi")]
        public String CodePostCl
        {
            get { return codePostCl; }
            set { codePostCl = value; }
        }


        /// <summary>
        /// Initialisation
        /// </summary>
        public Clientel()
        {
            noClient = 0;
            societe = "";
            nomCl = "";
            prenomCl = "";
            adresseCl = "";
            villeCl = "";
            codePostCl = "";
        }
        /// <summary>
        /// Initialisation avec les paramètres
        /// </summary>
        public Clientel(int no, string soc, string nom, string prenom, string adresse, string ville, String codePostal)
        {
            noClient = no;
            societe = soc;
            nomCl = nom;
            prenomCl = prenom;
            adresseCl = adresse;
            villeCl = ville;
            codePostCl = codePostal;
        }

        /// <summary>
        /// Lister les clients de la base
        /// </summary>
        /// <returns>Liste de numéros de clients</returns>
        public static List<String> LectureNoClients()
        {
            List<String> mesNumeros = new List<String>();
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur lecture du client.", "Clientel.LectureNoClient()");
            try
            {

                String mysql = "SELECT DISTINCT NO_CLIENT FROM clientel ORDER BY NO_CLIENT";
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

        /// <summary>
        /// Lire un utilisateur sur son ID
        /// </summary>
        /// <param name="numCli">N° de l'utilisateur à lire</param>
        public static Clientel getClient(int numCli)
        {

            String mysql;
            DataTable dt;
            Serreurs er = new Serreurs("Erreur sur recherche d'un client.", "Client.RechercheUnClient()");
            try
            {

                mysql = "SELECT SOCIETE, NOM_CL, PRENOM_CL,";
                mysql += "ADRESSE_CL, VILLE_CL, CODE_POST_CL ";
                mysql += "FROM clientel WHERE NO_CLIENT = " + numCli;
                dt = DBInterface.Lecture(mysql, er);

                if (dt.IsInitialized && dt.Rows.Count > 0)
                {
                    Clientel client = new Clientel();
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

        public static IEnumerable<Clientel> getClients()
        {
            IEnumerable<Clientel> clients = new List<Clientel>();
            DataTable dt;
            Clientel client;
            Serreurs er = new Serreurs("Erreur sur lecture des clients.", "ClientsList.getClients()");
            try
            {
                String mysql = "SELECT SOCIETE, NOM_CL, PRENOM_CL, ADRESSE_CL, VILLE_CL, CODE_POST_CL, " +
                               "NO_CLIENT FROM clientel ORDER BY NO_CLIENT";

                dt = DBInterface.Lecture(mysql, er);

                foreach (DataRow dataRow in dt.Rows)
                {
                    client = new Clientel();
                    client.NoClient = int.Parse(dataRow[6].ToString());
                    client.NomCl = dataRow[1].ToString();
                    client.Societe = dataRow[0].ToString();
                    client.PrenomCl = dataRow[2].ToString();
                    client.AdresseCl = dataRow[3].ToString();
                    client.VilleCl = dataRow[4].ToString();
                    client.CodePostCl = dataRow[5].ToString();

                    ((List<Clientel>)clients).Add(client);
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
        public static void updateClient(Clientel unCli)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un client.", "Client.update()");
            String requete = "UPDATE Clientel SET " +
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

        public static void insertClient(Clientel unCli)
        {
            Serreurs er = new Serreurs("Erreur sur la création d'un client.", "Client.insert()");
            String requete = "INSERT INTO Clientel (no_client, societe, nom_cl, prenom_cl, adresse_cl, ville_cl, code_post_cl) VALUES " +
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