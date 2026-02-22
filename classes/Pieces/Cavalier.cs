using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Audrey

namespace TP3EchecsETU
{
    public class Cavalier : Piece
    {
        public Cavalier(Couleur couleur, Position position)
            : base(Type.Cavalier, couleur, position, 3) { }

        public override void MettreAJourCoupsPossibles(Plateau plateau)
        {
            CoupsPossibles.Clear();

            int[][] deplacements =
            {
                new[] { -2, -1 },
                new[] { -2, 1 },
                new[] { -1, -2 },
                new[] { -1, 2 },
                new[] { 1, -2 },
                new[] { 1, 2 },
                new[] { 2, -1 },
                new[] { 2, 1 },
            };

            foreach (var dep in deplacements)
            {
                TenterAjouterCoup(plateau, Position.Ligne + dep[0], Position.Colonne + dep[1]);
            }
        }
    }
}
