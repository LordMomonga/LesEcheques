global using TP3EchecsETU;
using System.Drawing;
using System.Text;

namespace TP3EchecsETU
{
    public class Program
    {
        public const int TAILLE_PLATEAU = 8;
        public const int LIGNE_CURSEUR_DEFAUT = 11;
        public const int COLONNE_CURSEUR_DEFAUT = 0;
        public const int TAILLE_CASE_COLONNES = 2;

        private static readonly Dictionary<Type, char> symbolesPieces = new()
        {
            { Type.Pion, '♙' },
            { Type.Fou, '♗' },
            { Type.Cavalier, '♘' },
            { Type.Tour, '♖' },
            { Type.Dame, '♕' },
            { Type.Roi, '♔' },
        };

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool rejouer = true;
            while (rejouer)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════╗");
                Console.WriteLine("║       ♔ JEU D'ÉCHECS ♔       ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.WriteLine();

                IJoueur joueurBlanc = ChoisirJoueur(Couleur.Blanc);
                IJoueur joueurNoir = ChoisirJoueur(Couleur.Noir);

                Partie partie = new Partie(joueurBlanc, joueurNoir);
                partie.Jouer();

                Console.WriteLine("\nVoulez-vous rejouer? (o/n)");
                string? rep = Console.ReadLine();
                rejouer = rep?.Trim().ToLower() == "o";
            }

            Console.WriteLine("Merci d'avoir joué! À bientôt.");
        }

        private static IJoueur ChoisirJoueur(Couleur couleur)
        {
            while (true)
            {
                Console.WriteLine($"Type de joueur pour les {couleur}:");
                Console.WriteLine("  1 - Humain");
                Console.WriteLine("  2 - IA (joue le meilleur coup)");
                Console.WriteLine("  3 - Aléatoire");
                Console.Write("Votre choix : ");

                string? choix = Console.ReadLine();
                return choix?.Trim() switch
                {
                    "1" => new JoueurHumain(couleur),
                    "2" => new JoueurOrdi(couleur),
                    "3" => new JoueurRandom(couleur),
                    _ => InvalidChoix(couleur),
                };
            }
        }

        private static IJoueur InvalidChoix(Couleur couleur)
        {
            Console.WriteLine("Choix invalide, entrez 1, 2 ou 3.");
            return ChoisirJoueur(couleur); // récursif
        }

        // Dessine le plateau vide
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
                Console.Write($" {colonne + 1}");

            Console.WriteLine();
        }

        // Dessine le plateau avec toutes les pièces dessus
        public static void DessinerPlateauAvecPieces(Plateau plateau)
        {
            Console.SetCursorPosition(0, 0);

            for (int ligne = 0; ligne < TAILLE_PLATEAU; ligne++)
            {
                for (int colonne = 0; colonne < TAILLE_PLATEAU; colonne++)
                {
                    DeterminerCouleurArrierePlan(ligne, colonne);
                    Piece? piece = plateau.ObtenirPiece(new Position(ligne, colonne));

                    if (piece != null)
                    {
                        Console.ForegroundColor =
                            piece.Couleur == Couleur.Blanc
                                ? ConsoleColor.White
                                : ConsoleColor.Black;
                        Console.Write(symbolesPieces[piece.Type] + " ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.ResetColor();
                Console.WriteLine($" {ligne + 1}");
            }

            for (int colonne = 0; colonne < TAILLE_PLATEAU; colonne++)
                Console.Write($" {colonne + 1}");

            Console.WriteLine();
            Console.SetCursorPosition(COLONNE_CURSEUR_DEFAUT, LIGNE_CURSEUR_DEFAUT);
        }

        private static void DeterminerCouleurArrierePlan(int ligne, int colonne)
        {
            Console.BackgroundColor =
                (ligne + colonne) % 2 == 0 ? ConsoleColor.Blue : ConsoleColor.DarkBlue;
        }

        private static void DessinerPiece(Type type, Couleur couleur, int ligne, int colonne)
        {
            Console.SetCursorPosition(colonne * TAILLE_CASE_COLONNES, ligne);
            DeterminerCouleurArrierePlan(ligne, colonne);
            Console.ForegroundColor =
                couleur == Couleur.Blanc ? ConsoleColor.White : ConsoleColor.Black;
            Console.Write(symbolesPieces[type]);
            Console.ResetColor();
            Console.SetCursorPosition(COLONNE_CURSEUR_DEFAUT, LIGNE_CURSEUR_DEFAUT);
        }
    }
}
