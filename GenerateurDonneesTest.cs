using System;
using System.Collections.Generic;

namespace GestionBibliotheque
{
    public static class GenerateurDonneesTest
    {
        // Générer des utilisateurs de test
        public static void GenererUtilisateursTest(Bibliotheque maBibliotheque)
        {
            for (int i = 1; i < 10; i++)
            {
                string nom = $"Utilisateur{i}";
                string prenom = $"Prenom{i}";
                string adresse = $"Adresse{i}";
                int telephone = 1000000000 + i;
                string courriel = $"utilisateur{i}@example.com";
                int motDePasse = 1000 + i;

                Utilisateur utilisateur = new Utilisateur(nom, prenom, adresse, telephone, courriel, motDePasse, maBibliotheque);
                maBibliotheque.AjouterUtilisateur(utilisateur);
            }
        }

        // Générer des livres de test
        public static void GenererLivresTest(Bibliotheque bibliotheque)
        {
            for (int index = 1; index < 10; index++)
            {
                string titre = $"Livre{index}";
                string auteur = $"Auteur{index}";
                string isbn = $"ISBN{index}";
                string statut = "Disponible";

                Livre livre = new Livre(titre, auteur, isbn, statut);
                bibliotheque.AjouterLivre(livre);
            }
        }
    }
}