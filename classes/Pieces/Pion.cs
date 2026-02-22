using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Pion : Piece
    {
        public Pion(Couleur couleur, Position position)
            : base(Type.Pion, couleur, position, 1) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            // Le blanc avance vers les lignes décroissantes (ligne 7 -> ligne 0)
            // Le noir avance vers les lignes croissantes (ligne 0 -> ligne 7)
            int direction = Couleur == Couleur.Blanc ? -1 : 1;

            int ligneSuivante = Position.Ligne + direction;
            int col = Position.Colonne;

            // Avancer d'une case (seulement si vide)
            if (
                new Position(ligneSuivante, col).EstValide()
                && plateau.ObtenirPiece(new Position(ligneSuivante, col)) == null
            )
            {
                CoupsPossibles.Add(
                    new Coup(
                        this,
                        new Position(Position.Ligne, col),
                        new Position(ligneSuivante, col)
                    )
                );

                // Avancer de deux cases au premier mouvement
                int ligneDeux = Position.Ligne + direction * 2;
                bool ligneDepart =
                    (Couleur == Couleur.Blanc && Position.Ligne == 6)
                    || (Couleur == Couleur.Noir && Position.Ligne == 1);

                if (
                    !ADejaBouge
                    && ligneDepart
                    && plateau.ObtenirPiece(new Position(ligneDeux, col)) == null
                )
                {
                    CoupsPossibles.Add(
                        new Coup(
                            this,
                            new Position(Position.Ligne, col),
                            new Position(ligneDeux, col)
                        )
                    );
                }
            }

            // Captures en diagonale
            foreach (int deltaCol in new[] { -1, 1 })
            {
                var posDiag = new Position(ligneSuivante, col + deltaCol);
                if (posDiag.EstValide())
                {
                    Piece? cible = plateau.ObtenirPiece(posDiag);
                    if (cible != null && cible.Couleur != Couleur)
                    {
                        CoupsPossibles.Add(
                            new Coup(this, new Position(Position.Ligne, col), posDiag, cible)
                        );
                    }
                }
            }
        }
    }
}
