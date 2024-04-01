using System;
using System.Collections.Generic;

namespace GestionBibliotheque
{
    public class Bibliotheque
    {
        // Listes
        public List<Utilisateur> Utilisateurs { get; }
        public List<Livre> Livres { get; }
        public List<Emprunt> HistoriqueDesPrets { get; }

        // Constructeur
        public Bibliotheque()
        {
            Utilisateurs = new List<Utilisateur>();
            Livres = new List<Livre>();
            HistoriqueDesPrets = new List<Emprunt>();
        }

        // Méthodes
        public void AjouterLivre(Livre livre)
        {
            Livres.Add(livre);
        }

        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            Utilisateurs.Add(utilisateur);
        }

        public void AjouterEmprunt(Emprunt emprunt)
        {
            HistoriqueDesPrets.Add(emprunt);
        }

        public bool VerificationInformation(Utilisateur utilisateurActuel)
        {
            foreach (var utilisateur in Utilisateurs)
            {
                if (utilisateurActuel.numeroDeCarteConnexion == utilisateur.NumeroCarte && utilisateurActuel.motDePasseConnexion == utilisateur.MotDePasse)
                {
                    return true;
                }
            }
            return false;
        }

        public void RechercheLivre()
        {
            Console.WriteLine("--------Recherche de livre (par titre)--------");
            Console.WriteLine();
            Console.WriteLine("Entrez le titre du livre que vous désirez rechercher : ");
            string titreRecherche = Console.ReadLine().ToLower();

            if (string.IsNullOrWhiteSpace(titreRecherche))
            {
                Console.WriteLine("Titre invalide.");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
                return;
            }

            bool livreTrouve = false;

            foreach (var livre in Livres)
            {
                if (livre.Titre.ToLower() == titreRecherche)
                {
                    livreTrouve = true;

                    Console.WriteLine($"Ce livre existe dans notre bibliothèque et son statut actuel est : {livre.Statut}");
                    Console.WriteLine();
                    Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                    Console.ReadKey();
                }
            }

            if (!livreTrouve)
            {
                Console.WriteLine("Ce livre n'existe pas dans notre bibliothèque.");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
            }
        }

        public void AffichageLivres()
        {
            Console.WriteLine("--------Liste des livres de notre bibliothèque--------");
            Console.WriteLine();

            foreach (var livre in Livres)
            {
                Console.WriteLine($"Titre: {livre.Titre}, Auteur: {livre.Auteur}, ISBN: {livre.Isbn}, Statut: {livre.Statut}");
            }
            Console.WriteLine();
            Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }
    }
}
