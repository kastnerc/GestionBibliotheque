using GestionBibliotheque;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBibliotheque
{
    public class Livre
    {
        // Attributs
        private string titre;
        private string auteur;
        private string isbn;
        private string statut;
        private DateTime dateRetourPrevue;
        
        // Constructeur et destructeur
        public Livre() { }

        ~Livre() { }

        public Livre(string titre, string auteur, string isbn, string statut)
        {
            this.titre = titre;
            this.auteur = auteur;
            this.isbn = isbn;
            this.statut = statut;
        }

        // Getters et Setters
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }

        public string Auteur
        {
            get { return auteur; }
            set { auteur = value; }
        }

        public string Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public string Statut
        {
            get { return statut; }
            set { statut = value; }
        }

        public DateTime DateRetourPrevue
        {
            get { return dateRetourPrevue; }
            set { dateRetourPrevue = value; }
        }

        // Méthodes
        public void Reserver(Utilisateur utilisateurActuel, Bibliotheque maBibliotheque)
        {
            Console.WriteLine("--------Réserver un Livre--------");
            Console.WriteLine();
            Console.WriteLine("Entrer le titre du livre que vous désirez réserver : ");
            string titreRecherche = Console.ReadLine();

            bool livreReserve = false;

            foreach (var livre in maBibliotheque.Livres)
            {
                if (livre.Titre.ToLower() == titreRecherche.ToLower() && livre.Statut == "Disponible")
                {
                    livre.statut = "Réservé";
                    utilisateurActuel.Reservations.Add(livre);
                    livreReserve = true;

                    Console.WriteLine("Réservation réussie.");
                    Console.WriteLine();
                    Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                    Console.ReadKey();
                    break;
                }
            }

            if (!livreReserve)
            {
                Console.WriteLine($"Le livre n'est pas disponible ");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
            }
        }

        public void Retourner(Utilisateur utilisateurActuel)
        {
            Console.WriteLine("--------Retourner un Livre--------");

            bool livreRetour = false;

            if (utilisateurActuel.Emprunts.Count > 0)
            {
                Console.WriteLine("Entrer le titre du livre que vous désirez retourner : ");
                string titreRecherche = Console.ReadLine();

                foreach (var livre in utilisateurActuel.Emprunts)
                {
                    if (livre.Titre.ToLower() == titreRecherche.ToLower() && livre.Statut == "Emprunté")
                    {
                        livre.statut = "Disponible";
                        utilisateurActuel.Emprunts.Remove(livre);
                        livreRetour = true;

                        Console.WriteLine("Retour réussi.");
                        Console.WriteLine();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                        Console.ReadKey();
                        break;
                    }
                }

                foreach (var emprunt in utilisateurActuel.HistoriqueEmprunts)
                {
                    if (emprunt.LivreEmprunte.ToLower() == titreRecherche.ToLower())
                    {
                        emprunt.DateRetourReelle = DateTime.Now;
                        return;
                    }
                }

                        if (!livreRetour)
                {
                    Console.WriteLine("Ce livre n'est pas dans vos emprunts.");
                    Console.WriteLine();
                    Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                    Console.ReadKey();
                }
            } 
            else
            {
                Console.WriteLine("Vous n'avez aucun livre à retourner");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
            }
        }
    }
}