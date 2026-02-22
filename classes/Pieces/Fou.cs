using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Fou : Piece
    {
        public Fou(Couleur couleur, Position position)
            : base(Type.Fou, couleur, position, 3) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            // 4 directions diagonales
            int[][] directions =
            {
                new[] { 1, 1 },
                new[] { 1, -1 },
                new[] { -1, 1 },
                new[] { -1, -1 },
            };

            foreach (var dir in directions)
            {
                int l = Position.Ligne + dir[0];
                int c = Position.Colonne + dir[1];

                while (new Position(l, c).EstValide())
                {
                    bool? resultat = TenterAjouterCoup(plateau, l, c);
                    if (resultat == null || resultat == true)
                        break; // allié ou capture -> arrêt
                    l += dir[0];
                    c += dir[1];
                }
            }
        }
    }
}
