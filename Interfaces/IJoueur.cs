using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public interface IJoueur
    {
        Couleur Couleur { get; }
        List<Piece> Pieces { get; }

        /// <summary>
        /// Détermine et retourne le coup que ce joueur souhaite jouer.
        /// </summary>
        Coup? ChoisirCoup(Plateau plateau);

        void AjouterPiece(Piece piece);
        void RetirerPiece(Piece piece);
    }
}
