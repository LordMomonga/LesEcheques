// Mandat commun
namespace TP3EchecsETU
{
    public class Partie
    {
        public Plateau Plateau { get; private set; }
        public IJoueur JoueurBlanc { get; }
        public IJoueur JoueurNoir { get; }
        public List<Coup> Historique { get; } = new();
        public EtatPartie Etat { get; private set; } = EtatPartie.EnCours;
        private string _dernierCoupInfo = "";

        public Partie(IJoueur blanc, IJoueur noir)
        {
            JoueurBlanc = blanc;
            JoueurNoir = noir;
            Plateau = new Plateau();
            AssignerPiecesAuxJoueurs();
        }

        private void AssignerPiecesAuxJoueurs()
        {
            foreach (var piece in Plateau.ObtenirPiecesCouleur(Couleur.Blanc))
                JoueurBlanc.AjouterPiece(piece);

            foreach (var piece in Plateau.ObtenirPiecesCouleur(Couleur.Noir))
                JoueurNoir.AjouterPiece(piece);
        }

        public void Jouer()
        {
            IJoueur joueurActuel = JoueurBlanc; // Les blancs commencent

            while (Etat == EtatPartie.EnCours)
            {
                Console.Clear();
                Program.DessinerPlateauAvecPieces(Plateau);
                AfficherDernierCoup();
                AfficherInfosJoueur(joueurActuel);

                Coup? coup = joueurActuel.ChoisirCoup(Plateau);

                if (coup == null)
                {
                    Console.WriteLine(
                        $"Aucun coup possible pour {joueurActuel.Couleur}. Partie nulle!"
                    );
                    Etat = EtatPartie.Nulle;
                    break;
                }

                // Exécuter le coup
                ExecuterCoup(coup, joueurActuel);

                // Vérifier si la partie est terminée (roi capturé)
                Couleur adversaire =
                    joueurActuel.Couleur == Couleur.Blanc ? Couleur.Noir : Couleur.Blanc;
                if (!Plateau.RoiEstEnJeu(adversaire))
                {
                    Console.Clear();
                    Program.DessinerPlateauAvecPieces(Plateau);
                    Console.WriteLine(
                        $"\n🏆 VICTOIRE des {joueurActuel.Couleur}! Le roi adverse a été capturé."
                    );
                    Etat = EtatPartie.EchecEtMat;
                    break;
                }

                // Changer de joueur
                joueurActuel = joueurActuel == JoueurBlanc ? JoueurNoir : JoueurBlanc;
            }
        }

        private void ExecuterCoup(Coup coup, IJoueur joueur)
        {
            // Retirer la pièce capturée du joueur adverse
            if (coup.PieceCaptured != null)
            {
                IJoueur adversaire = joueur == JoueurBlanc ? JoueurNoir : JoueurBlanc;
                adversaire.RetirerPiece(coup.PieceCaptured);
                _dernierCoupInfo =
                    $"  ✔ {coup}   |   ♟ {coup.PieceCaptured.Type} {coup.PieceCaptured.Couleur} capturé(e)!";
            }
            else
            {
                _dernierCoupInfo = $"  ✔ {coup}";
            }

            // Déplacer la pièce
            Plateau.DeplacerPiece(coup);

            // Ajouter à l'historique
            Historique.Add(coup);
        }

        private void AfficherDernierCoup()
        {
            if (!string.IsNullOrEmpty(_dernierCoupInfo))
                Console.WriteLine(_dernierCoupInfo);
        }

        private void AfficherInfosJoueur(IJoueur joueur)
        {
            string type =
                joueur is JoueurHumain ? "Humain"
                : joueur is JoueurOrdi ? "IA"
                : "Aléatoire";
            Console.WriteLine($"\n--- Tour des {joueur.Couleur} ({type}) ---");
        }
    }
}
