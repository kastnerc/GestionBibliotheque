using System;

namespace GestionBibliotheque
{
    class Program
    {
        static void Main()
        {
            // Message de bienvenue
            Console.WriteLine("--------Bibliothèque Kastner--------");
            Console.WriteLine();
            Console.WriteLine("Bienvenue à notre bibliothèque");
            Console.WriteLine("Souhaitez-vous vous inscrire ou vous connecter ?");
            Console.WriteLine("1. Inscription");
            Console.WriteLine("2. Connexion");
            Console.WriteLine();

            string choix = Console.ReadLine();

            Bibliotheque maBibliotheque = new Bibliotheque();
            Livre mesLivres = new Livre();
            Emprunt mesEmprunts = new Emprunt();

            GenerateurDonneesTest.GenererUtilisateursTest(maBibliotheque);
            GenerateurDonneesTest.GenererLivresTest(maBibliotheque);

            Utilisateur utilisateurActuel = new();

            bool action = false;
            do
            {
                if (choix == "1")
                {
                    Console.Clear();
                    utilisateurActuel = new Utilisateur();
                    utilisateurActuel.Inscription(maBibliotheque);
                    choix = "2"; // Pour aller à la connexion après avoir finie l'inscription
                }
                else if (choix == "2")
                {
                    Console.Clear();
                    utilisateurActuel = new Utilisateur();
                    utilisateurActuel.Connexion(maBibliotheque);
                    utilisateurActuel = maBibliotheque.Utilisateurs.First(utilisateur => utilisateur.NumeroCarte == utilisateurActuel.numeroDeCarteConnexion);
                    action = true;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Entrer '1' pour s'inscrire, et '2' pour se connecter");
                    choix = Console.ReadLine();
                }
            } while (action == false);

            action = true;

            // Menu principal
            while (action)
            {
                Console.Clear();
                Console.WriteLine("--------Menu d'Utilisateur--------");
                Console.WriteLine();
                Console.WriteLine($"Bienvenue {utilisateurActuel.Prenom} {utilisateurActuel.Nom}");
                Console.WriteLine("Que souhaitez-vous faire aujourd'hui (rentrer le numéro d'une des options suivantes) ?");
                Console.WriteLine("1. Voir mes emprunts");
                Console.WriteLine("2. Voir mes réservations");
                Console.WriteLine("3. Voir mon historique d'emprunts");
                Console.WriteLine("4. Voir livres disponibles");
                Console.WriteLine("5. Voir mes informations");
                Console.WriteLine("6. Emprunter");
                Console.WriteLine("7. Réserver");
                Console.WriteLine("8. Retourner");
                Console.WriteLine("9. Rechercher");
                Console.WriteLine("0. Quitter");
                Console.WriteLine();

                choix = Console.ReadLine().ToLower();

                switch (choix)
                {
                    case "1":
                        Console.Clear();
                        utilisateurActuel.AffichageEmprunts();
                        break;

                    case "2":
                        Console.Clear();
                        utilisateurActuel.AffichageReservations();
                        break;

                    case "3":
                        Console.Clear();
                        utilisateurActuel.AffichageHistoriqueEmprunts();
                        break;

                    case "4":
                        Console.Clear();
                        maBibliotheque.AffichageLivres();
                        break;

                    case "5":
                        Console.Clear();
                        utilisateurActuel.AffichageInformation();
                        break;

                    case "6":
                        Console.Clear();
                        mesEmprunts.Emprunter(utilisateurActuel, maBibliotheque);
                        break;

                    case "7":
                        Console.Clear();
                        mesLivres.Reserver(utilisateurActuel, maBibliotheque);
                        break;

                    case "8":
                        Console.Clear();
                        mesLivres.Retourner(utilisateurActuel);
                        break;

                    case "9":
                        Console.Clear();
                        maBibliotheque.RechercheLivre();
                        break;

                    case "0":
                        Console.Clear();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour quitter...");
                        Console.ReadKey();
                        action = false;
                        break;

                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
