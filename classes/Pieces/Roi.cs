using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Samuel

namespace TP3EchecsETU
{
    public class Roi : Piece
    {
        public Roi(Couleur couleur, Position position)
            : base(Type.Roi, couleur, position, 1000) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            // Toutes les 8 directions, 1 case
            for (int dl = -1; dl <= 1; dl++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    if (dl == 0 && dc == 0)
                        continue;
                    TenterAjouterCoup(plateau, Position.Ligne + dl, Position.Colonne + dc);
                }
            }
        }
    }
}
