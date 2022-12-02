using BoardSystem;
using ChessSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using UnityEngine;


namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {

        private Board<PieceView> _board;
        private Engine<PieceView> _engine;

        private void Start()
        {

            _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
            _board.PieceMoved += (s, e) => e.Piece.MoveTo(e.ToPosition);
            _board.PieceTaken += (s, e) => e.Piece.Take();
            _board.PiecePlaced += (s, e) => e.Piece.Place(e.ToPosition);

            _engine = new Engine<PieceView>(_board);

            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(pieceView.GridPosition, pieceView);

            var boardView = FindObjectOfType<BoardView>();
            boardView.PositionClicked += PositionClicked;
        }


        private void PositionClicked(object sender, PositionEventArgs eventArgs)
        {
            var fromPosition = eventArgs.Position;
            Debug.Log($"{fromPosition}");


            if (!_board.TryGetPieceAt(fromPosition, out var piece))
                return;


            var moveSet = _engine.MoveSet.For(piece.Type);

            var validPositions = moveSet.Positions(fromPosition);            
            if (validPositions.Count == 0)
                return;

            var toPosition = validPositions[0];

            _engine.Move(fromPosition, toPosition);

        }
    }

}