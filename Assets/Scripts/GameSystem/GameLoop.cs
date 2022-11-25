using BoardSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using UnityEngine;


namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {

        private Board<PieceView> _board;
        private void Start()
        {

            _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
            _board.PieceMoved += (s, e) => e.Piece.MoveTo(e.ToPosition);
            _board.PieceTaken += (s, e) => e.Piece.Take();
            _board.PiecePlaced += (s, e) => e.Piece.Place(e.ToPosition);


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

            var toPosition = new Position(fromPosition.X, fromPosition.Y + 1);
            _board.Take(toPosition);
            _board.Move(fromPosition, toPosition);

        }
    }

}