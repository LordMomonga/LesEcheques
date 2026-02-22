// Étudiant 3 : Audrey

namespace TP3EchecsETU
{
    public class JoueurRandom : IJoueur
    {
        public Couleur Couleur { get; }
        public List<Piece> Pieces { get; } = new();

        private static readonly Random _random = new();

        public JoueurRandom(Couleur couleur)
        {
            Couleur = couleur;
        }

        public void AjouterPiece(Piece piece) => Pieces.Add(piece);

        public void RetirerPiece(Piece piece) => Pieces.Remove(piece);

        public Coup? ChoisirCoup(Plateau plateau)
        {
            foreach (var piece in Pieces.ToList())
                piece.MettreAJourCoupsPossibles(plateau);

            // Filtrer les pièces qui ont des coups disponibles
            var piecesDisponibles = Pieces.Where(p => p.CoupsPossibles.Count > 0).ToList();
            if (piecesDisponibles.Count == 0)
                return null;

            // Choisir une pièce aléatoire
            Piece pieceChoisie = piecesDisponibles[_random.Next(piecesDisponibles.Count)];

            // Choisir un coup aléatoire
            return pieceChoisie.CoupsPossibles[_random.Next(pieceChoisie.CoupsPossibles.Count)];
        }
    }
}
