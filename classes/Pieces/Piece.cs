using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public abstract class Piece : IDeplacable
    {
        public Type Type { get; protected set; }
        public Couleur Couleur { get; protected set; }
        public Position Position { get; set; }
        public int Valeur { get; protected set; }
        public List<Coup> CoupsPossibles { get; protected set; } = new();
        public bool ADejaBouge { get; set; } = false;

        protected Piece(Type type, Couleur couleur, Position position, int valeur)
        {
            Type = type;
            Couleur = couleur;
            Position = position;
            Valeur = valeur;
        }

        public abstract void MettreAJourCoupsPossibles(Plateau plateau);

        /// <summary>
        /// Ajoute un coup si la destination est valide. Retourne true si la case était occupée
        /// par un ennemi (on peut capturer mais on s'arrête), false si vide (on continue).
        /// Retourne null si bloqué par allié.
        /// </summary>
        protected bool? TenterAjouterCoup(Plateau plateau, int ligne, int colonne)
        {
            var dest = new Position(ligne, colonne);
            if (!dest.EstValide())
                return null;

            Piece? cible = plateau.ObtenirPiece(dest);

            if (cible == null)
            {
                CoupsPossibles.Add(
                    new Coup(this, new Position(Position.Ligne, Position.Colonne), dest)
                );
                return false; // case vide, continuer
            }
            else if (cible.Couleur != Couleur)
            {
                CoupsPossibles.Add(
                    new Coup(this, new Position(Position.Ligne, Position.Colonne), dest, cible)
                );
                return true; // ennemi capturé, arrêter
            }
            else
            {
                return null; // allié, arrêter sans ajouter
            }
        }
    }
}
