using BoardSystem;
using ChessSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class PlayState : State
    {
        private BoardView _boardView;
        private Board<PieceView> _board;
        private Engine<PieceView> _engine;
        Position? _selectedPosition;

        public override void OnEnter()
        {
            var asyncOperaion = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            asyncOperaion.completed += InitializeScene;
        }

        private void InitializeScene(AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();

            _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
            _board.PieceMoved += (s, e) => e.Piece.MoveTo(e.ToPosition);
            _board.PieceTaken += (s, e) => e.Piece.Take();
            _board.PiecePlaced += (s, e) => e.Piece.Place(e.ToPosition);

            _engine = new Engine<PieceView>(_board);

            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(pieceView.GridPosition, pieceView);

            
            if(_boardView != null)
                _boardView.PositionClicked += PositionClicked;
        }


        public override void OnExit()
        {
            if (_boardView != null)
                _boardView.PositionClicked -= PositionClicked;

            SceneManager.UnloadSceneAsync("Game");
        }


        private void PositionClicked(object sender, PositionEventArgs eventArgs)
        {
            var clickedPosition = eventArgs.Position;
            
            var piececlicked = _board.TryGetPieceAt(clickedPosition, out var piece);
            var ownPieceClicked = piececlicked && piece.Player == _engine.CurrentPlayer;
                
            if(ownPieceClicked)
            {
                _selectedPosition = clickedPosition;
                
                var moveSet = _engine.MoveSet.For(piece.Type);
                var validPositions = moveSet.Positions(clickedPosition);
                _boardView.ActivatedPositions = validPositions;
            
            }
            else if(_selectedPosition != null)
            {
                if(_engine.Move(_selectedPosition.Value, clickedPosition))
                    _boardView.ActivatedPositions = null;
            }
        }
        
    }
}
