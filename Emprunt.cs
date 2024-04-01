using System;

namespace GestionBibliotheque
{
    public class Emprunt
    {
        // Attributs
        private string livreEmprunte;
        private int utilisateurEmprunte;
        private DateTime dateEmprunt;
        private DateTime dateRetourReelle;

        // Constructeurs
        public Emprunt() { }

        public Emprunt(string livreEmprunte, int utilisateurEmprunte, DateTime dateEmprunt)
        {
            this.livreEmprunte = livreEmprunte;
            this.utilisateurEmprunte = utilisateurEmprunte;
            this.dateEmprunt = dateEmprunt;
        }

        public Emprunt(string livreEmprunte, int utilisateurEmprunte, DateTime dateEmprunt, DateTime dateRetourReelle)
        {
            this.livreEmprunte = livreEmprunte;
            this.utilisateurEmprunte = utilisateurEmprunte;
            this.dateEmprunt = dateEmprunt;
            this.dateRetourReelle = dateRetourReelle;
        }

        // Getters et setters
        public string LivreEmprunte
        {
            get { return livreEmprunte; }
            set { livreEmprunte = value; }
        }

        public int UtilisateurEmprunte
        {
            get { return utilisateurEmprunte; }
            set { utilisateurEmprunte = value; }
        }

        public DateTime DateEmprunt
        {
            get { return dateEmprunt; }
            set { dateEmprunt = value; }
        }

        public DateTime DateRetourReelle
        {
            get { return dateRetourReelle; }
            set { dateRetourReelle = value; }
        }

        //Méthode
        public void Emprunter(Utilisateur utilisateurActuel, Bibliotheque maBibliotheque)
        {
            Console.WriteLine("--------Emprunter un Livre--------");
            Console.WriteLine();
            Console.WriteLine("Entrer le titre du livre que vous désirez emprunter : ");
            string titreRecherche = Console.ReadLine();

            bool livreEmprunte = false;

            foreach (var livre in maBibliotheque.Livres)
            {
                if (livre.Titre.ToLower() == titreRecherche.ToLower() && livre.Statut == "Disponible")
                {
                    Emprunt emprunt = new Emprunt(livre.Titre, utilisateurActuel.NumeroCarte, DateTime.Now);

                    livre.Statut = "Emprunté";
                    livre.DateRetourPrevue = emprunt.dateEmprunt.AddDays(31);
                    utilisateurActuel.Emprunts.Add(livre);
                    utilisateurActuel.HistoriqueEmprunts.Add(emprunt);
                    maBibliotheque.HistoriqueDesPrets.Add(emprunt);
                    livreEmprunte = true;

                    Console.WriteLine($"{livre.Titre} est maintenant emprunté et doit être retourné le {livre.DateRetourPrevue.ToShortDateString()}.");
                    Console.WriteLine();
                    Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                    Console.ReadKey();
                    break;
                }
            }

            if (utilisateurActuel.Reservations.Count > 0)
            {
                foreach (var livre in utilisateurActuel.Reservations)
                {
                    if (livre.Titre.ToLower() == titreRecherche.ToLower() && livre.Statut == "Réservé")
                    {
                        Emprunt emprunt = new Emprunt(livre.Titre, utilisateurActuel.NumeroCarte, DateTime.Now);
                        
                        livre.Statut = "Emprunté";
                        livre.DateRetourPrevue = emprunt.dateEmprunt.AddDays(31);
                        utilisateurActuel.Emprunts.Add(livre);
                        utilisateurActuel.Reservations.Remove(livre);
                        utilisateurActuel.HistoriqueEmprunts.Add(emprunt);
                        maBibliotheque.HistoriqueDesPrets.Add(emprunt);
                        livreEmprunte = true;

                        Console.WriteLine($"{livre.Titre} est maintenant emprunté et doit être retourné le {livre.DateRetourPrevue.ToShortDateString()}.");
                        Console.WriteLine();
                        Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                        Console.ReadKey();
                        break;
                    }
                }
            }

            if (!livreEmprunte)
            {
                Console.WriteLine($"Le livre n'est pas disponible ");
                Console.WriteLine();
                Console.WriteLine("Appuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
            }
        }
    }
}
