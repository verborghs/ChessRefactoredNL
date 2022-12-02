using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    internal abstract class MoveSet<TPiece> : IMoveSet
         where TPiece : IPiece
    {

        private Board<TPiece> _board;

        protected Board<TPiece> Board => _board;

        protected MoveSet(Board<TPiece> board)
        {
            _board = board;
        }

        internal virtual bool Execute(Position fromPosition, Position toPosition)
        {
            var validPositions = Positions(fromPosition);
            if (!validPositions.Contains(toPosition))
                return false;

            _board.Take(toPosition);

            return _board.Move(fromPosition, toPosition);
        }

        public abstract List<Position> Positions(Position fromPosition);
        
    }
}
