using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Position
    {
        public int Ligne { get; set; }
        public int Colonne { get; set; }

        public Position(int ligne, int colonne)
        {
            Ligne = ligne;
            Colonne = colonne;
        }

        public bool EstValide()
        {
            return Ligne >= 0
                && Ligne < Program.TAILLE_PLATEAU
                && Colonne >= 0
                && Colonne < Program.TAILLE_PLATEAU;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Position autre)
                return Ligne == autre.Ligne && Colonne == autre.Colonne;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Ligne, Colonne);

        public override string ToString() => $"({Ligne + 1}, {Colonne + 1})";
    }
}
