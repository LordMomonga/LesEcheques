// Étudiant 2 : Samuel
namespace TP3EchecsETU
{
    public class JoueurOrdi : IJoueur
    {
        public Couleur Couleur { get; }
        public List<Piece> Pieces { get; } = new();

        public JoueurOrdi(Couleur couleur)
        {
            Couleur = couleur;
        }

        public void AjouterPiece(Piece piece) => Pieces.Add(piece);

        public void RetirerPiece(Piece piece) => Pieces.Remove(piece);

        public Coup? ChoisirCoup(Plateau plateau)
        {
            // Mettre à jour tous les coups possibles
            foreach (var piece in Pieces.ToList())
                piece.MettreAJourCoupsPossibles(plateau);

            // Trouver le coup qui capture la pièce adverse la plus précieuse
            Coup? meilleurCoup = null;
            int meilleureValeur = -1;

            foreach (var piece in Pieces)
            {
                foreach (var coup in piece.CoupsPossibles)
                {
                    if (coup.PieceCaptured != null && coup.PieceCaptured.Valeur > meilleureValeur)
                    {
                        meilleureValeur = coup.PieceCaptured.Valeur;
                        meilleurCoup = coup;
                    }
                }
            }

            if (meilleurCoup != null)
                return meilleurCoup;

            // Aucune capture possible : jouer le premier coup disponible de la pièce avec la plus grande valeur
            var piecesAvecCoups = Pieces
                .Where(p => p.CoupsPossibles.Count > 0)
                .OrderByDescending(p => p.Valeur)
                .ToList();

            return piecesAvecCoups.FirstOrDefault()?.CoupsPossibles.FirstOrDefault();
        }
    }
}
