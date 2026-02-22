using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel

namespace TP3EchecsETU
{
    public class Tour : Piece
    {
        public Tour(Couleur couleur, Position position)
            : base(Type.Tour, couleur, position, 5) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            // 4 directions : haut, bas, gauche, droite
            int[][] directions =
            {
                new[] { -1, 0 },
                new[] { 1, 0 },
                new[] { 0, -1 },
                new[] { 0, 1 },
            };

            foreach (var dir in directions)
            {
                int l = Position.Ligne + dir[0];
                int c = Position.Colonne + dir[1];

                while (new Position(l, c).EstValide())
                {
                    bool? resultat = TenterAjouterCoup(plateau, l, c);
                    if (resultat == null || resultat == true)
                        break;
                    l += dir[0];
                    c += dir[1];
                }
            }
        }
    }
}
