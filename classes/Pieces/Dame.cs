using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Dame : Piece
    {
        public Dame(Couleur couleur, Position position)
            : base(Type.Dame, couleur, position, 9) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            // Dame = Tour + Fou : toutes les 8 directions
            int[][] directions =
            {
                new[] { -1, 0 },
                new[] { 1, 0 },
                new[] { 0, -1 },
                new[] { 0, 1 }, // Tour
                new[] { 1, 1 },
                new[] { 1, -1 },
                new[] { -1, 1 },
                new[] { -1, -1 }, // Fou
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
