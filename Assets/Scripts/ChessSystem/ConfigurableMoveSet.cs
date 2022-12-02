using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{

    internal delegate List<Position> Collector<TPiece>(Board<TPiece> board, Position fromPosition)
        where TPiece : IPiece;

    internal class ConfigurableMoveSet<TPiece> : MoveSet<TPiece>
        where TPiece : IPiece
    {
        private readonly Collector<TPiece> _collector;

        public ConfigurableMoveSet(Board<TPiece> board, Collector<TPiece> collector) : base(board)
        {
            _collector = collector;
        }

        public override List<Position> Positions(Position fromPosition)
            => _collector(Board, fromPosition);
    }
}
