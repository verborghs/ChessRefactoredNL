using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChessSystem
{
    public class Engine<TPiece>
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;
        private readonly MoveSetCollection<TPiece> _moveSetCollection;
        private Player _currentPlayer = Player.Player1;

        public MoveSetCollection<TPiece> MoveSet => _moveSetCollection;
        public Player CurrentPlayer => _currentPlayer;

        public Engine(Board<TPiece> board)
        {
            _board = board;
            _moveSetCollection = new MoveSetCollection<TPiece>(_board);
        }

        public bool Move(Position fromPosition, Position toPosition)
        {
            if (!_board.TryGetPieceAt(fromPosition, out var piece))
                return false;

            if (piece.Player != _currentPlayer)
                return false;

            if (!_moveSetCollection.TryGetMoveSet(piece.Type, out var moveSet))
                return false;

            if (!moveSet.Execute(fromPosition, toPosition))
                return false;

            ChangePlayer();

            return true;
        }



        //public List<Position> Positions(Position fromPosition)
        //{
        //    if (!_board.TryGetPieceAt(fromPosition, out var piece))
        //        return new List<Position>(0);
        //
        //    if (!_moveSetCollection.TryGetMoveSet(piece.Type, out var moveSet))
        //        return new List<Position>(0);
        //
        //    return moveSet.Positions(fromPosition);
        //}

        private void ChangePlayer()
            => _currentPlayer = (_currentPlayer == Player.Player1) ? Player.Player2 : Player.Player1;
    }
}
