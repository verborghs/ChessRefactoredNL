using BoardSystem;
using ChessSystem;
using GameSystem.Helpers;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPiece
    {

        [SerializeField]
        private PieceType _type;

        [SerializeField]
        private Player _player;

        public Position GridPosition => PositionHelper.GridPosition(transform.position);

        public PieceType Type => _type;

        public Player Player => _player;



        private void Awake()
        {
            var gridPositon = PositionHelper.GridPosition(transform.position);
            transform.position = PositionHelper.WorldPosition(gridPositon);
        }


        #region View Methods
        internal void MoveTo(Position toPosition)
            => transform.position = PositionHelper.WorldPosition(toPosition);


        internal void Take()
            => gameObject.SetActive(false);


        internal void Place(Position toPosition)
        {
            gameObject.SetActive(true);
            MoveTo(toPosition);
        }
        #endregion



    }

}