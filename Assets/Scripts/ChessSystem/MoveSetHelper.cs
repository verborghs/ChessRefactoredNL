using BoardSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChessSystem
{
    internal class MoveSetHelper<TPiece>
        where TPiece : IPiece
    {
        private TPiece _currentPiece;
        private readonly Position _currentPosition;
        private Board<TPiece> _board;
        private List<Position> _positions = new List<Position>();

        public MoveSetHelper(Board<TPiece> board, Position position)
        {
            _currentPosition = position;
            _board = board;

            if (!_board.TryGetPieceAt(_currentPosition, out _currentPiece))
                Debug.Log($"Passed in a position {_currentPosition} which contains no piece to {nameof(MoveSetHelper<TPiece>)}.");
        }

        public MoveSetHelper<TPiece> Forward(int maxSteps = int.MaxValue, params Validator[] condition)
                => Collect(new Vector2Int(0, 1), maxSteps, condition);

        public MoveSetHelper<TPiece> ForwardRight(int maxSteps = int.MaxValue, params Validator[] condition)
            => Collect(new Vector2Int(1, 1), maxSteps, condition);

        public MoveSetHelper<TPiece> Right(int maxSteps = int.MaxValue, params Validator[] condition)
            => Collect(new Vector2Int(1, 0), maxSteps, condition);

        public MoveSetHelper<TPiece> BackwardRight(int maxSteps = int.MaxValue, params Validator[] condition)
            => Collect(new Vector2Int(1, -1), maxSteps, condition);

        public MoveSetHelper<TPiece> Backward(int maxSteps = int.MaxValue, params Validator[] condition)
        {
            return Collect(new Vector2Int(0, -1), maxSteps, condition);
        }

        public MoveSetHelper<TPiece> BackwardLeft(int maxSteps = int.MaxValue, params Validator[] condition)
            => Collect(new Vector2Int(-1, -1), maxSteps, condition);

        public MoveSetHelper<TPiece> Left(int maxSteps = int.MaxValue, params Validator[] condition)
            => Collect(new Vector2Int(-1, 0), maxSteps, condition);

        public MoveSetHelper<TPiece> ForwardLeft(int maxSteps = int.MaxValue, params Validator[] condition)
        => Collect(new Vector2Int(-1, 1), maxSteps, condition);


        public delegate bool Validator(Position currentPosition, Board<TPiece> board, Position targetTile);

        public MoveSetHelper<TPiece> Collect(Vector2Int direction, int maxSteps = int.MaxValue, params Validator[] condition)
        {
            if (_currentPiece == null)
                return this;

            //Added
            if(_currentPiece.Player == Player.Player2)
                direction.y = -direction.y;

            var currentStep = 0;

            //changed
            var position = new Position(_currentPosition.X + direction.x, _currentPosition.Y + direction.y);
            
                   //Changed
            while (_board.IsValid(position)
                && currentStep < maxSteps
                && (condition == null || condition.All((v) => v(_currentPosition, _board, position)))
                )
            {
                //Changed
                if (_board.TryGetPieceAt(position, out var piece))
                {
                    if (piece.Player != _currentPiece.Player)
                        _positions.Add(position);

                    break;
                }

                _positions.Add(position);

                //Changed
                position = new Position(position.X + direction.x, position.Y + direction.y);
                currentStep++;
            }

            return this;
        }


        public List<Position> ValidPositions()
        {
            return _positions;
        }
    }

}