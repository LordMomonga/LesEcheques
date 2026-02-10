using System.Drawing;
using System.Text;

namespace TP3EchecsETU
{
    public class Program
    {
        // PROF : vous pourriez avoir besoin de ces constantes, mais devraient-elles toutes être ici? 
        public const int TAILLE_PLATEAU = 8;
        public const int LIGNE_CURSEUR_DEFAUT = 11;
        public const int COLONNE_CURSEUR_DEFAUT = 0;
        public const int TAILLE_CASE_COLONNES = 2;

        // PROF : à décommenter et compléter lorsque votre enum sera prête. Assurez-vous que les symboles correspondent bien à chaque type de pièce. 
        //private static readonly Dictionary<Type, char> symbolesPieces = new()
        //{
        //    { Type.[...], '♙' },
        //    { Type.[...], '♗' },
        //    { Type.[...], '♘' },
        //    { Type.[...], '♖' },
        //    { Type.[...], '♕' },
        //    { Type.[...], '♔' }
        //};

        public static void Main(string[] args)
        {
            // PROF : compléter le Main! Le code suivant sert de base et d'exemple pour l'affichage.
            // Vous pouvez (et devriez) faire des ajustements.
            // Note : le jeu est très petit dans la console, il est donc recommandé de zoomer lorsque vous exécutez le programme. 

            Console.OutputEncoding = Encoding.UTF8;

            DessinerPlateau();

            // PROF : compléter le code de l'affichage ici. 

        }

        private static void DeterminerCouleurArrierePlan(int ligne, int colonne)
        {
            if ((ligne + colonne) % 2 == 0)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
        }

        private static void DessinerPlateau()
        {
            for (int ligne = 0; ligne < TAILLE_PLATEAU; ligne++)
            {
                for (int colonne = 0; colonne < TAILLE_PLATEAU; colonne++)
                {
                    DeterminerCouleurArrierePlan(ligne, colonne);
                    Console.Write("  ");
                }
                Console.ResetColor();
                Console.WriteLine($" {ligne + 1}");
            }

            for (int colonne = 0; colonne < TAILLE_PLATEAU; colonne++)
            {
                Console.Write($" {colonne + 1}");
            }
        }

        private static void DessinerPiece(/*Type type, Couleur couleur,*/ int ligne, int colonne)
        {
            Console.SetCursorPosition(colonne * TAILLE_CASE_COLONNES, ligne);
            DeterminerCouleurArrierePlan(ligne, colonne);
            // PROF : à modifier et décommenter lorsque vos enums seront prêtes.
            // Console.ForegroundColor = couleur == Couleur.[...] ? ConsoleColor.White : ConsoleColor.Black;

            // PROF : à décommenter lorsque votre enum sera prête et que votre dictionnaire plus haut sera complété.
            // Console.Write(symbolesPieces[type]);

            Console.ResetColor();
            Console.SetCursorPosition(COLONNE_CURSEUR_DEFAUT, LIGNE_CURSEUR_DEFAUT);
        }
    }
}
