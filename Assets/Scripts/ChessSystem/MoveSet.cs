using BoardSystem;
using CommandSystem;
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
        private readonly CommandQueue _commandQueue;

        protected Board<TPiece> Board => _board;
        protected CommandQueue CommandQueue => _commandQueue;


        protected MoveSet(Board<TPiece> board, CommandQueue commandQueue)
        {
            _board = board;
            _commandQueue = commandQueue;
        }

        internal virtual bool Execute(Position fromPosition, Position toPosition)
        {
            var validPositions = Positions(fromPosition);
            if (!validPositions.Contains(toPosition))
                return false;

            var pieceTaken = _board.TryGetPieceAt(toPosition, out var takenPiece);

            Action execute = () =>
           {
               if (pieceTaken)
                   _board.Take(toPosition);

               _board.Move(fromPosition, toPosition);
           };

            Action undo = () =>
            {
                _board.Move(toPosition, fromPosition);

                if (pieceTaken)
                    _board.Place(toPosition, takenPiece);
            };


            //var command = new MoveCommand<TPiece>(_board, toPosition, fromPosition);
            var command = new DelegateCommand(execute, undo);
            _commandQueue.Execute(command);

            //CommandQueue.GetInstance().Execute(command);

            return true;
        }

        public abstract List<Position> Positions(Position fromPosition);

    }

    internal class MoveCommand<TPiece> : ICommand
    {
        Board<TPiece> _board;
        Position _toPosition;
        Position _fromPosition;
        TPiece _takenPiece;
        private bool _pieceTaken;

        public MoveCommand(Board<TPiece> board, Position toPosition, Position fromPosition)
        {
            _board = board;
            _toPosition = toPosition;
            _fromPosition = fromPosition;
        }

        public void Execute()
        {
            _pieceTaken = _board.TryGetPieceAt(_toPosition, out _takenPiece);

            if (_pieceTaken)
            {
                _pieceTaken &= _board.Take(_toPosition);
            }

            _board.Move(_fromPosition, _toPosition);
        }

        public void Undo()
        {
            _board.Move(_toPosition, _fromPosition);

            if (_pieceTaken)
                _board.Place(_toPosition, _takenPiece);
        }
    }
}

