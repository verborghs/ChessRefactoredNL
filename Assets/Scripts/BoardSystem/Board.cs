using System;
using System.Collections.Generic;

namespace BoardSystem
{
    public class PieceMovedEventArgs<TPiece> : EventArgs
    {
        public TPiece Piece { get; }

        public Position FromPosition { get; }

        public Position ToPosition { get; }

        public PieceMovedEventArgs(TPiece piece, Position fromPosition, Position toPosition)
        {
            Piece = piece;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }

    public class PieceTakenEventArgs<TPiece> : EventArgs
    {
        public TPiece Piece { get; }

        public Position FromPosition { get; }

        public PieceTakenEventArgs(TPiece piece, Position fromPosition)
        {
            Piece = piece;
            FromPosition = fromPosition;
        }
    }

    public class PiecePlacedEventArgs<TPiece> : EventArgs
    {
        public TPiece Piece { get; }

        public Position ToPosition { get; }

        public PiecePlacedEventArgs(TPiece piece, Position toPosition)
        {
            Piece = piece;
            ToPosition = toPosition;
        }
    }


    public class PositionsActivatedEventArgs : EventArgs
    {
        public List<Position> OldPositions { get;  }
        public List<Position> NewPositions { get; }

        public PositionsActivatedEventArgs(List<Position> oldPositions, List<Position> newPositions)
        {
            OldPositions = oldPositions;
            NewPositions = newPositions;
        }
    }
    public class Board<TPiece>
    {

        public event EventHandler<PieceMovedEventArgs<TPiece>> PieceMoved;
        public event EventHandler<PieceTakenEventArgs<TPiece>> PieceTaken;
        public event EventHandler<PiecePlacedEventArgs<TPiece>> PiecePlaced;
       

        private readonly Dictionary<Position, TPiece> _pieces = new Dictionary<Position, TPiece>();

        private readonly int _rows;
        private readonly int _columns;



        public Board(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public bool Move(Position fromPosition, Position toPosition)
        {
            if (!IsValid(fromPosition))
                return false;

            if (!IsValid(toPosition))
                return false;

            if (_pieces.ContainsKey(toPosition))
                return false;

            if (!_pieces.TryGetValue(fromPosition, out var piece))
                return false;


            _pieces[toPosition] = piece;
            _pieces.Remove(fromPosition);

            OnPieceMoved(new PieceMovedEventArgs<TPiece>(piece, fromPosition, toPosition));

            return true;
        }

        public bool Place(Position toPosition, TPiece piece)
        {
            if (!IsValid(toPosition))
                return false;

            if (_pieces.ContainsKey(toPosition))
                return false;

            if (_pieces.ContainsValue(piece))
                return false;


            _pieces[toPosition] = piece;

            OnPiecePlaced(new PiecePlacedEventArgs<TPiece>(piece, toPosition));

            return true;
        }

        public bool Take(Position fromPosition)
        {
            if (!IsValid(fromPosition))
                return false;

            if (!_pieces.TryGetValue(fromPosition, out var piece))
                return false;

            _pieces.Remove(fromPosition);

            OnPieceTaken(new PieceTakenEventArgs<TPiece>(piece, fromPosition));

            return true;
        }

        public bool TryGetPieceAt(Position position, out TPiece piece)
            => _pieces.TryGetValue(position, out piece);

        public bool IsValid(Position position)
            => (0 <= position.X && position.X < _columns) && (0 <= position.Y && position.Y < _rows);


        #region EventTriggers
        protected virtual void OnPieceMoved(PieceMovedEventArgs<TPiece> eventArgs)
        {
            var handler = PieceMoved;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPiecePlaced(PiecePlacedEventArgs<TPiece> eventArgs)
        {
            var handler = PiecePlaced;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnPieceTaken(PieceTakenEventArgs<TPiece> eventArgs)
        {
            var handler = PieceTaken;
            handler?.Invoke(this, eventArgs);
        }
        #endregion


        // public event EventHandler<PositionsActivatedEventArgs> PositionsActivated;
        //private List<Position> _activatedPositions;
        //
        //public List<Position> ActivatedPositions
        //{
        //    get
        //    {
        //
        //        return _activatedPositions;
        //    }
        //
        //    set
        //    {
        //        var eventArgs = new PositionsActivatedEventArgs(_activatedPositions, value);
        //
        //        _activatedPositions = value;
        //
        //        OnPositionsActivated(eventArgs);
        //    }
        //}
        //
        //private void OnPositionsActivated(PositionsActivatedEventArgs eventArgs)
        //{
        //    var handler = PositionsActivated;
        //    handler?.Invoke(this, eventArgs);
        //}
    }

}