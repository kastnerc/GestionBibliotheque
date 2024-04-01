using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GestionBibliotheque
{
    public class Utilisateur
    {
        // Attributs
        private string nom;
        private string prenom;
        private string adresse;
        private long telephone;
        private string courriel;
        private int numeroCarte;
        private int motDePasse;
        public int numeroDeCarteConnexion;
        public int motDePasseConnexion;

        public static int numeroDeCarteBase = 10000000;

        public List<Emprunt> HistoriqueEmprunts = new List<Emprunt>();
        public List<Livre> Reservations = new List<Livre>();
        public List<Livre> Emprunts = new List<Livre>();

        // Constructeur et destructeur
        public Utilisateur() { }

        ~Utilisateur() { }

        public Utilisateur(string nom, string prenom, string adresse, int telephone, string courriel, int motDePasse, Bibliotheque maBibliotheque)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.telephone = telephone;
            this.courriel = courriel;
            this.numeroCarte = ++numeroDeCarteBase;
            this.motDePasse = motDePasse;
            maBibliotheque.AjouterUtilisateur(this);
        }

        // Getters et Setters
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public long Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public string Courriel
        {
            get { return courriel; }
            set { courriel = value; }
        }

        public int NumeroCarte
        {
            get { return numeroCarte; }
            set { numeroCarte = value; }
        }

        public int MotDePasse
        {
            get { return motDePasse; }
            set { motDePasse = value; }
        }

        // Méthodes
        public void Inscription(Bibliotheque maBibliotheque)
        {
            Console.WriteLine("--------Inscription--------");
            Console.WriteLine();

            Console.WriteLine("Entrer votre prénom : ");
            prenom = Console.ReadLine();

            Console.WriteLine("Entrer votre nom : ");
            nom = Console.ReadLine();

            Console.WriteLine("Entrer votre adresse (numéro civique + nom de rue, ville, code postal, pays) : ");
            adresse = Console.ReadLine();

            // Vérification que le numéro de téléphone est valide
            bool NumeroTelephoneValide;
            do
            {
                Console.WriteLine("Entrer votre numéro de téléphone : ");
                NumeroTelephoneValide = long.TryParse(Console.ReadLine(), out telephone);
                if (!NumeroTelephoneValide)
                {
                    Console.WriteLine("Le numéro de téléphone doit être un nombre entier. Réessayer");
                }
            } while (!NumeroTelephoneValide);

            Console.WriteLine("Entrer votre courriel : ");
            courriel = Console.ReadLine();

            this.numeroCarte = ++numeroDeCarteBase;
            Console.WriteLine($"Voici votre numéro de carte : {numeroCarte}");

            bool motDePasseIdentique = false;
            do
            {
                //Vérification que le PIN est de 4 chiffres
                int motDePasse;
                bool isMotDePasseValide;
                do
                {
                    Console.WriteLine("Entrer votre mot de passe (PIN de 4 chiffres) : ");
                    string inputMotDePasse = Console.ReadLine();
                    isMotDePasseValide = int.TryParse(inputMotDePasse, out motDePasse) && inputMotDePasse.Length == 4;
                    if (!isMotDePasseValide)
                    {
                        Console.WriteLine("Le mot de passe doit être un nombre entier à quatre chiffres.");
                    }
                } while (!isMotDePasseValide);

                this.motDePasse = motDePasse;

                Console.WriteLine("Confirmer votre mot de passe : ");
                int motDePasse2 = Convert.ToInt32(Console.ReadLine());

                //Verification que les mots de passe correspondent
                if (motDePasse == motDePasse2)
                {
                    motDePasseIdentique = true;
                }
                else
                {
                    Console.WriteLine("Votre mot de passe ne correspond pas à ceci. Réessayer");
                }
            } while (!motDePasseIdentique);

            maBibliotheque.AjouterUtilisateur(this);

            Console.WriteLine();
            Console.WriteLine("Vous êtes maintenant inscrit.");
            Console.WriteLine();
            Console.WriteLine("Appuyez n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }

        public void Connexion(Bibliotheque maBibliotheque)
        {
            bool connexionReussie;
            do
            {
                Console.WriteLine("--------Connexion--------");
                Console.WriteLine();
                Console.WriteLine("Entrer votre numéro de carte : ");
                int.TryParse(Console.ReadLine(), out int numCarte);
                numeroDeCarteConnexion = numCarte;

                Console.WriteLine("Entrer votre mot de passe : ");
                int.TryParse(Console.ReadLine(), out int mdp);
                motDePasseConnexion = mdp;

                connexionReussie = maBibliotheque.VerificationInformation(this);

                if (connexionReussie)
                {
                    Console.WriteLine("Connexion réussie");
                }
                else
                {
                    Console.WriteLine("Il n'y a pas d'utilisateur avec ce numéro de carte et mot de passe.");
                    Console.WriteLine();
                    Console.WriteLine("Appuyez n'importe quelle touche pour réessayer...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!connexionReussie);

        }
        //Methode pour l'historique d'emprunts
        
        public void AffichageHistoriqueEmprunts()
        {
            if (HistoriqueEmprunts.Count > 0)
            {
                Console.WriteLine("--------Votre Historique d'Emprunts--------");

                foreach (var emprunt in HistoriqueEmprunts)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{emprunt.LivreEmprunte}");

                    if (emprunt.DateRetourReelle == new DateTime(1, 1, 1, 0, 0, 0))
                    {
                        Console.WriteLine("Le livre n'a pas encore été retourné");
                    }
                    else
                    {
                        Console.WriteLine($"Date du retour : {emprunt.DateRetourReelle}");
                    }
                }
            }
            else
            {
                Console.WriteLine("--------Votre Historique d'Emprunts--------");
                Console.WriteLine();
                Console.WriteLine("Aucun livre emprunté.");
            }
            Console.WriteLine();
            Console.WriteLine("Appuyez n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }
       
        public void AffichageEmprunts()
        {
            if (Emprunts.Count > 0)
            {
                Console.WriteLine("--------Vos Emprunts--------");

                foreach (var livre in Emprunts)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{livre.Titre}");
                    Console.WriteLine($"Ce livre doit être retourné à la date suivante : {livre.DateRetourPrevue}");
                }
            }
            else
            {
                Console.WriteLine("--------Vos Emprunts--------");
                Console.WriteLine();
                Console.WriteLine("Aucun livre emprunté.");
            }
            Console.WriteLine();
            Console.WriteLine("Appuyez n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }

        public void AffichageReservations()
        {
            if (Reservations.Count > 0)
            {
                Console.WriteLine("--------Vos Réservations--------");

                foreach (var livre in Reservations)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{livre.Titre}");
                }
            }
            else
            {
                Console.WriteLine("--------Vos Réservations--------");
                Console.WriteLine();
                Console.WriteLine("Aucun livre réservé.");
            }
            Console.WriteLine();
            Console.WriteLine("Appuyez n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }

        public void AffichageInformation()
        {
            Console.WriteLine("--------Votre Information--------");
            Console.WriteLine();
            Console.WriteLine($"Prénom : {prenom}");
            Console.WriteLine($"Nom : {nom}");
            Console.WriteLine($"Adresse : {adresse}");
            Console.WriteLine($"Téléphone : {telephone}");
            Console.WriteLine($"Courriel : {courriel}");
            Console.WriteLine();
            Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
            Console.ReadKey();
        }
    }
}
