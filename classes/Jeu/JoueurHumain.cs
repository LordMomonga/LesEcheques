// Louis-Émile

namespace TP3EchecsETU
{
    public class JoueurHumain : IJoueur
    {
        public Couleur Couleur { get; }
        public List<Piece> Pieces { get; } = new();

        public JoueurHumain(Couleur couleur)
        {
            Couleur = couleur;
        }

        public void AjouterPiece(Piece piece) => Pieces.Add(piece);

        public void RetirerPiece(Piece piece) => Pieces.Remove(piece);

        public Coup? ChoisirCoup(Plateau plateau)
        {
            while (true)
            {
                // Étape 1 : choisir la pièce à déplacer
                Console.WriteLine(
                    $"\n[{Couleur}] Entrez la position de la pièce à déplacer (ligne colonne, ex: 2 3) :"
                );
                Position? origine = LirePosition();
                if (origine == null)
                    continue;

                Piece? piece = plateau.ObtenirPiece(origine);

                if (piece == null)
                {
                    Console.WriteLine("Aucune pièce à cette position. Réessayez.");
                    continue;
                }

                if (piece.Couleur != Couleur)
                {
                    Console.WriteLine("Cette pièce ne vous appartient pas. Réessayez.");
                    continue;
                }

                piece.MettreAJourCoupsPossibles(plateau);

                if (piece.CoupsPossibles.Count == 0)
                {
                    Console.WriteLine(
                        "Cette pièce n'a aucun coup possible. Choisissez-en une autre."
                    );
                    continue;
                }

                // Afficher les coups possibles
                Console.WriteLine($"Coups possibles pour {piece.Type} en {origine} :");
                foreach (var c in piece.CoupsPossibles)
                    Console.WriteLine(
                        $"  -> {c.Destination}"
                            + (c.PieceCaptured != null ? $" (capture {c.PieceCaptured.Type})" : "")
                    );

                // Étape 2 : choisir la destination
                Console.WriteLine("Entrez la position de destination (ligne colonne) :");
                Position? destination = LirePosition();
                if (destination == null)
                    continue;

                Coup? coup = piece.CoupsPossibles.FirstOrDefault(c =>
                    c.Destination.Equals(destination)
                );

                if (coup == null)
                {
                    Console.WriteLine("Coup invalide. Réessayez.");
                    continue;
                }

                return coup;
            }
        }

        private Position? LirePosition()
        {
            try
            {
                string? entree = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entree))
                    return null;

                string[] parts = entree.Trim().Split(' ');
                if (parts.Length != 2)
                    throw new FormatException();

                int ligne = int.Parse(parts[0]) - 1;
                int colonne = int.Parse(parts[1]) - 1;

                var pos = new Position(ligne, colonne);
                if (!pos.EstValide())
                {
                    Console.WriteLine("Position hors du plateau (1-8). Réessayez.");
                    return null;
                }

                return pos;
            }
            catch
            {
                Console.WriteLine(
                    "Format invalide. Entrez deux nombres séparés par un espace (ex: 2 3)."
                );
                return null;
            }
        }
    }
}
