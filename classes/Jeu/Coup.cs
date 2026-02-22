using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3EchecsETU
{
    public class Coup
    {
        public Position Origine { get; }
        public Position Destination { get; }
        public Piece Piece { get; }
        public Piece? PieceCaptured { get; }

        public Coup(
            Piece piece,
            Position origine,
            Position destination,
            Piece? pieceCaptured = null
        )
        {
            Piece = piece;
            Origine = origine;
            Destination = destination;
            PieceCaptured = pieceCaptured;
        }

        public override string ToString()
        {
            string capture = PieceCaptured != null ? $" x {PieceCaptured.Type}" : "";
            return $"{Piece.Type} {Origine} -> {Destination}{capture}";
        }
    }
}
