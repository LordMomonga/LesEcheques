using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Plateau
    {
        private Piece?[,] _cases;

        // Constructeur de base : position initiale d'une partie d'échecs standard
        public Plateau()
        {
            _cases = new Piece?[Program.TAILLE_PLATEAU, Program.TAILLE_PLATEAU];
            InitialiserPartieStandard();
        }

        // Constructeur avec tableau personnalisé (pour les tests)
        public Plateau(Piece?[,] cases)
        {
            _cases = cases;
        }

        public Piece? ObtenirPiece(Position pos)
        {
            if (!pos.EstValide())
                return null;
            return _cases[pos.Ligne, pos.Colonne];
        }

        public void PlacerPiece(Piece piece, Position pos)
        {
            _cases[pos.Ligne, pos.Colonne] = piece;
            piece.Position = pos;
        }

        public void RetirerPiece(Position pos)
        {
            _cases[pos.Ligne, pos.Colonne] = null;
        }

        public void DeplacerPiece(Coup coup)
        {
            RetirerPiece(coup.Origine);
            PlacerPiece(coup.Piece, coup.Destination);
            coup.Piece.ADejaBouge = true;
        }

        public void MettreAJourTousLesCoupsPossibles()
        {
            foreach (var piece in ObtenirToutesLesPieces())
            {
                piece.MettreAJourCoupsPossibles(this);
            }
        }

        public List<Piece> ObtenirToutesLesPieces()
        {
            var liste = new List<Piece>();
            for (int l = 0; l < Program.TAILLE_PLATEAU; l++)
            for (int c = 0; c < Program.TAILLE_PLATEAU; c++)
                if (_cases[l, c] != null)
                    liste.Add(_cases[l, c]!);
            return liste;
        }

        public List<Piece> ObtenirPiecesCouleur(Couleur couleur)
        {
            return ObtenirToutesLesPieces().Where(p => p.Couleur == couleur).ToList();
        }

        public bool RoiEstEnJeu(Couleur couleur)
        {
            return ObtenirPiecesCouleur(couleur).Any(p => p.Type == Type.Roi);
        }

        private void InitialiserPartieStandard()
        {
            // Pièces noires (ligne 0 = haut)
            PlacerPiece(new Tour(Couleur.Noir, new Position(0, 0)), new Position(0, 0));
            PlacerPiece(new Cavalier(Couleur.Noir, new Position(0, 1)), new Position(0, 1));
            PlacerPiece(new Fou(Couleur.Noir, new Position(0, 2)), new Position(0, 2));
            PlacerPiece(new Dame(Couleur.Noir, new Position(0, 3)), new Position(0, 3));
            PlacerPiece(new Roi(Couleur.Noir, new Position(0, 4)), new Position(0, 4));
            PlacerPiece(new Fou(Couleur.Noir, new Position(0, 5)), new Position(0, 5));
            PlacerPiece(new Cavalier(Couleur.Noir, new Position(0, 6)), new Position(0, 6));
            PlacerPiece(new Tour(Couleur.Noir, new Position(0, 7)), new Position(0, 7));

            for (int c = 0; c < Program.TAILLE_PLATEAU; c++)
                PlacerPiece(new Pion(Couleur.Noir, new Position(1, c)), new Position(1, c));

            // Pièces blanches (ligne 7 = bas)
            PlacerPiece(new Tour(Couleur.Blanc, new Position(7, 0)), new Position(7, 0));
            PlacerPiece(new Cavalier(Couleur.Blanc, new Position(7, 1)), new Position(7, 1));
            PlacerPiece(new Fou(Couleur.Blanc, new Position(7, 2)), new Position(7, 2));
            PlacerPiece(new Dame(Couleur.Blanc, new Position(7, 3)), new Position(7, 3));
            PlacerPiece(new Roi(Couleur.Blanc, new Position(7, 4)), new Position(7, 4));
            PlacerPiece(new Fou(Couleur.Blanc, new Position(7, 5)), new Position(7, 5));
            PlacerPiece(new Cavalier(Couleur.Blanc, new Position(7, 6)), new Position(7, 6));
            PlacerPiece(new Tour(Couleur.Blanc, new Position(7, 7)), new Position(7, 7));

            for (int c = 0; c < Program.TAILLE_PLATEAU; c++)
                PlacerPiece(new Pion(Couleur.Blanc, new Position(6, c)), new Position(6, c));
        }
    }
}
