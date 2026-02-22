using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public interface IDeplacable
    {
        /// <summary>
        /// Met à jour la liste des coups possibles pour cette pièce selon l'état actuel du plateau.
        /// </summary>
        void MettreAJourCoupsPossibles(Plateau plateau);
    }
}
